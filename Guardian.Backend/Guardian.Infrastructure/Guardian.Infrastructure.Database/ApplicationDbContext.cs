using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database.EntityConfiguration;
using Guardian.Infrastructure.Database.Seeds.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        // This constructor is used of runit testing
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<GameUsers> GameUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityTypeConfiguration).Assembly);
            ApplicationDbContextSeed.Seed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer("DataSource=app.db");
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
