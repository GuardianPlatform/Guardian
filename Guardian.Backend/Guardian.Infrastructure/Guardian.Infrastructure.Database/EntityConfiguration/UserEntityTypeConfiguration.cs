using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Persistence.EntityConfiguration
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
                .HasMany(x => x.Games)
                .WithMany(x => x.Users);

            builder
                .HasMany(x => x.Ratings)
                .WithOne(x => x.User);
        }
    }
}
