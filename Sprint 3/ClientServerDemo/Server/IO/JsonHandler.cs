using Newtonsoft.Json;
using Server.Crypto;
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

            string rawData = CryptoHandler.DecryptContent(File.ReadAllBytes(savePath));
            return JsonConvert.DeserializeObject<T>(rawData);
        }

        public static async Task<T> LoadObjectAsync<T>(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);

            if (!File.Exists(savePath))
                File.Create(savePath);

            string rawData = CryptoHandler.DecryptContent(File.ReadAllBytes(savePath));
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(rawData));
        }

        public static async Task<bool> SaveObject<T>(string filename, T classObject)
        {
            try
            {
                string saveFolder = Directory.GetCurrentDirectory();
                string savePath = Path.Combine(saveFolder, filename);

                string rawData = await Task.Run(() => JsonConvert.SerializeObject(classObject));
                await Task.Run(() => File.WriteAllBytes(savePath, CryptoHandler.EncryptContent(rawData)));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetFileRaw(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            return File.ReadAllText(savePath);
        }

        public static void SaveFile(string filename, byte[] content)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            File.WriteAllBytes(savePath, content);
        }
    }
}