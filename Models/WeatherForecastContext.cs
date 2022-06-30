using Microsoft.EntityFrameworkCore;

namespace DockerizedTestAPI.Models
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options) : base(options)
        {

        }

        public DbSet<WeatherForecast> Weather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("WeatherSchema");
        }
    }
}
