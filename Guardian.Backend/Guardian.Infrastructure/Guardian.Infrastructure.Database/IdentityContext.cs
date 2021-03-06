using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database.EntityConfiguration;
using Guardian.Infrastructure.Database.Seeds.ApplicationDbContext;
using Guardian.Infrastructure.Database.Seeds.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Infrastructure.Database
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            modelBuilder.ApplyConfiguration(new GameUsersEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GameCategoryEntityTypeConfiguration());

            IdentityContextSeed.Seed(modelBuilder);
            
        }

        public DbSet<GameUsers> GameUsers { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        
    }
}
