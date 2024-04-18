using System.IO.Ports;
using System.Text.Json;

namespace laserControl
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort;
        private LaserDevice laserDevice;
        private ScreenVisualizationPanel visualizationPanel;

        private void MessageOnError(Action method)
        {
            try { method(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public MainForm()
        {
            serialPort = new SerialPort();

            laserDevice = new LaserDevice(new IOPort(serialPort, LaserDeviceMessage.Size));

            InitializeComponent();

            visualizationPanel = new(screenVisualizationPanel, laserDevice, cursorLabel);

            laserDevice.OnTargetReach = () =>
            {
                visualizationPanel.LaserPosition = visualizationPanel.TargetLaserPosition;
            };

            initializeControls();
            initializeStripMenu();
        }

        private void onProfileUpdate()
        {
            if (serialPort.IsOpen) { laserDevice.SendProfile(); }
        }

        private void initializeControls()
        {
            connectionButton.Click += (object? sender, EventArgs e) =>
            {
                MessageOnError(() =>
                {
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                        connectionButton.Text = "Connect";
                    }
                    else
                    {
                        serialPort.Open();
                        connectionButton.Text = "Disconnect";
                    }
                });
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

            screenVisualizationPanel.MouseDown += (object? sender, MouseEventArgs e) =>
            {
                if (!serialPort.IsOpen) return;
                var targetPos = visualizationPanel.GetLaserPosition(e.Location);
                laserDevice.AddTarget(new(targetPos.X, targetPos.Y, 0));
                visualizationPanel.TargetLaserPosition = targetPos;
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

            chooseBackgroundToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                OpenFileDialog backgroundOpenDialog = new();
                backgroundOpenDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                if (backgroundOpenDialog.ShowDialog() != DialogResult.OK) return;
                visualizationPanel.Background = new Bitmap(backgroundOpenDialog.FileName);
            };

            clearBackgroundToolStripMenuItem.Click += (object? sender, EventArgs e) => visualizationPanel.Background = null;

            configureDeviceToolStripMenuItem.Click += (object? sender, EventArgs e) => {
                DeviceConfigurationForm form = new(laserDevice.Profile);
                form.OnApply += onProfileUpdate;
                form.Show();
            };

            moveTo00ToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                laserDevice.AddTarget(new(0, 0, 0));
            };
        }
    }
}
