using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VrDemo.IO
{
    public static class JsonLoader
    {
        public static async Task<Tuple<string, JObject>> LoadSendable(string name)
        {
            Tuple<string, JObject> data = await Task.Run(() =>
            {
                string path = Path.Combine(FolderData.SENDABLES_FOLDER, $"{name}.json").Remove(0, 1).Replace("%20", " ");

                string jsonRaw = File.ReadAllText(path);
                JObject jsonObject = JObject.Parse(jsonRaw);

                return new Tuple<string, JObject>(jsonRaw, jsonObject);
            });
            return data;
        }
    }
}