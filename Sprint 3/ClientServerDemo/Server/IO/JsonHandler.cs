using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Server.IO
{
    public static class JsonHandler
    {
        public static async Task<T> LoadObject<T>(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);

            using (StreamReader reader = File.OpenText(savePath))
            {
                string rawData = await reader.ReadToEndAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(rawData));
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