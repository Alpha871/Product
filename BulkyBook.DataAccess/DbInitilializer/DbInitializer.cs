using BulkyBook.DataAccess.DbInitilializer;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.DbInitilializer
{
    public class DbInitializer:IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                _db.Database.EnsureCreated();
                var migrations = _db.Database.GetPendingMigrations();
                if (migrations.Any())
                    _db.Database.Migrate();
            }
            catch (Exception)
            {

            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(Commun.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_User_Comp)).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well

                var user = new ApplicationUser
                {
                    UserName = "b1912105551@sakarya.edu.tr",
                    Email = "b191210551@sakarya.edu.tr",
                    Name = "Alpha Bah",
                    PhoneNumber = "05310000000",
                    StreetAddress = "Serdivan",
                    State = "Turkey",
                    PostalCode = "54580",
                    City = "Sakarya"
                };
                var password = "ISE";

                _userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, Commun.Role_Admin).GetAwaiter().GetResult();

                _db.SaveChanges();

            }

            return;
        }
    }
}