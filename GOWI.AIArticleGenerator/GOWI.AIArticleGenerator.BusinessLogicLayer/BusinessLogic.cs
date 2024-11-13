namespace GOWI.AIArticleGenerator.BusinessLogicLayer
{
    using System.Text.RegularExpressions;
    using GOWI.AIArticleGenerator.BusinessLogicLayer.Interfaces;
    using GOWI.AIArticleGenerator.DataAccessLayer;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;
    using Microsoft.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;
        private ILogger<DataAccess> _dataAccessLogger;
        private readonly ILogger<OpenAIService> _serviceLayerlogger;
        private DataAccess _dataAccess;
        private OpenAIService _openAIService;
        private IHttpClientFactory _httpClientFactory;
        private List<Choice> _generatedArticles;

        public BusinessLogic(ILogger<BusinessLogic> logger,
                            ILogger<DataAccess> loggerDAL,
                            ILogger<OpenAIService> loggerSL,
                            IHttpClientFactory clientFactory
                           )
        {
            _logger = logger;
            _dataAccessLogger = loggerDAL;
            _serviceLayerlogger = loggerSL;
            _httpClientFactory = clientFactory;
        }

        public async Task<List<Choice>> GetArticlesAsync()
        {
            try
            {
                string prompt = "Always create an article in this format:" +
                    "\r\n Article title \r\n" +
                    "Article short description: \r\n" +
                    "Article full description:\r\n" +
                    "For this article creation use provided data only!";

                _dataAccess = new DataAccess(_dataAccessLogger);
                _openAIService = new OpenAIService(_httpClientFactory,
                                    _serviceLayerlogger);

                var transactions = await _dataAccess.GetTransactionsAsync();
                _generatedArticles = new List<Choice>();

                foreach (var transaction in transactions)
                {
                    var generatedArticle = await _openAIService.GenerateArticlesAsync
                                    (prompt, transaction);

                    var articleContent = generatedArticle.Choices
                                                .Select(s => s.Text)
                                                .FirstOrDefault();

                    _logger.LogInformation("Generated article:\r\n {article}\r\n" +
                        "at: {time}", articleContent, DateTimeOffset.Now);

                    if (articleContent != null)
                    {
                        var choiceObject = new Choice
                        {
                            Text = articleContent,
                            Index = 0,
                            Logprobs = 0,
                            FinishReason = string.Empty,
                            TransactionId = transaction.TransactionId,
                        };
                        _generatedArticles.Add(choiceObject);
                    }
                }

                _logger.LogInformation("BusinessLogic GetArticlesAsync method executed successfully " +
                    "at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error happened inside of BusinessLogic GetArticlesAsync method at: {time}." +
                                "Error message: {error}", DateTimeOffset.Now, ex.Message);
            }

            return _generatedArticles;
        }

        public List<DTOArticle> FormatArticles(List<Choice> articles)
        {
            var titlePattern = @"(?<=Article title:)(.*?)(?=\n)";
            var shortDescriptionPattern = @"(?<=Article short description:)(.*?)(?=\n)";
            var fullDescriptionPattern = @"(?<=Article full description:)([\s\S]*?)(?=\n)";

            List<DTOArticle> formattedArticles = new List<DTOArticle>();

            try
            {
                foreach (var article in articles)
                {
                    if (Regex.IsMatch(article.Text, titlePattern) == true &&
                        Regex.IsMatch(article.Text, shortDescriptionPattern) == true &&
                        Regex.IsMatch(article.Text, fullDescriptionPattern) == true
                    )
                    {
                        var formattedArticle = new DTOArticle
                        {
                            Title = Regex.Match(article.Text, titlePattern).Value,
                            ShortDescription = Regex.Match(article.Text,
                                                        shortDescriptionPattern).Value,
                            FullDescription = Regex.Match(article.Text,
                                                        fullDescriptionPattern).Value,
                            TransactionId = article.TransactionId,
                        };

                        formattedArticles.Add(formattedArticle);
                    }
                    else
                    {
                        continue;
                    }
                }

                _logger.LogInformation("BusinessLogic FormatArticles method executed successfully " +
                                        "at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error happened inside of BusinessLogic FormatArticles method at: {time}." +
                "Error message: {error}", DateTimeOffset.Now, ex.Message);
            }

            return formattedArticles;
        }

        public async Task SaveFormattedArticlesAsync(List<DTOArticle> articles)
        {
            try
            {
                _dataAccess = new DataAccess(_dataAccessLogger);
                await _dataAccess.SaveFormattedArticlesAsync(articles);

                _logger.LogInformation("BusinessLogic SaveFormattedArticlesAsync method executed " +
                        "successfully at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {

                _logger.LogError("Error happened inside of BusinessLogic SaveFormattedArticlesAsync method at: {time}." +
                    "Error message: {error}", DateTimeOffset.Now, ex.Message);
            }
        }
    }
}
