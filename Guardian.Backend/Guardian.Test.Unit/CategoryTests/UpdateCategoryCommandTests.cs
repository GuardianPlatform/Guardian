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
    public class UpdateCategoryCommandTests
    {

        [Test]
        public async Task UpdateCategoryTest_IsCategoryUpdated()
        {
            const string expectedName = "Nowa Kategoria";
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);
            context.Add(new Category {CategoryName = "Kategoria"});
            await context.SaveChangesAsync();
            var request = new UpdateCategoryCommand()
            {
                CategoryName = "Nowa Kategoria",
                Id = 1
            };
            var service = new UpdateCategoryCommand.UpdateCategoryCommandHandler(context); 
            
            context.ChangeTracker.Clear();
            await service.Handle(request, default);
            var result = context.Categories.First();
            
            Assert.AreEqual(expectedName, result.CategoryName);
        }
    }
}
