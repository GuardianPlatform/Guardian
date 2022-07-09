using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardian.Infrastructure.Database.EntityConfiguration
{
    public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder
               .Property(x => x.Score)
               .IsRequired();

            builder.Property(x => x.Comment)
                .HasMaxLength(1000);

            builder
                .HasOne(x => x.User)
                .WithMany(y => y.Ratings)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder
                 .HasOne(x => x.Game)
                 .WithMany(x => x.Ratings)
                 .HasForeignKey(x => x.GameId)
                 .IsRequired();

        }
    }
}
