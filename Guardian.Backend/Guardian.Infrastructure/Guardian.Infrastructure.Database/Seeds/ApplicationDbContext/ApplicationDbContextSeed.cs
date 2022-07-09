using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.Infrastructure.Database.Seeds.ApplicationDbContext
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateCategory(modelBuilder);
        }

        private static void CreateCategory(ModelBuilder modelBuilder)
        {
            var categories = DefaultCategories.CreateCategories();
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
