using Microsoft.EntityFrameworkCore;

namespace SeriesApi.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Series> Series { get; set; }
    }
}
