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
                    Id = 1,
                    Name = "Medal of Honor",
                    Description = "World War II shooter game.",
                    Author = "EA Games",
                    License = "-",
                    ImageUrl = "https://image.ceneostatic.pl/data/products/10116336/i-medal-of-honor-digital.jpg"


                },
                new Game
                {
                    Id = 2,
                    Name = "Need for Speed",
                    Description = "Racing game with super fast cars.",
                    Author = "Ea Games",
                    License = "-",
                    ImageUrl = "https://store-images.s-microsoft.com/image/apps.50422.14208985329983396.5216b3ae-22f3-400f-ad5d-45a1eb1686ba.6d0f6755-43ce-4902-8fe6-3939b2e29e4d?q=90&w=480&h=270"

                },
                new Game
                {
                    Id = 3,
                    Name = "FIFA",
                    Description = "Football game.",
                    Author = "EA Sports",
                    License = "-",
                    ImageUrl = "https://s2.tvp.pl/images2/2/7/8/uid_2787b3f1460aaecdbefc4818c064be541652200255438_width_1280_play_0_pos_0_gs_0_height_720_fifa-22-jest-aktualnie-najnowsza-wersja-gry-fot-ea-sports.jpg"
                },
                new Game
                {
                    Id = 4,
                    Name = "Farming Simulator",
                    Description = "Farm simulator.",
                    Author = "CD Projekt",
                    License = "-",
                    ImageUrl = "https://smartcdkeys.com/image/data/products/farming-simulator-22/cover/farming-simulator-22-smartcdkeys-cheap-cd-key-cover.jpg"
                }

            };

        }
    }
}
