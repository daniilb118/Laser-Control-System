using System.IO.Ports;

namespace laserControl
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort;
        private LaserDevice laserDevice;

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
            initializeControls();
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
        }
    }
}
