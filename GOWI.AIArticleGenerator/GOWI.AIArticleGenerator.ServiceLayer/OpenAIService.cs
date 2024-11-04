namespace GOWI.AIArticleGenerator.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;
    using GOWI.AIArticleGenerator.ServiceLayer.Interfaces;
    using Microsoft.Extensions.Logging;

    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<OpenAIService> _logger;
        private Converter _converter;


        public OpenAIService(HttpClient httpClient, ILogger<OpenAIService> logger)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY");
            _logger = logger;
            _converter = Converter.ConverterInstance;
        }

        public async Task<string> GenerateArticles(string prompt, List<DTOTransaction> transactions)
        {
            var serializedData = _converter.SerializeToJSON(transactions);
            var completePrompt = $"Question: {prompt} Data for article creation: {serializedData}";

            var request = new
            {
                model = "gpt-3.5-turbo",
                prompt = completePrompt,
                max_tokens = 1024,
            };

            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

            var response = await _httpClient.PostAsJsonAsync(
                    Environment.GetEnvironmentVariable("OPENAI_END_POINT")
                                                                , request);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var deserializedData = _converter.DeserializeJSON(jsonResponse);
            _logger.LogInformation("All statements inside of GenerateArticles method" +
                                    "have been executed!");

            return deserializedData.Result.Choices.First().Text;
        }
    }
}
