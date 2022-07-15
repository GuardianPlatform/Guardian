using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Guardian.Domain.Entities;
using Guardian.Domain.Enum;
using Guardian.Infrastructure.Database.Seeds;
using Guardian.Service.Features.Game.Commands;
using Guardian.Test.Integration.WebFactory;
using Guardian.Test.Integration.WebFactory.UserClaims;
using Newtonsoft.Json;
using RestSharp;
using RichardSzalay.MockHttp;
using Xunit;

namespace Guardian.Test.Integration
{
    public class GameIntegrationTests : IClassFixture<GuardianWebApplicationFactory>
    {
        private readonly GuardianWebApplicationFactory _factory;

        private const string Root = "api";
        private const string Version = "v1.0";
        private const string Controller = "Games";

        public GameIntegrationTests(GuardianWebApplicationFactory factory)
        {
            _factory = factory;
            _factory.SetAuthorization(new AuthorizedUserClaims());
        }

        [Fact]
        public async Task GetAllGames_WhenContainsFourGames_ShouldReturnAllGames()
        {
            var client = _factory.CreateClient();

            var expected = JsonConvert.DeserializeObject<IEnumerable<Game>>($"[\r\n  {{\r\n    \"name\": \"Medal of Honor\",\r\n    \"description\": \"World War II shooter game.\",\r\n    \"author\": \"EA Games\",\r\n    \"license\": \"-\",\r\n    \"imageUrl\": \"https://image.ceneostatic.pl/data/products/10116336/i-medal-of-honor-digital.jpg\",\r\n    \"categories\": [\r\n      {{\r\n        \"categoryName\": \"Shooter\",\r\n        \"games\": [],\r\n        \"id\": 5\r\n      }}\r\n    ],\r\n    \"ratings\": null,\r\n    \"users\": null,\r\n    \"id\": 1\r\n  }},\r\n  {{\r\n    \"name\": \"Need for Speed\",\r\n    \"description\": \"Racing game with super fast cars.\",\r\n    \"author\": \"Ea Games\",\r\n    \"license\": \"-\",\r\n    \"imageUrl\": \"https://store-images.s-microsoft.com/image/apps.50422.14208985329983396.5216b3ae-22f3-400f-ad5d-45a1eb1686ba.6d0f6755-43ce-4902-8fe6-3939b2e29e4d?q=90&w=480&h=270\",\r\n    \"categories\": [\r\n      {{\r\n        \"categoryName\": \"Racing\",\r\n        \"games\": [],\r\n        \"id\": 4\r\n      }}\r\n    ],\r\n    \"ratings\": null,\r\n    \"users\": null,\r\n    \"id\": 2\r\n  }},\r\n  {{\r\n    \"name\": \"FIFA\",\r\n    \"description\": \"Football game.\",\r\n    \"author\": \"EA Sports\",\r\n    \"license\": \"-\",\r\n    \"imageUrl\": \"https://s2.tvp.pl/images2/2/7/8/uid_2787b3f1460aaecdbefc4818c064be541652200255438_width_1280_play_0_pos_0_gs_0_height_720_fifa-22-jest-aktualnie-najnowsza-wersja-gry-fot-ea-sports.jpg\",\r\n    \"categories\": [\r\n      {{\r\n        \"categoryName\": \"Sport\",\r\n        \"games\": [],\r\n        \"id\": 2\r\n      }}\r\n    ],\r\n    \"ratings\": null,\r\n    \"users\": null,\r\n    \"id\": 3\r\n  }},\r\n  {{\r\n    \"name\": \"Farming Simulator\",\r\n    \"description\": \"Farm simulator.\",\r\n    \"author\": \"CD Projekt\",\r\n    \"license\": \"-\",\r\n    \"imageUrl\": \"https://smartcdkeys.com/image/data/products/farming-simulator-22/cover/farming-simulator-22-smartcdkeys-cheap-cd-key-cover.jpg\",\r\n    \"categories\": [\r\n      {{\r\n        \"categoryName\": \"Simulator\",\r\n        \"games\": [],\r\n        \"id\": 3\r\n      }}\r\n    ],\r\n    \"ratings\": null,\r\n    \"users\": null,\r\n    \"id\": 4\r\n  }}\r\n]");

            var response = await client.GetFromJsonAsync<IEnumerable<Game>>($"{Root}/{Version}/{Controller}");

            response.Should().NotBeNullOrEmpty();
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetGameById_WhenAuthorizedAndContainGames_ShouldReturnAllGames()
        {
            var client = _factory.CreateClient();

            var response = await client.GetFromJsonAsync<Game>($"{Root}/{Version}/{Controller}/1");

            response.Should().NotBeNull();
            response.Id.Should().Be(1);
            response.Name.Should().Be("Medal of Honor");
        }

        [Fact]
        public async Task GetGameById_WhenUnauthorizedAndContainGames_ShouldReturn403Unauthorized()
        {
            _factory.SetAuthorization(new UnauthorizedUserClaims());
            var client = _factory.CreateClient();

            var response = client.GetFromJsonAsync<Game>($"{Root}/{Version}/{Controller}/1");

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => response);
            Assert.Contains("403", exception.Message);
        }

        [Fact]
        public async Task Create_WhenUnauthorized_ShouldReturn403Unauthorized()
        {
            _factory.SetAuthorization(new UnauthorizedUserClaims());
            var client = _factory.CreateClient();

            var game = new CreateGameCommand()
            {
                Author = "author",
                CategoryIds = new List<int>() { 1 },
                Description = "description",
                ImageUrl = "image",
                License = "license",
                Name = "name"
            };

            var response = client.PostAsJsonAsync($"{Root}/{Version}/{Controller}", game);

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => response);
            Assert.Contains("403", exception.Message);
        }

        [Fact]
        public async Task Create_WhenAuthorizedAndGameNotExist_ShouldCreateGame()
        {
            var client = _factory.CreateClient();

            var game = new CreateGameCommand()
            {
                Author = "author",
                CategoryIds = new List<int>() { 1 },
                Description = "description",
                ImageUrl = "image",
                License = "license",
                Name = "name"
            };

            var response = await client.PostAsJsonAsync($"{Root}/{Version}/{Controller}", game);
            var result = await response.Content.ReadFromJsonAsync<Game>();

            response.EnsureSuccessStatusCode();
            result.Id.Should().Be(5);
            result.Name.Should().Be("name");
            result.License.Should().Be("license");
            result.ImageUrl.Should().Be("image");
            result.Description.Should().Be("description");
            result.Author.Should().Be("author");
            result.Categories.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_WhenAuthorizedAndGameExist_ShouldThrowException()
        {
            var client = _factory.CreateClient();

            var game = new CreateGameCommand()
            {
                Author = "author",
                CategoryIds = new List<int>() { 1 },
                Description = "description",
                ImageUrl = "image",
                License = "license",
                Name = "Medal of Honor"
            };

            var response = await client.PostAsJsonAsync($"{Root}/{Version}/{Controller}", game);
            var result = await response.Content.ReadAsStringAsync();

            Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
            result.Should().Contain("exist");
        }
    }
}
