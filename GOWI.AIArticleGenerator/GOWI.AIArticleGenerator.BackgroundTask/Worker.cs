namespace GOWI.AIArticleGenerator.BackgroundTask
{
    using GOWI.AIArticleGenerator.BusinessLogicLayer;
    using GOWI.AIArticleGenerator.DataAccessLayer;
    using GOWI.AIArticleGenerator.ServiceLayer;
    using Microsoft.Extensions.Logging;
    using Microsoft.Identity.Client;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILogger<BusinessLogic> _businessLogicLogger;
        private ILogger<DataAccess> _dataAccessLogger;
        private readonly ILogger<OpenAIService> _serviceLayerlogger;
        private IMsalHttpClientFactory _httpClientFactory;
        private BusinessLogic _businessLogic;

        public Worker(ILogger<Worker> logger,
                      ILogger<BusinessLogic> loggerBLL,
                      ILogger<DataAccess> loggerDAL,
                      ILogger<OpenAIService> loggerSL,
                      IMsalHttpClientFactory factory)

        {
            _logger = logger;
            _businessLogicLogger = loggerBLL;
            _dataAccessLogger = loggerDAL;
            _serviceLayerlogger = loggerSL;
            _httpClientFactory = factory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _businessLogic = new BusinessLogic(_businessLogicLogger,
                                                _dataAccessLogger,
                                                _serviceLayerlogger,
                                                _httpClientFactory);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(
                    Microsoft.Extensions.Logging.LogLevel.Information))
                {
                    _logger.LogInformation(
                                    "Worker running at: {time}",
                                    DateTimeOffset.Now);
                }

                try
                {
                    var generatedArticles = await _businessLogic.
                                                            GetArticles();
                    _logger.LogInformation(
                                "Generated articles data: {articles}",
                                generatedArticles);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                await Task.Delay(6000, stoppingToken);
            }
        }
    }
}
