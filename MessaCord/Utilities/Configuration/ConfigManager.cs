using System.IO;
using Newtonsoft.Json;

namespace MessaCord.Utilities.Configuration
{
    public class ConfigManager
    {
        private string _configFilePath = "config.json";
        private string _configFolderPath = "Ressources/";
        public Config Config { get; }
        public ConfigManager()
        {
            using (StreamReader r = new StreamReader(_configFolderPath + _configFilePath))
            {
                string json = r.ReadToEnd();
                Config = JsonConvert.DeserializeObject<Config>(json);
            }
        }
        public ConfigManager(string configFilePath)
        {
            using (StreamReader r = new StreamReader(_configFolderPath + _configFilePath))
            {
                string json = r.ReadToEnd();
                Config = JsonConvert.DeserializeObject<Config>(json);
            }
        }
    }
}