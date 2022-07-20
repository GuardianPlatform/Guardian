using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Service.Features.Category.Commands;
using Guardian.Service.Features.Category.Queries;
using Guardian.Service.Features.Game.Commands;
using Guardian.Test.Unit.Persistence;
using NUnit.Framework;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Guardian.Test.Unit.CategoryTests
{
    public class UpdateCategoryCommandHandlerTests
    {

        [Test]
        public async Task UpdateCategoryTest_IsCategoryUpdated()
        {
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);

            context.Add(new Category {CategoryName = "Kategoria", Id = 0});

            await context.SaveChangesAsync();

            var request = new UpdateCategoryCommand()
            {
                CategoryName = "Nowa Kategoria",
                Id = 0
            };

            var category = context.Categories.FirstOrDefault(c => c.CategoryName == "Kategoria");

            category.CategoryName = request.CategoryName;

            context.ChangeTracker.Clear();

            context.Update(category);

            await context.SaveChangesAsync();

            string defaultName = "Nowa Kategoria";

            Assert.IsTrue(category.CategoryName == defaultName);
        }
    }
}
