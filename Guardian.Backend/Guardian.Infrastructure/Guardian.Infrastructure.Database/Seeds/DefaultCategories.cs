using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Entities;

namespace Guardian.Infrastructure.Database.Seeds
{
    public static class DefaultCategories
    {
        public static List<Category> CreateCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    CategoryName = "Strategy"
                },
                new Category()
                {
                    Id = 2,
                    CategoryName = "Sport"
                },
                new Category()
                {
                    Id = 3,
                    CategoryName = "Simulator"
                },
                new Category()
                {
                    Id = 4,
                    CategoryName = "Racing"
                },
                new Category()
                {
                    Id = 5,
                    CategoryName = "Shooter"
                }
            };
        }
    };
}
