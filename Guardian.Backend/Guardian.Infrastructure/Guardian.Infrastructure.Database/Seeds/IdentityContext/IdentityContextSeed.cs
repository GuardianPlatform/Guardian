using Guardian.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Guardian.Infrastructure.Database.Seeds.Contexts
{
    public static class IdentityContextSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            CreateRoles(modelBuilder);

            CreateBasicUsers(modelBuilder);

            MapUserRole(modelBuilder);
        }

        private static void CreateRoles(ModelBuilder modelBuilder)
        {
           var roles = DefaultRoles.IdentityRoleList();
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        private static void CreateBasicUsers(ModelBuilder modelBuilder)
        {
            var users = DefaultUser.IdentityBasicUserList();
            modelBuilder.Entity<User>().HasData(users);
        }

        private static void MapUserRole(ModelBuilder modelBuilder)
        {
            var identityUserRoles = MappingUserRole.IdentityUserRoleList();
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);
        }

    }
}
