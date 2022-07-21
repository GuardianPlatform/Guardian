using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain;
using Guardian.Domain.Entities;
using Guardian.Service.Features.Category.Commands;
using Guardian.Test.Unit.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Guardian.Test.Unit.CategoryTests
{
    public class DeleteCategoryCommandTests
    {
        [Test]
        public async Task DeleteCategoryCommandHandler_CanDeleteCategory()
        {
            const int expectedNumberOfCategories = 0;
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);
            context.Add(new Category {CategoryName = "Kategoria"});
            await context.SaveChangesAsync();
            var request = new DeleteCategoryCommand() {Id = 1};
            var service = new DeleteCategoryCommand.DeleteCategoryCommandHandler(context);

            context.ChangeTracker.Clear();
            await service.Handle(request, default);

            

            Assert.AreEqual(expectedNumberOfCategories, context.Categories.Count());

        }
    }
}
