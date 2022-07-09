using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardian.Infrastructure.Database.EntityConfiguration
{
    public class GameUsersEntityTypeConfiguration : IEntityTypeConfiguration<GameUsers>
    {
        public void Configure(EntityTypeBuilder<GameUsers> builder)
        {
            builder.HasKey(x => new { x.GameId, x.UserId });

            builder.HasOne(x => x.User)
                .WithMany(x => x.GameUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Game)
                .WithMany(x => x.GameUsers)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
