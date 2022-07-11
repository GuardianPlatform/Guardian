using Microsoft.EntityFrameworkCore;
using Guardian.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;
using Guardian.Domain.Models;
using System.Linq;
using Guardian.Infrastructure.Database;
using Guardian.Service.Features.Game.Queries;
using System;
using Guardian.Service.Features.Game.Commands;

namespace Guardian.Test.Unit.Persistence
{

    public class GameControllerTests
    {
        
        [Test]
        public async Task PaginationTest_FirstPageThreeItems()
        {
               DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        var context = new ApplicationDbContext(dbContextOptions);

            context.Add(new Game { Name = "gra1", Description = "dsc1", Author = "aa1" });
            context.Add(new Game { Name = "gra2", Description = "dsc2", Author = "aa2" });
            context.Add(new Game { Name = "gra3", Description = "dsc3", Author = "aa3" });
            context.Add(new Game { Name = "gra4", Description = "dsc4", Author = "aa4" });
            context.SaveChanges();
            const int MaxGamescount = 3;
            var request = new GetAllGamesQuery()
            {
                Pagination = new PagiantionModel(0, MaxGamescount)
            };

            var service = new GetAllGamesQuery.GetAllGamesQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(MaxGamescount, result.Count());
        }

        [Test]
        public async Task PaginationTest_SecondPageOneItem()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            var context = new ApplicationDbContext(dbContextOptions);

            context.Add(new Game { Name = "gra1", Description = "dsc1", Author = "aa1" });
            context.Add(new Game { Name = "gra2", Description = "dsc2", Author = "aa2" });
            context.Add(new Game { Name = "gra3", Description = "dsc3", Author = "aa3" });
            context.Add(new Game { Name = "gra4", Description = "dsc4", Author = "aa4" });
            context.SaveChanges();
            const int MaxGamescount = 3;
            var request = new GetAllGamesQuery()
            {
                Pagination = new PagiantionModel(1, MaxGamescount)
            };

            var service = new GetAllGamesQuery.GetAllGamesQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task PaginationTest_OneItemFirstPage()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            var context = new ApplicationDbContext(dbContextOptions);

            context.Add(new Game { Name = "gra1", Description = "dsc1", Author = "aa1" });

            context.SaveChanges();
            const int MaxGamescount = 3;
            var request = new GetAllGamesQuery()
            {
                Pagination = new PagiantionModel(0, MaxGamescount)
            };

            var service = new GetAllGamesQuery.GetAllGamesQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task PaginationTest_OneItemSecondPage()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            var context = new ApplicationDbContext(dbContextOptions);

            context.Add(new Game { Name = "gra1", Description = "dsc1", Author = "aa1" });
            context.SaveChanges();
            const int MaxGamescount = 3;
            var request = new GetAllGamesQuery()
            {
                Pagination = new PagiantionModel(1, MaxGamescount)
            };

            var service = new GetAllGamesQuery.GetAllGamesQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetGameByIdTest()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            var context = new ApplicationDbContext(dbContextOptions);

            context.Add(new Game { Name = "gra1", Description = "dsc1", Author = "aa1" });
            context.Add(new Game { Name = "gra2", Description = "dsc2", Author = "aa2" });
            context.Add(new Game { Name = "gra3", Description = "dsc3", Author = "aa3" });
            context.Add(new Game { Name = "gra4", Description = "dsc4", Author = "aa4" });
            context.SaveChanges();

            var request = new GetGameQuery()
            {
                Id = 3
            };
            var ListOfGames = context.Games.ToList();
            var service = new GetGameQuery.GetGameQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(ListOfGames[2].Id, result.Id);
        }

        [Test]
        public async Task CreateGameQueryTest_ReturnId()
        {
            DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            var context = new ApplicationDbContext(dbContextOptions);

            context.Add(new Game { Name = "gra1", Description = "dsc1", Author = "aa1" });
            context.Add(new Game { Name = "gra2", Description = "dsc2", Author = "aa2" });
            context.Add(new Game { Name = "gra3", Description = "dsc3", Author = "aa3" });
            context.Add(new Game { Name = "gra4", Description = "dsc4", Author = "aa4" }); //Id4 in list[3]
            context.SaveChanges();

            var request = new CreateGameCommand()
            {
                Name = "gra5",
                Description = "desc5",
                Author = "aa5",
            };

            var service = new CreateGameCommand.CreateGameCommandHandler(context);

            var result = await service.Handle(request, default);
            context.SaveChanges();
            var ListOfGames = context.Games.ToList();
            Assert.AreEqual(ListOfGames[ListOfGames.Count - 1].Id.ToString(), result);
        }
    }

}
