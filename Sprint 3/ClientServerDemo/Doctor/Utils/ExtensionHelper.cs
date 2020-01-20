using System;
using System.Collections.Generic;

namespace Doctor.Utils
{
    public static class ExtensionHelper
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static string ToFullString(this string[] array)
        {
            string data = "";
            for (int i = 0; i < array.Length; i++)
                data += array[i];

            return data;
        }

        /// <summary>
        /// Converts the byte array to it's representive string
        /// </summary>
        public static string ToRepString(this byte[] array, string seperator = ",")
        {
            string data = "[";
            for (int i = 0; i < array.Length; i++)
            {
                data += array[i];

                if (i != array.Length - 1)
                    data += seperator;
            }

            return data + "]";
        }

        /// <summary>
        /// Parse the representive string to the byte array
        /// </summary>
        public static byte[] ParseRepString(this string repString)
        {
            List<byte> dataResult = new List<byte>();

            string[] data = repString.Replace("[", "").Replace("]", "").Split(',');
            for (int i = 0; i < data.Length; i++)
            {
                byte value;
                if (byte.TryParse(data[i], out value))
                    dataResult.Add(value);
            }
            return dataResult.ToArray();
        }
    }
}