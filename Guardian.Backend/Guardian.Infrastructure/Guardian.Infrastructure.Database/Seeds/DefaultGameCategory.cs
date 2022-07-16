using System.Collections.Generic;
using Guardian.Domain.Entities;

namespace Guardian.Infrastructure.Database.Seeds
{
    public static class DefaultGameCategory
    {
        public static List<GameCategory> CreateDefaultGameCategories()
        {
            return new List<GameCategory>
            {
                new GameCategory
                {
                    GameId = 1,
                    CategoryId = 5
                },
                new GameCategory
                {
                    GameId = 2,
                    CategoryId = 4
                },
                new GameCategory
                {
                    GameId = 3,
                    CategoryId = 2
                },
                new GameCategory
                {
                    GameId = 4,
                    CategoryId = 3
                }
            };
        }
    }
}
