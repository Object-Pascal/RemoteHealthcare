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

        #region obsolete stuff
        public static void SaveTestEncryptedData()
        {
            byte[] c = Crypto.CryptoHandler.EncryptContent(JsonHandler.GetFileRaw("doctorData_old.json"));
            string data = System.Text.Encoding.UTF8.GetString(c);

            JsonHandler.SaveFile("doctorData.json", c);
        }

        public static string GetFileRaw(string filename)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            return File.ReadAllText(savePath);
        }

        public static async void SaveFile(string filename, string content)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            await Task.Run(() => File.WriteAllText(savePath, content));
        }

        public static void SaveFile(string filename, byte[] content)
        {
            string saveFolder = Directory.GetCurrentDirectory();
            string savePath = Path.Combine(saveFolder, filename);
            File.WriteAllBytes(savePath, content);
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
        #endregion
    }
}