namespace GOWI.AIArticleGenerator.BusinessLogicLayer
{
    using GOWI.AIArticleGenerator.BusinessLogicLayer.Interfaces;
    using GOWI.AIArticleGenerator.DataAccessLayer;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;
    using Microsoft.Extensions.Logging;
    using Microsoft.Identity.Client;


    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;
        private ILogger<DataAccess> _dataAccessLogger;
        private readonly ILogger<OpenAIService> _serviceLayerlogger;
        private DataAccess _dataAccess;
        private OpenAIService _openAIService;
        private IMsalHttpClientFactory _httpClientFactory;

        public BusinessLogic(ILogger<BusinessLogic> logger,
                            ILogger<DataAccess> loggerDAL,
                            ILogger<OpenAIService> loggerSL,
                            IMsalHttpClientFactory factory)
        {
            _logger = logger;
            _dataAccessLogger = loggerDAL;
            _serviceLayerlogger = loggerSL;
            _httpClientFactory = factory;
        }

        public async Task<string> GetArticles()
        {
            _dataAccess = new DataAccess(_dataAccessLogger);
            var transactionData = await _dataAccess.GetTransactions();

            string prompt = "Generate an article for each transaction." +
                            "It must be created in this manner: title, " +
                            "short description," +
                            " and full description!";

            _openAIService = new OpenAIService(_httpClientFactory,
                                                _serviceLayerlogger);

            var generatedArticles = await _openAIService.GenerateArticles
                                                    (prompt, transactionData);

            return await Task.FromResult(generatedArticles);
        }
    }
}
