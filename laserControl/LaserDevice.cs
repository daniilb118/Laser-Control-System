using CsvHelper.Configuration.Attributes;
using System.Numerics;

namespace laserControl
{
    public class LaserDevice
    {
        private IOPort ioPort;

        public LaserDevice(IOPort ioPort)
        {
            this.ioPort = ioPort;
            ioPort.OnReceive = message =>
            {
                if (message[5] == (byte)LaserDeviceMessageType.TargetReached)
                {
                    OnTargetReach();
                }
            };
        }

        public LaserDeviceProfile Profile { get; set; } = new();

        /// <summary>
        /// sends device-side profile properties to the device
        /// </summary>
        public void SendProfile()
        {
            ioPort.Send(LaserDeviceMessage.SetBacklashX(Profile.AxisXBacklash));
            ioPort.Send(LaserDeviceMessage.SetBacklashY(Profile.AxisYBacklash));
        }

        public void AddTarget(Target target)
        {
            var motorsTarget = Profile.DiscreteMotorsPosition(target.Position);
            ioPort.Send(LaserDeviceMessage.AddTarget(motorsTarget, (byte)(target.Intensity * 255)));
        }

        public UInt32 Speed
        {
            set
            {
                if (value > Profile.MaxSpeed) return;
                ioPort.Send(LaserDeviceMessage.SetSpeed(value));
            }
        }

        public void ResetOrigin()
        {
            ioPort.Send(LaserDeviceMessage.ResetOrigin());
        }

        public void ClearBuffer()
        {
            ioPort.Send(LaserDeviceMessage.ClearBuffer());
        }

        private int trajectoryBufferLength => 32;

        private int bufferedTrajectoryLength = 0;

        public delegate void OnTargetReachDelegate();

        /// <summary>
        /// called when device physically reaches target of setted trajectory
        /// </summary>
        public OnTargetReachDelegate OnTargetReach { get; set; } = () => { };

        public class Target(float x, float y, float intensity)
        {
            public Target(Vector2 position, float intensity) :
                this(position.X, position.Y, intensity)
            { }

            [Name("x")]
            public float X { get; set; } = x;
            [Name("y")]
            public float Y { get; set; } = y;
            [Name("intensity")]
            public float Intensity { get; set; } = intensity;
            [Ignore]
            public Vector2 Position
            {
                get => new Vector2(X, Y);
                set
                {
                    X = value.X;
                    Y = value.Y;
                }
            }
        }
    }
}