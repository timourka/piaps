namespace DesctopSheduleManager.Utilities
{
    public static class ApiClient
    {
        private static readonly HttpClient _client;

        static ApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(ConfigManager.Config.ApiBaseUrl)
            };
        }

        public static HttpClient Instance
        {
            get
            {
                // Обновляем заголовок sid перед каждым использованием
                if (_client.DefaultRequestHeaders.Contains("sid"))
                    _client.DefaultRequestHeaders.Remove("sid");

                if (!string.IsNullOrEmpty(SessionManager.SID))
                    _client.DefaultRequestHeaders.Add("sid", SessionManager.SID);

                return _client;
            }
        }
    }
}
