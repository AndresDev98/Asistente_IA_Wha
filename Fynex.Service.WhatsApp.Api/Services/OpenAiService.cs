using Fynex.Service.WhatsApp.Api.Utils;
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
            _apiKey = config["OpenAI:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetReplyAsync(string userMessage)
        {
            var request = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = PromptBuilder.GetBasePrompt() },
                new { role = "user", content = userMessage }
            }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var body = await res.Content.ReadAsStringAsync();
            dynamic parsed = JsonConvert.DeserializeObject(body);
            return parsed.choices[0].message.content.ToString();
        }
    }

}
