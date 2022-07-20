using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Test.Unit.Persistence
{
    public class TestDbContext
    {
        public static ApplicationDbContext TestDbContextMethod(out DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);
            return context;
        }
    }
}
