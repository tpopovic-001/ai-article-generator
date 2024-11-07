namespace GOWI.AIArticleGenerator.BusinessLogicLayer
{
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
        private List<string> _apiResponse;

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

        public async Task<List<string>> GetArticles()
        {
            try
            {
                string prompt = "You are professional journalist. Make an article in format: title," +
                    "short description, full description. Here's the data to work with: ";

                _dataAccess = new DataAccess(_dataAccessLogger);
                _openAIService = new OpenAIService(_httpClientFactory,
                                    _serviceLayerlogger);

                var transactions = await _dataAccess.GetTransactions();
                _apiResponse = new List<string>();

                foreach (var transaction in transactions)
                {
                    var generatedArticle = await _openAIService.GenerateArticle
                                    (prompt, transaction);

                    _apiResponse.Add(generatedArticle);
                }

                _logger.LogInformation("BusinessLogic GetArticles method executed successfully " +
                    "at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error happened inside of BusinessLogic GetArticles method at: {time}." +
                                "Error message: {error}", DateTimeOffset.Now, ex.Message);
            }

            return await Task.FromResult(_apiResponse);
        }
    }
}
