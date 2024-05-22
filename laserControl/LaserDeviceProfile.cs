using System.ComponentModel;
using System.Numerics;
using System.Text.Json.Serialization;

namespace laserControl
{
    public class LaserDeviceProfile
    {
        //properties of physical device installation that affect only PC-side computations
        public float ScreenSize { get => screensSize; set => screensSize = numberPositivenessCheck(value); } //meters
        public float ScreenDistance { get => screenDistance; set => screenDistance = numberPositivenessCheck(value); } //meters
        public float MirrorDistance { get => mirrorDistance; set => mirrorDistance = numberPositivenessCheck(value); } //meters
        public UInt32 MotorStepsPerRotation { get; set; } = 4096; //motor steps
        public bool IsAxisXInverted { get; set; } = false;
        public bool IsAxisYInverted { get; set; } = false;
        public UInt32 MaxSpeed { get; set; } = 100; //motors steps per second
        //device-side properties
        public UInt16 AxisXBacklash { get; set; } = 0; //motor steps
        public UInt16 AxisYBacklash { get; set; } = 0; //motor steps

        //duplicate of CompensationOffsets but serializable
        public float[][] SerializableCompensationOffsets
        {
            get => CompensationOffsets.Select(i => new float[] { i.X, i.Y }).ToArray();
            set {
                if (value.Length != CompensationGridSize * CompensationGridSize | value.Any(i => i.Length != 2))
                    throw new ArgumentException("CompensationOffsets is not a float[16][2] array.");
                CompensationOffsets = value.Select(i => new Vector2(i[0], i[1])).ToArray(); 
            }
        }

        [JsonIgnore]
        public Vector2[] CompensationOffsets { get; set; } = new Vector2[CompensationGridSize * CompensationGridSize];

        public static int CompensationGridSize => 4;

        public Vector2 AngularMotorsPosition(Vector2 laserPosition)
        {
            static double transform(double x) => x + ((x < 0) ? Math.PI / 2 : 0);
            double beta = transform(Math.Atan(ScreenDistance / laserPosition.Y) / 2);
            double alpha = transform(Math.Atan((ScreenDistance / Math.Sin(2 * beta) + MirrorDistance) / laserPosition.X) / 2);
            return new Vector2((float)alpha, (float)beta);
        }

        /// <summary>
        /// A linear compenstation of trajectory deviation based on CompenstationOffsets.
        /// </summary>
        /// <param name="position">
        /// A position inside square 2x2, it's not scaled to the screen size.
        /// </param>
        public Vector2 Compensate(Vector2 position)
        {
            var scaledPosition = ((position + Vector2.One) / 2) * (CompensationGridSize - 1); //[0; compensationGridSize]
            var gridX = Math.Clamp((int)Math.Floor(scaledPosition.X), 0, CompensationGridSize - 2);
            var gridY = Math.Clamp((int)Math.Floor(scaledPosition.Y), 0, CompensationGridSize - 2);
            var relativePosition = scaledPosition - new Vector2(gridX, gridY);
            var pointIndex = gridY * CompensationGridSize + gridX;
            Vector2 horizontalLerp(int i) => Vector2.Lerp(CompensationOffsets[i], CompensationOffsets[i + 1], relativePosition.X);
            var compensationOffset = Vector2.Lerp(horizontalLerp(pointIndex), horizontalLerp(pointIndex + CompensationGridSize), relativePosition.Y);
            return position + compensationOffset;
        }

        public Int16[] DiscreteMotorsPosition(Vector2 laserPosition) => ToMotorSteps(AngularMotorsPosition(laserPosition));

        private Int16[] ToMotorSteps(Vector2 angularMotorsPosition)
        {
            var origin = AngularMotorsPosition(new Vector2(0, 0));
            var result = (angularMotorsPosition - origin) * (float)(MotorStepsPerRotation / (Math.PI * 2));
            return [Convert.ToInt16(IsAxisXInverted ? -result.X : result.X), Convert.ToInt16(IsAxisYInverted ? -result.Y : result.Y)];
        }

        private float numberPositivenessCheck(float value)
        {
            if (value <= 0) throw new ArgumentException("the value must be bigger than zero");
            return value;
        }

        private float screensSize = 1;
        private float screenDistance = 1;
        private float mirrorDistance = 0.1f;
    }
}
