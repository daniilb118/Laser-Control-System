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

        public void AddTarget(Vector2 laserPosition, byte intensity)
        {
            var target = Profile.ToMotorSteps(Profile.AngularMotorsPosition(laserPosition));
            ioPort.Send(LaserDeviceMessage.AddTarget(target, intensity));
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

        public delegate void OnTargetReachDelegate();

        /// <summary>
        /// called when device physically reaches target of setted trajectory
        /// </summary>
        public OnTargetReachDelegate OnTargetReach { get; set; } = () => { };
    }
}