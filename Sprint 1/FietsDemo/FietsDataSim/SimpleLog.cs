using System.IO;

namespace FietsDataSim
{
    public static class SimpleLog
    {
        public static void Log(string fileName, string logLine)
        {
            string targetDirectory = Directory.GetCurrentDirectory() + @"\Data";
            string targetFile = targetDirectory + $@"\{fileName}";

            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            File.AppendAllLines(targetFile, new string[] { logLine });
        }
    }
}
