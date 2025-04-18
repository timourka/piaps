namespace DesctopSheduleManager.Utilities
{
    public static class ApiClientFactory
    {
        public static HttpClient Create()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ConfigManager.Config.ApiBaseUrl)
            };

            if (!string.IsNullOrEmpty(SessionManager.SID))
            {
                client.DefaultRequestHeaders.Add("sid", SessionManager.SID);
            }

            return client;
        }
    }
}
