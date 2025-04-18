using System.Text.Json;

namespace DesctopSheduleManager.Utilities
{
    public static class ConfigManager
    {
        private static AppConfig _config;

        public static AppConfig Config
        {
            get
            {
                if (_config == null)
                    LoadConfig();
                return _config;
            }
        }

        private static void LoadConfig()
        {
            var configText = File.ReadAllText("config.json");
            _config = JsonSerializer.Deserialize<AppConfig>(configText);
        }
    }
}
