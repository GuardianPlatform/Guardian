using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardian.Infrastructure.Database.EntityConfiguration
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(5000)
                .IsRequired();

            builder
                .Property(x => x.Author)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.License)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasMany(x => x.Categories)
                .WithMany(x => x.Games);

            builder
                .HasMany(x => x.Ratings)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Cascade);
               
            builder
                .HasMany(x => x.Users)
                .WithMany(x => x.Games);

        }
    }
}
