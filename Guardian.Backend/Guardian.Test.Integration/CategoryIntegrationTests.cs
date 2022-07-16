using Guardian.Domain.Entities;
using Guardian.Test.Integration.WebFactory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Guardian.Test.Integration
{
    [Collection("IntegrationTests")]
    public class CategoryIntegrationTests
    {
        private const string Root = "api";
        private const string Version = "v1.0";
        private const string Controller = "Categories";

        public CategoryIntegrationTests()
        {

        }

        [Fact]
        public async Task GetAllCategories_WhenExistFiveCategories_ShouldReturnAllCategoriesWithoutGames()
        {
            var factory = GuardianWebApplicationFactory.Unauthorized();
            var client = factory.CreateClient();

            var expected = JsonConvert.DeserializeObject<IEnumerable<Game>>($"[\r\n {{\r\n    \"categoryName\": \"Strategy\",\r\n    \"games\": null,\r\n    \"id\": 1\r\n  }},\r\n  {{\r\n    \"categoryName\": \"Sport\",\r\n    \"games\": null,\r\n    \"id\": 2\r\n  }},\r\n  {{\r\n    \"categoryName\": \"Simulator\",\r\n    \"games\": null,\r\n    \"id\": 3\r\n  }},\r\n  {{\r\n    \"categoryName\": \"Racing\",\r\n    \"games\": null,\r\n    \"id\": 4\r\n  }},\r\n  {{\r\n    \"categoryName\": \"Shooter\",\r\n    \"games\": null,\r\n    \"id\": 5\r\n  }}\r\n]");
            var response = await client.GetFromJsonAsync<IEnumerable<Game>>($"{Root}/{Version}/{Controller}");

            response.Should().NotBeNullOrEmpty();
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetGamesByCategoryById_WhenCategoryContainGame_ShouldReturnGamesForGivenCategory()
        {
            var factory = GuardianWebApplicationFactory.Unauthorized();
            var client = factory.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<Game>>($"{Root}/{Version}/{Controller}/games/sport");
            var result = response.FirstOrDefault();

            response.Should().NotBeNull();
            result.Author.Should().Be("EA Sports");
            result.Name.Should().Be("FIFA");
            result.Description.Should().Be("Football game.");
            result.ImageUrl.Should().NotBeNullOrEmpty().And.Contain("http");
        }

        [Fact]
        public async Task GetGamesByCategoryById_WhenCategoryIsEmpty_ShouldReturnEmptyList()
        {
            var factory = GuardianWebApplicationFactory.Unauthorized();
            var client = factory.CreateClient();

            var response = await client.GetFromJsonAsync<IEnumerable<Game>>($"{Root}/{Version}/{Controller}/games/strategy");

            response.Should().BeEmpty();
        }
    }
}
