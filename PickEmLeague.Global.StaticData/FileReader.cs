using System;
using System.IO;
using System.Reflection;

namespace PickEmLeague.Global.StaticData
{
    public static class FileReader
    {
        public static string ReadFile(string fileKey)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{fileKey}.json");

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }

            return "";
        }

        public static void WriteToFile(string fileKey, string data)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{fileKey}.json");

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(data);
            }
        }

    }
}
