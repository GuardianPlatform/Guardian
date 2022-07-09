using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardian.Infrastructure.Database.EntityConfiguration
{
    public class GameCategoryEntityTypeConfiguration : IEntityTypeConfiguration<GameCategory>
    {
        public void Configure(EntityTypeBuilder<GameCategory> builder)
        {
            builder.HasKey(x => new { x.GameId, x.CategoryId });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.GameCategories)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Game)
                .WithMany(x => x.GameCategories)
                .HasForeignKey(x => x.GameId);
        }
    }
}
