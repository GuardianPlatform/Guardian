using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using Guardian.Service.Features.Category.Queries;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Guardian.Test.Unit.Persistence
{
        
    public class CategoriesControllerTests
    {

        [Test]
        public async Task GetAllCategoriesTest_ReturnCorrectNumberOfCategory()
        {
            
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);

            context.Add(new Category {CategoryName = "Kategoria 1"});
            context.Add(new Category {CategoryName = "Kategoria 2"});
            context.Add(new Category {CategoryName = "Kategoria 3"});
            context.SaveChanges();

            const int numberOfGames = 3;

            var request = new GetAllCategoriesQuery() { };

            var service = new GetAllCategoriesQuery.GetAllCategoriesQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(numberOfGames, result.Count());
        }

        [Test]
        public async Task GetAllCategoriesTest_EmptyCategoryList()
        {
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);

            const int numberOfGames = 0;
            
            var request = new GetAllCategoriesQuery();

            var service = new GetAllCategoriesQuery.GetAllCategoriesQueryHandler(context);

            var result = await service.Handle(request, default);

            Assert.AreEqual(numberOfGames, result.Count());
        }

        [Test]
        public async Task 
    }
}
