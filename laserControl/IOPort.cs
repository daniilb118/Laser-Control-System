using System.IO.Ports;

namespace laserControl
{
    public class IOPort(SerialPort port)
    {
        private SerialPort port = port;

        public void Send(byte[] message)
        {

        }

        public OnReceiveFunc OnReceive { get; set; } = message => {};

        public delegate void OnReceiveFunc(byte[] message);
    }
}
