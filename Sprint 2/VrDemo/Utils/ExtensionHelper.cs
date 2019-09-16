using Newtonsoft.Json.Linq;
using System;
using System.Text;
using VrDemo.Data;

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

        public static Node ToNode(this JObject data)
        {
            return new Node(data.SelectToken("data.data.data.name").ToString(), data.SelectToken("data.data.data.uuid").ToString());
        }
        public static bool IsNodeResponseOk(this JObject data)
        {
            return data.SelectToken("data.data.status").ToString() == "ok" ? true : false;
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