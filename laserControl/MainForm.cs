using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Text.Json;

namespace laserControl
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort;
        private LaserDevice laserDevice;
        private LaserTrajectoryEditor laserTrajectoryEditor;

        private enum Mode
        {
            Stop = 0,
            Manual = 1,
            Auto = 2,
            Repeat = 3,
        }

        private void MessageOnError(Action method)
        {
            try { method(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private Mode programMode
        {
            get => (Mode)programModeSetter.SelectedIndex;
            set
            {
                programModeSetter.SelectedIndex = (int)value;
            }
        }

        public MainForm()
        {
            serialPort = new SerialPort();

            IOPort port = new(serialPort, LaserDeviceMessage.Size);

            laserDevice = new LaserDevice(port);

            InitializeComponent();

            laserTrajectoryEditor = new(laserDevice, targetGridView, screenVisualizationPanel, laserPositionLabel, intensitySetter);

            updateMenu(serialPort.IsOpen);

            initializeProtocolDebugger(port);
            initializeLaserControlPanel();
            initializeControls();
            initializeStripMenu();
        }

        private void onProfileUpdate()
        {
            laserTrajectoryEditor.ScreenSize = laserDevice.Profile.ScreenSize;
            if (serialPort.IsOpen) { laserDevice.SendProfile(); }
        }

        private void updateMenu(bool isOpen)
        {
            connectionButton.Text = isOpen ? "Disconnect" : "Connect";
            programModeSetter.Enabled = isOpen;
        }

        [ConditionalAttribute("DEBUG")]
        private void initializeProtocolDebugger(IOPort port)
        {
            var testButton = new Button();
            screenVisualizationPanel.Controls.Add(testButton);
            testButton.Click += (object? o, EventArgs e) =>
            {
                var label = new Label();
                label.Size = new(1000, 1000);
                label.Click += (object? o, EventArgs e) => label.Text = "Debug:\n";
                port.OnFrame = (Frame frame, string who) =>
                {
                    label.Text += frame.isAuxiliary == 1
                        ? $"{Convert.ToHexString(frame.message[0..5])} {frame.thisACK} {frame.thatACK} {who}\n"
                        : $"{Convert.ToHexString(frame.message[0..5])} {frame.isAuxiliary} {frame.thisACK} {frame.thatACK} {(LaserDeviceMessageType)frame.message[5]} {who}\n";
                };
                var form = new Form();
                form.Controls.Add(label);
                form.Show();
            };
        }

        private void initializeLaserControlPanel()
        {
            programModeSetter.Items.AddRange(["Stop", "Manual", "Auto", "Repeat"]);

            programMode = Mode.Stop;

            programModeSetter.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                if (programMode == Mode.Stop)
                {
                    laserDevice.SetTarget(new(0, 0, 0));
                }
                else if (programMode == Mode.Auto | programMode == Mode.Repeat)
                {
                    laserDevice.Trajectory = laserTrajectoryEditor.GetTargets(laserTrajectoryEditor.SelectedIndex, programMode == Mode.Auto ? laserTrajectoryEditor.TrajectoryLength : null).GetEnumerator();
                }
            };

            laserDevice.OnTargetReach = () =>
            {
                laserTrajectoryEditor.LaserPosition = laserTrajectoryEditor.TargetLaserPosition;
                if (programMode == Mode.Auto)
                {
                    if (laserTrajectoryEditor.SelectedIndex < laserTrajectoryEditor.TrajectoryLength - 1)
                    {
                        laserTrajectoryEditor.SelectedIndex = laserTrajectoryEditor.SelectedIndex + 1;
                    }
                    else
                    {
                        programMode = Mode.Manual;
                    }
                }
                else if (programMode == Mode.Repeat)
                {
                    laserTrajectoryEditor.SelectedIndex = (laserTrajectoryEditor.SelectedIndex + 1) % laserTrajectoryEditor.TrajectoryLength;
                }
            };
        }

        private void initializeControls()
        {
            splitContainer1.FixedPanel = FixedPanel.Panel1;

            laserTrajectoryEditor.OnUserSelectedTargetChanged += (LaserDevice.Target target) =>
            {
                if (programMode == Mode.Auto | programMode == Mode.Repeat) { programMode = Mode.Manual; }
                if (programMode != Mode.Manual) return;
                laserDevice.SetTarget(target);
            };

            connectionButton.Click += (object? sender, EventArgs e) =>
            {
                MessageOnError(() =>
                {
                    if (serialPort.IsOpen)
                    {
                        if (programMode == Mode.Stop)
                        {
                            serialPort.Close();
                        }
                        else
                        {
                            programMode = Mode.Stop;
                        }
                    }
                    else
                    {
                        serialPort.Open();
                    }
                });

                if (serialPort.IsOpen)
                {
                    laserDevice.SendProfile();
                    laserDevice.Speed = (uint)speedSetter.Value;
                }

                updateMenu(serialPort.IsOpen);
            };

            serialPortSelector.DropDown += (object? sender, EventArgs e) =>
            {
                serialPortSelector.Items.Clear();
                serialPortSelector.Items.AddRange(SerialPort.GetPortNames());
            };

            serialPortSelector.SelectedIndexChanged += (object? sender, EventArgs e) =>
            {
                serialPort.PortName = serialPortSelector.SelectedItem?.ToString();
            };

            speedSetter.ValueChanged += (object? sender, EventArgs e) =>
            {
                if (!serialPort.IsOpen) return;
                laserDevice.Speed = (uint)speedSetter.Value;
            };

            splitContainer1.FixedPanel = FixedPanel.Panel1;
        }

        private void initializeStripMenu()
        {
            menuStrip.BackColor = Color.Transparent;

            importDeviceProfileToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                OpenFileDialog fileDialog = new();
                fileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
                MessageOnError(() =>
                {
                    if (fileDialog.ShowDialog() != DialogResult.OK) return;
                    var profile = JsonSerializer.Deserialize<LaserDeviceProfile>(File.ReadAllText(fileDialog.FileName));
                    if (profile == null) throw new Exception("Couldn't read device profile.");
                    laserDevice.Profile = profile;
                    onProfileUpdate();
                });
            };

            exportDeviceProfileToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                SaveFileDialog fileDialog = new();
                fileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
                MessageOnError(() =>
                {
                    if (fileDialog.ShowDialog() != DialogResult.OK) return;
                    File.WriteAllText(fileDialog.FileName, JsonSerializer.Serialize(laserDevice.Profile));
                });
            };

            importTrajectoryToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                OpenFileDialog fileDialog = new();
                fileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                MessageOnError(() =>
                {
                    using var reader = new StreamReader(fileDialog.FileName);
                    using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                    laserTrajectoryEditor.NormalizedTargets = new List<LaserDevice.Target>(csvReader.GetRecords<LaserDevice.Target>());
                });
            };

            exportTrajectoryToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                SaveFileDialog fileDialog = new();
                fileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                MessageOnError(() =>
                {
                    if (fileDialog.ShowDialog() != DialogResult.OK) return;
                    using var writer = new StreamWriter(fileDialog.FileName);
                    using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
                    csvWriter.WriteRecords(laserTrajectoryEditor.NormalizedTargets);
                });
            };

            chooseBackgroundToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                OpenFileDialog backgroundOpenDialog = new();
                backgroundOpenDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                if (backgroundOpenDialog.ShowDialog() != DialogResult.OK) return;
                laserTrajectoryEditor.Background = new Bitmap(backgroundOpenDialog.FileName);
            };

            clearBackgroundToolStripMenuItem.Click += (object? sender, EventArgs e) => laserTrajectoryEditor.Background = null;

            configureDeviceToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                DeviceConfigurationForm form = new(laserDevice.Profile);
                form.OnApply += (LaserDeviceProfile profile) =>
                {
                    laserDevice.Profile = profile;
                    onProfileUpdate();
                };
                form.Show();
            };

            moveTo00ToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                laserDevice.SetTarget(new(0, 0, 0));
            };
        }
    }
}
