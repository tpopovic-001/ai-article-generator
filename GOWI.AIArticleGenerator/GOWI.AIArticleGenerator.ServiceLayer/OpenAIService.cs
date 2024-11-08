﻿namespace GOWI.AIArticleGenerator.ServiceLayer
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
        private string _deserializedArticle;

        public OpenAIService(IHttpClientFactory clientFactory,
                            ILogger<OpenAIService> logger)
        {
            _httpClientFactory = clientFactory;
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_KEY");
            _logger = logger;
            _converter = Converter.ConverterInstance;
        }

        public async Task<string> GenerateArticle(string prompt, DTOTransaction transaction)
        {
            try
            {
                var serializedTransaction = await _converter.SerializeToJSON(transaction);

                var completePrompt = $"Prompt: {prompt}. " +
                                    $"Data: {serializedTransaction}";
                var request = new
                {
                    model = "gpt-3.5-turbo",
                    prompt = completePrompt,
                    max_tokens = 500,
                };
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

                var response = await httpClient.PostAsJsonAsync(
                        Environment.
                        GetEnvironmentVariable("OPENAI_END_POINT")
                                                        , request);

                var jsonResponse = await response.Content.
                                        ReadAsStringAsync();

                _deserializedArticle = jsonResponse;

                _logger.LogInformation(
                    "OpenAIService GenerateArticles method executed successfully at: {time}",
                    DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                "Error happened inside of OpenAIService GenerateArticles method at: {time}. " +
                "Error message: {error}",
                DateTimeOffset.Now, ex.Message);
            }

            return await Task.FromResult(_deserializedArticle);
        }
    }
}
