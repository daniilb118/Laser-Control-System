namespace laserControl
{
    public partial class DeviceConfigurationForm : Form
    {
        public Action OnApply {  get; set; } = () => {};

        public DeviceConfigurationForm(LaserDeviceProfile deviceProfile)
        {
            InitializeComponent();

            screenSizeSetter.DecimalPlaces = 5;
            screenDistanceSetter.DecimalPlaces = 5;
            mirrorDistanceSetter.DecimalPlaces = 5;
            maxSpeedSetter.DecimalPlaces = 5;

            applyButton.Click += new EventHandler((object? sender, EventArgs e) => {
                deviceProfile.ScreenSize = (float)screenSizeSetter.Value;
                deviceProfile.ScreenDistance = (float)screenDistanceSetter.Value;
                deviceProfile.MirrorDistance = (float)mirrorDistanceSetter.Value;
                deviceProfile.MotorStepsPerRotation = (uint)motorStepsPerRotationSetter.Value;
                deviceProfile.IsAxisXInverted = axisXInvertedSetter.Checked;
                deviceProfile.IsAxisYInverted = axisYInvertedSetter.Checked;
                deviceProfile.MaxSpeed = (uint)maxSpeedSetter.Value;
                deviceProfile.AxisXBacklash = (ushort)axisXbacklashSetter.Value;
                deviceProfile.AxisYBacklash = (ushort)axisYbacklashSetter.Value;

                OnApply();
            });

            screenSizeSetter.Value = (decimal)deviceProfile.ScreenSize;
            screenDistanceSetter.Value = (decimal)deviceProfile.ScreenDistance;
            mirrorDistanceSetter.Value = (decimal)deviceProfile.MirrorDistance;
            motorStepsPerRotationSetter.Value = deviceProfile.MotorStepsPerRotation;
            axisXInvertedSetter.Checked = deviceProfile.IsAxisXInverted;
            axisYInvertedSetter.Checked = deviceProfile.IsAxisYInverted;
            maxSpeedSetter.Value = deviceProfile.MaxSpeed;
            axisXbacklashSetter.Value = deviceProfile.AxisXBacklash;
            axisYbacklashSetter.Value = deviceProfile.AxisYBacklash;
        }
    }
}
