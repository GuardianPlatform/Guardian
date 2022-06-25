using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Guardian.Infrastructure.Database
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Game> Games { get; set; }
        

        Task<int> SaveChangesAsync();
    }
}
