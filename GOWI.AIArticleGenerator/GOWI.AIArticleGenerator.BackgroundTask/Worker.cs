namespace GOWI.AIArticleGenerator.BackgroundTask
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string? _connStringReadCheck;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
