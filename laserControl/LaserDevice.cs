﻿using CsvHelper.Configuration.Attributes;
using System.Numerics;
using static laserControl.LaserDevice;

namespace laserControl
{
    public class LaserDevice
    {
        public delegate void OnTargetReachDelegate();

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
                isBufferedTrajectoryEnded = false;
                loadTargetsToDeviceBuffer();
            }
        }

        public LaserDeviceProfile Profile { get; set; } = new();

        public UInt32 Speed
        {
            set
            {
                if (value > Profile.MaxSpeed) return;
                ioPort.Send(LaserDeviceMessage.SetSpeed(value));
            }
        }

        public Vector2 Position
        {
            //declares current laser position for synchronization (doesn't move laser)
            set => ioPort.Send(LaserDeviceMessage.DeclarePosition(Profile.DiscreteMotorsPosition(value)));
        }

        /// <summary>
        /// sends device-side profile properties to the device
        /// </summary>
        public void SendProfile()
        {
            ioPort.Send(LaserDeviceMessage.SetBacklashX(Profile.AxisXBacklash));
            ioPort.Send(LaserDeviceMessage.SetBacklashY(Profile.AxisYBacklash));
        }

        public void ClearBuffer()
        {
            trajectory = null;
            bufferedTrajectoryLength = 0;
            ioPort.Send(LaserDeviceMessage.ClearBuffer());
            isBufferedTrajectoryEnded = true;
        }

        /// <summary>
        /// clears buffered trajectory and sends trajectory with the single target
        /// discards remained trajectory that was set via LaserDevice.Trajectory
        /// </summary>
        public void SetTarget(Target target)
        {
            ClearBuffer();
            AddTarget(target);
            ioPort.Send(LaserDeviceMessage.EndTrajectory());
            isBufferedTrajectoryEnded = true;
        }

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

        /// <summary>
        /// transfers buffered trajectory until device buffer available or until trajectory completely transfered
        /// </summary>
        private void loadTargetsToDeviceBuffer()
        {
            var nextAvailable = moveNext();
            while (nextAvailable & bufferAvailable > 16)
            {
                if (Trajectory == null) return; //unreachable
                AddTarget(Trajectory.Current);
                nextAvailable = moveNext();
            }
            if (!nextAvailable & !isBufferedTrajectoryEnded)
            {
                trajectory = null;
                ioPort.Send(LaserDeviceMessage.EndTrajectory());
                isBufferedTrajectoryEnded = true;
            }
        }

        private IOPort ioPort;
        private IEnumerator<Target>? trajectory;
        private bool isBufferedTrajectoryEnded = true;
        private int bufferedTrajectoryLength = 0;

        private bool moveNext() => Trajectory?.MoveNext() == true;
        private int trajectoryBufferLength => 32;
        private int bufferAvailable => trajectoryBufferLength - bufferedTrajectoryLength;

        private Int16[] compensatedMotorDiscretePosition(Vector2 laserPosition)
        {
            return Profile.DiscreteMotorsPosition(Profile.Compensate(laserPosition / Profile.ScreenSize) * Profile.ScreenSize);
        }

        private void AddTarget(Target target)
        {
            bufferedTrajectoryLength = Math.Max(0, bufferedTrajectoryLength + 1);
            ioPort.Send(LaserDeviceMessage.AddTarget(compensatedMotorDiscretePosition(target.Position), (byte)(target.Intensity * 255)));
        }
    }
}