namespace DataAccess
{
    using Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Models;

    public class SampleDbContext : DbContext
    {
        private ApplicationOptions _options;

        public SampleDbContext(IOptions<ApplicationOptions> options)
        {
            _options = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.ConnectionString);
        }

        public DbSet<Sample> Samples { get; set; }
    }
}