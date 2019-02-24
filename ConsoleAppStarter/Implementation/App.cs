namespace ConsoleAppStarter.Implementation
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using DataAccess;
    using Interfaces;
    using Microsoft.Extensions.Options;

    public class App : IApp
    {
        private readonly IOptions<ApplicationOptions> _options;
        private readonly SampleDbContext _dbContext;

        public App(IOptions<ApplicationOptions> options, SampleDbContext dbContext)
        {
            _options = options;
            _dbContext = dbContext;
        }

        public Task Run()
        {
            Console.WriteLine("Works");
            Console.WriteLine($"ConnectionString = {_options.Value.ConnectionString}");
            Console.WriteLine("We also ave DbContext in here!");
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }
}