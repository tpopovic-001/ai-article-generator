namespace GOWI.AIArticleGenerator.BackgroundTask
{
    using DotNetEnv;
    using GOWI.AIArticleGenerator.BackgroundTask;
    using GOWI.AIArticleGenerator.BackgroundTask.Entities.Context;
    using Microsoft.EntityFrameworkCore;

    public class Program()
    {
        public static void Main(string[] args)
        {
            Env.Load("./.env.env");
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Configuration.AddEnvironmentVariables();
            builder.Services.AddDbContext<DevAfjPp18032024Context>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

            var host = builder.Build();
            host.Run();
        }
    }
}
