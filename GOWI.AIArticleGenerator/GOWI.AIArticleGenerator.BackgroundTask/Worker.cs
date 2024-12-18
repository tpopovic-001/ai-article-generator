namespace GOWI.AIArticleGenerator.BackgroundTask
{
    using GOWI.AIArticleGenerator.BusinessLogicLayer;
    using GOWI.AIArticleGenerator.DataAccessLayer;
    using GOWI.AIArticleGenerator.ServiceLayer;
    using Microsoft.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILogger<BusinessLogic> _businessLogicLogger;
        private ILogger<DataAccess> _dataAccessLogger;
        private readonly ILogger<OpenAIService> _serviceLayerlogger;
        private BusinessLogic _businessLogic;
        private IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger,
                      ILogger<BusinessLogic> loggerBLL,
                      ILogger<DataAccess> loggerDAL,
                      ILogger<OpenAIService> loggerSL,
                      IHttpClientFactory clientFactory)

        {
            _logger = logger;
            _businessLogicLogger = loggerBLL;
            _dataAccessLogger = loggerDAL;
            _serviceLayerlogger = loggerSL;
            _httpClientFactory = clientFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _businessLogic = new BusinessLogic(_businessLogicLogger,
                                                _dataAccessLogger,
                                                _serviceLayerlogger,
                                                _httpClientFactory
                                                );

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
                                                            GetArticlesAsync();

                    var formattedArticles = _businessLogic.
                                          FormatArticles(generatedArticles);

                    if (formattedArticles.Count != 0)
                    {
                        _businessLogic.SaveFormattedArticlesAsync(formattedArticles);
                    }

                    _logger.LogInformation(
                        "Worker ExecuteAsync method executed successfully at: {time}",
                        DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        "Error happened inside of Worker ExecuteAsync method at: {time}. Error message: {error}",
                        DateTimeOffset.Now, ex.Message);
                }

                await Task.Delay(0, stoppingToken);
            }
        }
    }
}
