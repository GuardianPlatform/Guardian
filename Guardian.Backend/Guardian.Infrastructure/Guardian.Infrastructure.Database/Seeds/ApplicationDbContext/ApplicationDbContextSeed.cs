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
        public static void Seed(ModelBuilder modelBuilder)
        {
            CreateCategory(modelBuilder);
            CreateGames(modelBuilder);
            CreateCategoryGame(modelBuilder);
        }

        private static void CreateCategory(ModelBuilder modelBuilder)
        {
            var categories = DefaultCategories.CreateDefaultCategories();
            modelBuilder.Entity<Category>().HasData(categories);
        }

        private static void CreateGames(ModelBuilder modelBuilder)
        {
            var games = DefaultGames.CreateDefaultGames();
            modelBuilder.Entity<Game>().HasData(games);
        }

        private static void CreateCategoryGame(ModelBuilder modelBuilder)
        {
            var gameCategories = DefaultGameCategory.CreateDefaultGameCategories();
            modelBuilder.Entity<GameCategory>().HasData(gameCategories);
        }

    }
}
