using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace VrDemo.Utils
{
    public static class ExtensionHelper
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static string ToSimpleString(this float[] data)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            for (int i = 0; i < data.Length; i++)
            {
                string t = data[i].ToString();
                t = t.Replace(',', '.');

                sb.Append(t);

                if (i != data.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");

            return sb.ToString();
        }

        public static string BeautifyJson(this string data)
        {
            return JObject.Parse(data).ToString();
        }
        
        public static string MinifyJson(this string data)
        {
            return data.Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }
    }
}