using GoodRoad.Data;
using GoodRoad.Data.Enums;
using GoodRoad.Interfaces;
using GoodRoad.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoodRoad.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> rolemanager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _rolemanager = rolemanager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            // Apply migration if they are not applied

            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {


            }

            // Create roles if they are not created

            if (!_rolemanager.RoleExistsAsync(UserRoles.Admin).GetAwaiter().GetResult())
            {

                _rolemanager.CreateAsync(new IdentityRole(UserRoles.Admin)).GetAwaiter().GetResult();
                _rolemanager.CreateAsync(new IdentityRole(UserRoles.User)).GetAwaiter().GetResult();

                // If roles aren't created than create admin user as well

                _userManager.CreateAsync(new AppUser
                {
                    UserName = "akmat.ismailov2002@gmail.com",
                    Email = "akmat.ismailov2002@gmail.com",
                    Surname="Adminov",
                    Name = "Admin",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                    NumberOfHoles = 3,
                    NumberOfComments = 21,
                    CreatedAt = DateTime.Now,
                    ImageUrl = "https://www.example.com/mountain-view.jpg",
                    ImageId = "mountain-view.jpg"
                },
                password: "test123Z.").GetAwaiter().GetResult();

                AppUser applicationUser = _dbContext.AppUsers.FirstOrDefault(x => x.UserName == "akmat.ismailov2002@gmail.com");

                _userManager.AddToRoleAsync(applicationUser, UserRoles.Admin).GetAwaiter().GetResult();
            }



        }
    }
}
