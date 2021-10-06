namespace HassDeviceWorkers
{
    public static class ByteArrayExtensions
    {
        public static decimal ParseShortLE(this byte[] array, int indexFrom, decimal divider) =>
            (short)((array[indexFrom + 1] << 8) | array[indexFrom]) / divider;

        public static decimal ParseTwoBytesLE(this byte[] array, int indexFrom) =>
            (array[indexFrom + 1] << 8) | array[indexFrom];

        public static decimal ParseThreeBytesLE(this byte[] array, int indexFrom) =>
            (array[indexFrom + 2] << 16) | (array[indexFrom + 1] << 8) | array[indexFrom];

        public static decimal ParseFourBytesLE(this byte[] array, int indexFrom) =>
            (array[indexFrom + 3] << 24) | (array[indexFrom + 2] << 16) | (array[indexFrom + 1] << 8) | array[indexFrom];
    }
}
