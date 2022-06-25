using Guardian.Domain.Entities;
using Guardian.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Guardian.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertCustomerIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var user = new User();
            context.Users.Add(user);
            Assert.AreEqual(EntityState.Added, context.Entry(user).State);
        }
    }
}
