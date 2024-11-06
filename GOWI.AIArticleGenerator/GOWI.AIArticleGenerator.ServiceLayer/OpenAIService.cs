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
    using Microsoft.Identity.Client;

    public class OpenAIService : IOpenAIService
    {
        private readonly string _apiKey;
        private readonly ILogger<OpenAIService> _logger;
        private readonly IMsalHttpClientFactory _httpClientFactory;
        private Converter _converter;

        public OpenAIService(IMsalHttpClientFactory factory,
                            ILogger<OpenAIService> logger)
        {
            _httpClientFactory = factory;
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY");
            _logger = logger;
            _converter = Converter.ConverterInstance;
        }

        public async Task<string> GenerateArticles(string prompt,
                                List<DTOTransaction> transactions)
        {
            Task<APIResponse> deserializedData = null;

            try
            {
                var serializedData = _converter.
                                    SerializeToJSON(transactions);
                var completePrompt = $"Prompt: {prompt} " +
                                    $"Data: {serializedData}";
                var request = new
                {
                    model = "gpt-3.5-turbo",
                    prompt = completePrompt,
                    max_tokens = 3000,
                };
                var httpClient = _httpClientFactory.GetHttpClient();
                httpClient.DefaultRequestHeaders.Add("api-key",
                            Environment.GetEnvironmentVariable("OPENAI_KEY"));

                var response = await httpClient.PostAsJsonAsync(
                        Environment.
                        GetEnvironmentVariable("OPENAI_END_POINT")
                                                        , request);

                var jsonResponse = await response.Content.
                                        ReadAsStringAsync();
                deserializedData = _converter.DeserializeJSON(jsonResponse);

                _logger.LogInformation(
                    "OpenAIService GenerateArticles method " +
                    "completed successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "OpenAIService GenerateArticles method error: {error}",
                    ex.Message);
            }

            return deserializedData.Result.Choices.First().Text;
        }
    }
}
