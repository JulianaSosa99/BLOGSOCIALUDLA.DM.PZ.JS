using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebSocialUdla.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //sembrar roles

            var adminRoleId = "657932e3-27c9-4798-a56a-7beccc00695c";
            var superAdminRoleId = "cb13d071-3192-46ef-bb82-42d3c912f61c";
            var userRoleId = "ec8bcf1e-03cd-4c28-9809-c87931cf0f2a";
            var roles = new List<IdentityRole>
            {
               
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },

                 new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                  new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
            };
            
            builder.Entity<IdentityRole>().HasData(roles);

            //sembrar superadmin
            var superAdminId = "0c5b42e9-66de-42ad-a47f-f2e344799295";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@udla.com",
                Email = "superadmin@udla.com",
                NormalizedEmail = "superadmin@udla.com".ToUpper(),
                NormalizedUserName = "superadmin@udla.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //agregar todos los roles para el superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,

                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,

                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,

                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }

    }
}
