using Newtonsoft.Json.Linq;
using System;

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

        public static string BeautifyJson(this string data)
        {
            return JObject.Parse(data).ToString();
        }
        
        public static string ToCleanPacketString(this string data)
        {
            return data.Replace("\n", "").Replace("\r", "").Replace(" ", "");
        }
    }
}