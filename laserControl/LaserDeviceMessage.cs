namespace laserControl
{
    enum LaserDeviceMessageType : byte
    {
        SetSpeed = 0,
        SetBacklashX = 1,
        SetBacklashY = 2,
        ResetOrigin = 3,
        SetTarget = 4,
        ClearBuffer = 5,
        Echo = 6,
        TargetReached = 7,
        DebugInfo = 8,
    }

    internal class LaserDeviceMessage
    {
        public static int Size => 6;
        public static byte[] SetSpeed(float speed)
        {
            return BuildCommand(LaserDeviceMessageType.SetSpeed, BitConverter.GetBytes(speed));
        }
        public static byte[] SetBacklashX(UInt16 offset)
        {
            return BuildCommand(LaserDeviceMessageType.SetBacklashX, BitConverter.GetBytes(offset));
        }
        public static byte[] SetBacklashY(UInt16 offset)
        {
            return BuildCommand(LaserDeviceMessageType.SetBacklashY, BitConverter.GetBytes(offset));
        }
        public static byte[] ResetOrigin()
        {
            return BuildCommand(LaserDeviceMessageType.ResetOrigin, []);
        }
        public static byte[] SetTarget(Int16[] target, byte intencity)
        {
            return BuildCommand(LaserDeviceMessageType.SetTarget, [.. GetTargetBytes(target), intencity]);
        }
        public static byte[] ClearBuffer()
        {
            return BuildCommand(LaserDeviceMessageType.ClearBuffer, []);
        }
        public static byte[] Echo()
        {
            return BuildCommand(LaserDeviceMessageType.Echo, []);
        }
        private static byte[] BuildCommand(LaserDeviceMessageType type, byte[] payload)
        {
            return [.. PadArray(payload, Size - 1), (byte)type];
        }
        private static byte[] PadArray(byte[] array, int length)
        {
            byte[] zeros = new byte[length - array.Length];
            Array.Clear(zeros, 0, length - array.Length);
            return [.. array, .. zeros];
        }
        private static byte[] GetTargetBytes(Int16[] target)
        {
            return [.. BitConverter.GetBytes(target[0]), .. BitConverter.GetBytes(target[1])];
        }
    }
}
