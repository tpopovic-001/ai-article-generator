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
    using Microsoft.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public class OpenAIService : IOpenAIService
    {
        private readonly string _apiKey;
        private readonly ILogger<OpenAIService> _logger;
        private Converter _converter;
        private IHttpClientFactory _httpClientFactory;
        private APIResponse _generatedArticle;

        public OpenAIService(IHttpClientFactory clientFactory,
                            ILogger<OpenAIService> logger)
        {
            _httpClientFactory = clientFactory;
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY");
            _logger = logger;
            _converter = Converter.ConverterInstance;
        }

        public async Task<APIResponse> GenerateArticlesAsync(string prompt, DTOTransaction transaction)
        {
            try
            {
                var serializedTransaction = _converter.SerializeToJSON(transaction);

                var completePrompt = $"Prompt: {prompt}. " +
                                    $"Here's the data: {serializedTransaction}";
                var request = new
                {
                    model = "gpt-3.5-turbo",
                    prompt = completePrompt,
                    max_tokens = 700,
                };
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

                var response = await httpClient.PostAsJsonAsync(
                        Environment.
                        GetEnvironmentVariable("OPENAI_END_POINT")
                                                        , request);

                var jsonResponse = await response.Content.
                                        ReadAsStringAsync();

                _generatedArticle = _converter.DeserializeJSON(jsonResponse);

                _logger.LogInformation(
                    "OpenAIService GenerateArticlesAsync method executed successfully at: {time}",
                    DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                "Error happened inside of OpenAIService GenerateArticlesAsync method at: {time}. " +
                "Error message: {error}",
                DateTimeOffset.Now, ex.Message);
            }

            return _generatedArticle;
        }
    }
}
