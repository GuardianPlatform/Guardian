using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using Guardian.Service.Features.Category.Commands;
using Guardian.Service.Features.Category.Queries;
using Guardian.Service.Features.Game.Queries;
using Guardian.Test.Unit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Guardian.Test.Unit.CategoryTests
{

    public class CreateCategoryCommandTests
    {
        [Test]
        public async Task CreateCategoryTest_AddSameCategoryException()
        {
            const string exceptionMessage = "Category with given name already exists";
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);
            context.Add(new Category { CategoryName = "Kategoria" });
            await context.SaveChangesAsync();
            var service = new CreateCategoryCommand.CreateCategoryCommandHandler(context);
            var request = new CreateCategoryCommand("Kategoria");

           
            Exception myException = Assert.ThrowsAsync<Exception>(async () => await service.Handle(request, default));

            Assert.AreEqual(exceptionMessage, myException.Message);
        }

        [Test]
        public async Task CreateCategoryTest_IsOneCategoryCreated()
        {
            const int expectedNumberOfGames = 1;
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);
            var service = new CreateCategoryCommand.CreateCategoryCommandHandler(context);

            await service.Handle(new CreateCategoryCommand("Kategoria"), default);
            var result = context.Categories.Count();

            Assert.AreEqual(expectedNumberOfGames, result);
        }

        [Test]
        public async Task CreateCategoryTest_IsFewCategoryCreated()
        {
            const int expectedNumberOfGames = 3;
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);
            var service = new CreateCategoryCommand.CreateCategoryCommandHandler(context);

            await service.Handle(new CreateCategoryCommand("Kategoria1"), default);
            await service.Handle(new CreateCategoryCommand("Kategoria2"), default);
            await service.Handle(new CreateCategoryCommand("Kategoria3"), default);
            var result = context.Categories.Count();

            Assert.AreEqual(expectedNumberOfGames, result);
        }
    }
}
