namespace WordPredictionBackend.Clients
{
    public class WebServiceClient
    {
        private readonly HttpClient _httpClient;

        public WebServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string[]> GetPredictionsFromText(string text, string language = "da-DK")
        {
            return await _httpClient.GetFromJsonAsync<string[]>($"?locale={language}&text={text}") ?? Array.Empty<string>();
        }


    }
}
