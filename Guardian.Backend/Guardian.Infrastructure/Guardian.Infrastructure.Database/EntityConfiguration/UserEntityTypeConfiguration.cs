using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Infrastructure.Database.EntityConfiguration
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.Email)
                .HasMaxLength(320)
                .IsRequired();

            builder
                .Property(x => x.Login)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasMany(x => x.Ratings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
