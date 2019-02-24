namespace ConsoleAppStarter
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Common;
    using DataAccess;
    using Implementation;
    using Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = BuildServices();

            var app = services.GetService<IApp>();

            await app.Run();
        }

        private static IServiceProvider BuildServices()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddTransient<IApp, App>();

            ConfigureDbContext(services);
            CreateAndBindOptions<ApplicationOptions>(config, services);

            return services.BuildServiceProvider();
        }

        private static void ConfigureDbContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<SampleDbContext>();
        }

        private static T CreateAndBindOptions<T>(IConfigurationRoot configuration, IServiceCollection services, string optionsKey = null) where T : class, new()
        {
            var optionsObject = new T();
            var optionsWrapper = Options.Create(optionsObject);

            if (string.IsNullOrWhiteSpace(optionsKey))
            {
                configuration.Bind(optionsObject);
            }
            else
            {
                configuration.Bind(optionsKey, optionsObject);
            }

            services.AddTransient(c => optionsWrapper);

            return optionsObject;
        }
    }
}
