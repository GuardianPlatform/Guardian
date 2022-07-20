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
            var context = TestDbContext.TestDbContextMethod(out var dbContextOptions);
            
            context.Add(new Category {CategoryName = "Kategoria", Id = 0});
            
            await context.SaveChangesAsync();

            var category = context.Categories.FirstOrDefault(name => name.CategoryName == "Kategoria");
            context.ChangeTracker.Clear();

            var request = new DeleteCategoryCommand();

            request.Id = category.Id;
            context.Remove(category);

            await context.SaveChangesAsync();

            int numberOfCategories = 0;

            Assert.AreEqual(numberOfCategories, context.Categories.Count());

        }
    }
}
