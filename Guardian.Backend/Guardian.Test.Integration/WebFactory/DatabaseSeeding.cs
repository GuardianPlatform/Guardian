using Guardian.Infrastructure.Database;

namespace Guardian.Test.Integration.WebFactory
{
    public class DatabaseSeeding
    {
        public static void ApplicationContext(ApplicationDbContext applicationContext)
        {
           /* applicationContext.Categories.AddRange(DefaultCategories.CreateDefaultCategories());
            applicationContext.Games.AddRange(DefaultGames.CreateDefaultGames());
            applicationContext.Ratings.AddRange(DefaultRatings.CreateDefaultRatings());
            applicationContext.GameCategories.AddRange(DefaultGameCategory.CreateDefaultGameCategories());
            applicationContext.SaveChanges();*/
        }

        public static void IdentityContext(IdentityContext identityContext)
        {

        }
    }
}
