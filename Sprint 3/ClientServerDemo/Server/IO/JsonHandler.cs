using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Server.IO
{
    public static class JsonHandler
    {
        public static T LoadObject<T>(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);

            if (!File.Exists(savePath))
                File.Create(savePath);

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename));
        }

        public static async Task<T> LoadObjectAsync<T>(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);

            if (!File.Exists(savePath))
                File.Create(savePath);

            using (StreamReader reader = File.OpenText(savePath))
            {
                string rawData = await reader.ReadToEndAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(rawData));
            }
        }

        public static async Task<bool> SaveObject<T>(string filename, T classObject)
        {
            try
            {
                string saveFolder = Directory.GetCurrentDirectory();
                string savePath = Path.Combine(saveFolder, filename);

                string rawData = await Task.Run(() => JsonConvert.SerializeObject(classObject));
                await Task.Run(() => File.WriteAllText(savePath, rawData));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async void SaveFile(string filename, string content)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            await Task.Run(() => File.WriteAllText(savePath, content));
        }

        public static async void DeleteFile(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            await Task.Run(() => File.Delete(savePath));
        }

        public static bool FileExists(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            return File.Exists(savePath);
        }
    }
}