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
    
    public class CreateCategoryCommandHandlerTests
    {
        [Test]
        [ExpectedException(typeof(Exception),
            "Category with given name already exist")]
        public async Task CreateCategoryTest_AddSameCategoryException()
        {
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);

            context.Add(new Category { CategoryName = "Kategoria" });

            await context.SaveChangesAsync();

            var request = new CreateCategoryCommand("Kategoria");
        }
        
        [Test]
        public async Task CreateCategoryTest_IsOneCategoryCreated()
        {
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);

            var request = new CreateCategoryCommand("Kategoria");
            context.Add(new Category() {CategoryName = $"{request.CategoryName}"});

            await context.SaveChangesAsync();

            int numberOfGames = 1;

            var result = context.Categories.Count();

            Assert.AreEqual(numberOfGames, result);
        }

        [Test]
        public async Task CreateCategoryTest_IsFewCategoryCreated()
        {
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);

            var request1 = new CreateCategoryCommand("Kategoria1");
            context.Add(new Category() { CategoryName = $"{request1.CategoryName}" });

            var request2 = new CreateCategoryCommand("Kategoria2");
            context.Add(new Category() { CategoryName = $"{request2.CategoryName}" });

            var request3 = new CreateCategoryCommand("Kategoria3");
            context.Add(new Category() { CategoryName = $"{request3.CategoryName}" });

            await context.SaveChangesAsync();

            int numberOfGames = 3;

            var result = context.Categories.Count();

            Assert.AreEqual(numberOfGames, result);
        }
    }
}
