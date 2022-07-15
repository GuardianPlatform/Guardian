using Guardian.Infrastructure.Database.Seeds.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardian.Infrastructure.Database;
using Guardian.Infrastructure.Database.Seeds;
using Guardian.Infrastructure.Database.Seeds.IdentityContext;

namespace Guardian.Test.Integration.WebFactory
{
    public class DatabaseSeeding
    {
        public static void ApplicationContext(IApplicationDbContext applicationContext)
        {
            applicationContext.Categories.AddRange(DefaultCategories.CreateDefaultCategories());
            applicationContext.Games.AddRange(DefaultGames.CreateDefaultGames());
            applicationContext.Ratings.AddRange(DefaultRatings.CreateDefaultRatings());
            applicationContext.GameCategories.AddRange(DefaultGameCategory.CreateDefaultGameCategories());
        }

        public static void IdentityContext(IdentityContext identityContext)
        {

        }
    }
}
