using System;

namespace HassDeviceWorkers
{
    public static class StringExtensions
    {
        public static byte[] FromHexStringToByteArray(this string str)
        {
            var bytes = new byte[str.Length / 2];

            for (int i = 0; i < str.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);

            return bytes;
        }
    }
}
