using System.Drawing.Drawing2D;
using System.Text.Json;

namespace laserControl
{
    public partial class DeviceConfigurationForm : Form
    {
        public Action<LaserDeviceProfile> OnApply { get; set; } = (LaserDeviceProfile profile) => { };

        public DeviceConfigurationForm(LaserDeviceProfile deviceProfile)
        {
            InitializeComponent();

            screenSizeSetter.DecimalPlaces = 5;
            screenDistanceSetter.DecimalPlaces = 5;
            mirrorDistanceSetter.DecimalPlaces = 5;
            maxSpeedSetter.DecimalPlaces = 5;

            var bufferedDeviceProfile = JsonSerializer.Deserialize<LaserDeviceProfile>(JsonSerializer.Serialize(deviceProfile));
            if (bufferedDeviceProfile == null) return; //unreachable

            var calibrationPanel = new CalibrationPanel(calibrationBackgroundPanel, bufferedDeviceProfile, screenPointPositionLabel);

            screenSizeSetter.ValueChanged += (object? sender, EventArgs e) =>
            {
                bufferedDeviceProfile.ScreenSize = (float)screenSizeSetter.Value;
                calibrationPanel.Redraw();
            };

            applyButton.Click += new EventHandler((object? sender, EventArgs e) => {

                bufferedDeviceProfile.ScreenDistance = (float)screenDistanceSetter.Value;
                bufferedDeviceProfile.MirrorDistance = (float)mirrorDistanceSetter.Value;
                bufferedDeviceProfile.MotorStepsPerRotation = (uint)motorStepsPerRotationSetter.Value;
                bufferedDeviceProfile.IsAxisXInverted = axisXInvertedSetter.Checked;
                bufferedDeviceProfile.IsAxisYInverted = axisYInvertedSetter.Checked;
                bufferedDeviceProfile.MaxSpeed = (uint)maxSpeedSetter.Value;
                bufferedDeviceProfile.AxisXBacklash = (ushort)axisXbacklashSetter.Value;
                bufferedDeviceProfile.AxisYBacklash = (ushort)axisYbacklashSetter.Value;

                OnApply(bufferedDeviceProfile);

                calibrationPanel.Redraw();
            });

            screenSizeSetter.Value = (decimal)bufferedDeviceProfile.ScreenSize;
            screenDistanceSetter.Value = (decimal)bufferedDeviceProfile.ScreenDistance;
            mirrorDistanceSetter.Value = (decimal)bufferedDeviceProfile.MirrorDistance;
            motorStepsPerRotationSetter.Value = bufferedDeviceProfile.MotorStepsPerRotation;
            axisXInvertedSetter.Checked = bufferedDeviceProfile.IsAxisXInverted;
            axisYInvertedSetter.Checked = bufferedDeviceProfile.IsAxisYInverted;
            maxSpeedSetter.Value = bufferedDeviceProfile.MaxSpeed;
            axisXbacklashSetter.Value = bufferedDeviceProfile.AxisXBacklash;
            axisYbacklashSetter.Value = bufferedDeviceProfile.AxisYBacklash;

            splitContainer1.FixedPanel = FixedPanel.Panel1;
        }
    }
}
