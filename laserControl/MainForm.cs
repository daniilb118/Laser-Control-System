using System.IO.Ports;

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
                laserDevice.AddTarget(targetPos, 0);
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

            chooseBackgroundToolStripMenuItem.Click += (object? sender, EventArgs e) =>
            {
                OpenFileDialog backgroundOpenDialog = new();
                backgroundOpenDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                if (backgroundOpenDialog.ShowDialog() != DialogResult.OK) return;
                visualizationPanel.Background = new Bitmap(backgroundOpenDialog.FileName);
            };

            clearBackgroundToolStripMenuItem.Click += (object? sender, EventArgs e) => visualizationPanel.Background = null;
        }
    }
}
