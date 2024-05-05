using CsvHelper.Configuration.Attributes;
using System.Numerics;

namespace laserControl
{
    public class LaserDevice
    {
        private IOPort ioPort;
        private IEnumerator<Target>? trajectory;

        /// <summary>
        /// device starts retracing setted trajectory after call
        /// </summary>
        public IEnumerator<Target>? Trajectory
        {
            get => trajectory;
            set
            {
                ClearBuffer();
                trajectory = value;
                loadTargetsToDeviceBuffer();
            }
        }

        private bool moveNext() => Trajectory?.MoveNext() == true;

        /// <summary>
        /// transfers buffered trajectory until device buffer available or until trajectory completely transfered
        /// </summary>
        private void loadTargetsToDeviceBuffer()
        {
            var nextAvailable = moveNext();
            while (nextAvailable & bufferAvailable > 16)
            {
                AddTarget(Trajectory.Current); //Trajectory is not null here
                nextAvailable = moveNext();
            }
            if (!nextAvailable) trajectory = null;
        }

        public LaserDevice(IOPort ioPort)
        {
            this.ioPort = ioPort;
            ioPort.OnReceive = message =>
            {
                if (message[5] == (byte)LaserDeviceMessageType.TargetReached)
                {
                    bufferedTrajectoryLength = Math.Max(0, bufferedTrajectoryLength - 1);
                    loadTargetsToDeviceBuffer();
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

        /// <summary>
        /// clears buffered trajectory and sends trajectory with the single target
        /// discards remained trajectory that was set via LaserDevice.Trajectory
        /// </summary>
        public void SetTarget(Target target)
        {
            ClearBuffer();
            AddTarget(target);
        }

        private void AddTarget(Target target)
        {
            bufferedTrajectoryLength = Math.Max(0, bufferedTrajectoryLength + 1);
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
            trajectory = null;
            bufferedTrajectoryLength = 0;
            ioPort.Send(LaserDeviceMessage.ClearBuffer());
        }

        private int trajectoryBufferLength => 32;

        private int bufferedTrajectoryLength = 0;

        private int bufferAvailable => trajectoryBufferLength - bufferedTrajectoryLength;

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