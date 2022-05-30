using Guardian.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Guardian.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Rating> Orders { get; set; }
        DbSet<Game> Products { get; set; }
        DbSet<Supplier> Suppliers { get; set; }

        Task<int> SaveChangesAsync();
    }
}
