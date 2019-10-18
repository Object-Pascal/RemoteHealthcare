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
    }
}