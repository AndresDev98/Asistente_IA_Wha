using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Fynex.Service.WhatsApp.Api.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _apiKey = config["OpenAI:ApiKey"] ?? throw new ArgumentNullException("OpenAI:ApiKey not found in configuration");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> SendMessageAsync(string userMessage)
        {
            var request = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "system", content = "Eres un asistente experto en seguros. Responde de forma clara y precisa." },
                    new { role = "user", content = userMessage }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI request failed: {error}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseBody);
            return result.choices[0].message.content.ToString();
        }
    }
}
