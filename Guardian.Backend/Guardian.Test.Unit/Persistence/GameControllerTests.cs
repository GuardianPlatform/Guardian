using Microsoft.EntityFrameworkCore;
using Guardian.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;
using Guardian.Domain.Models;
using System.Linq;
using Guardian.Infrastructure.Database;
using Guardian.Service.Features.Game.Queries;
using System;

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
    }

}
