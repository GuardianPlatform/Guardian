using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Entities;

namespace Guardian.Infrastructure.Database.Seeds
{
    public static class DefaultGames
    {
        public static List<Game> CreateDefaultGames()
        {
            return new List<Game>()
            {
                new Game
                {
                    Id = (int)DatabaseGeneratedOption.Identity,
                    Name = "Medal of Honor",
                    Description = "World War II shooter game.",
                    Author = "EA Games",
                    License = "-"
                   

                },
                new Game
                {
                    Id = (int)DatabaseGeneratedOption.Identity,
                    Name = "Need for Speed",
                    Description = "Racing game with super fast cars.",
                    Author = "Ea Games",
                    License = "-"
                    
                },
                new Game
                {
                    Id = (int)DatabaseGeneratedOption.Identity,
                    Name = "FIFA",
                    Description = "Football game.",
                    Author = "EA Sports",
                    License = "-"
                },
                new Game
                {
                    Id = (int)DatabaseGeneratedOption.Identity,
                    Name = "Farming Simulator",
                    Description = "Farm simulator.",
                    Author = "CD Projekt",
                    License = "-"

                }

            };

        }
    }
}
