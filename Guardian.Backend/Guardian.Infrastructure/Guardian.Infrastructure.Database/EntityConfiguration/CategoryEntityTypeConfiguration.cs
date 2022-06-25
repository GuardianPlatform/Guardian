using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Persistence.EntityConfiguration
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(x => x.CategoryName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(x => x.Games)
                .WithMany(x => x.Categories);
                
               
        }
    }
}
