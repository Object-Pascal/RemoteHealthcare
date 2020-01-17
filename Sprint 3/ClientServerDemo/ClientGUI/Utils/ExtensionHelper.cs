using System;

namespace ClientGUI.Utils
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
    }
}