using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.Database;
using Guardian.Service.Features.Category.Commands;
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

        
    }
}
