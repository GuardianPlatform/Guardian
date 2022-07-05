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
               .HasMaxLength(1000)
               .IsRequired();

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(y => y.Ratings);

            builder
               .Property(x => x.GameId)
               .IsRequired();


            builder
                 .HasOne(x => x.Game)
                 .WithMany(x => x.Ratings);

        }
    }
}
