using System.Numerics;

namespace laserControl
{
    public class LaserDevice()
    {
        private LaserDeviceProfile profile = new();

        /// <summary>
        /// sends device-side profile properties to the device
        /// </summary>
        private void sendProfile()
        {

        }

        public LaserDeviceProfile Profile
        {
            get => profile;
            set
            {
                profile = value;
                sendProfile();
            }
        }

        public void SetTarget(Vector2 laserPosition, byte intensity)
        {
            var target = profile.ToMotorSteps(profile.AngularMotorsPosition(laserPosition));
            //TODO: send target to a device
        }

        public UInt32 Speed
        {
            set
            {
                if (value > Profile.MaxSpeed) return;
                //TODO: send speed value to a device
            }
        }

        public delegate void OnTargetReachDelegate();

        /// <summary>
        /// called when device physically reaches target of setted trajectory
        /// </summary>
        public OnTargetReachDelegate OnTargetReach { get; set; } = () => { };
    }
}