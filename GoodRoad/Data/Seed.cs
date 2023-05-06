using Microsoft.AspNetCore.Identity;
using GoodRoad.Data;
using GoodRoad.Models;
using System.Diagnostics;
using GoodRoad.Data.Enums;

namespace GoodRoad.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Holes.Any())
                {
                    context.Holes.AddRange(new List<Hole>()
                    {
                        new Hole()
                        {
                            Name = "Gorgeous Greens",
                            Description = "This is a stunning hole surrounded by lush greenery",
                            NumberOfLikes = 12,
                            Size = Size.Large,
                            Address = new Address()
                            {
                            Street = "456 Park Ave",
                            City = "Chicago",
                            State = "IL"
                            },
                            //ContributorId = "546c9eb0-9f33-4774-87a0-4c0d24d37cdd",
                            Coordinates = new Coordinates()
                            {
                            Latitude = 41.881832,

                            Longitude = -87.623177
                            },
                            CreatedAt = new DateTime(2022, 4, 1),
                            FixedAt = new DateTime(2022, 4, 15),
                            ImageUrl = "https://www.example.com/gorgeous-greens.jpg",
                            ImageId = "gorgeous-greens.jpg"
                            },

                        new Hole()
                        {
                            Name = "Rocky Ravine",
                            Description = "This hole has a rocky terrain and a challenging layout",
                            NumberOfLikes = 8,
                            Size = Size.Medium,
                            Address = new Address()
                            {
                            Street = "789 Mountain Rd",
                            City = "Denver",
                            State = "CO"
                            },
                            //ContributorId = "546c9eb0-9f33-4774-87a0-4c0d24d37cdd",
                            Coordinates = new Coordinates()
                            {
                            Latitude = 39.739236,
                            Longitude = -104.990251
                            },
                            CreatedAt = new DateTime(2022, 3, 15),
                            FixedAt = new DateTime(2022, 3, 30),
                            ImageUrl = "https://www.example.com/rocky-ravine.jpg",
                            ImageId = "rocky-ravine.jpg"
                            
                        },

                        new Hole()
                        {
                            Name = "Seaside Six",
                            Description = "This hole has a beautiful ocean view and a sandy beach",
                            NumberOfLikes = 5,
                            Size = Size.Small,
                            Address = new Address()
                            {
                            Street = "321 Ocean Blvd",
                            City = "Miami",
                            State = "FL"
                            },
                            //ContributorId = "b23d9d20-e626-46ec-b21f-af7af707246a",
                            Coordinates = new Coordinates()
                            {
                            Latitude = 25.761681,
                            Longitude = -80.191788
                            },
                            CreatedAt = new DateTime(2022, 5, 1),
                            FixedAt = new DateTime(2022, 5, 15),
                            ImageUrl = "https://www.example.com/seaside-six.jpg",
                            ImageId = "seaside-six.jpg",
                            
                            },


                        new Hole()
                        {
                            Name = "Mountain View",
                            Description = "This hole offers a breathtaking view of the mountains",
                            NumberOfLikes = 10,
                            Size = Size.Large,
                            Address = new Address()
                            {
                            Street = "456 Hillside Dr",
                            City = "Aspen",
                            State = "CO"
                            },
                            //ContributorId = "b23d9d20-e626-46ec-b21f-af7af707246a",
                            Coordinates = new Coordinates()
                            {
                            Latitude = 39.191097,
                            Longitude = -106.817535
                            },
                            CreatedAt = new DateTime(2022, 6, 1),
                            FixedAt = new DateTime(2022, 6, 15),
                            ImageUrl = "https://www.example.com/mountain-view.jpg",
                            ImageId = "mountain-view.jpg"
                        }
                    });

                    context.SaveChanges();
                }
                
                if (!context.Comments.Any())
                {
                    context.Comments.AddRange(new List<Comment>()
                    {
                        new Comment()
                        {
                            Text = "Great job!",
                            CreatedAt = DateTime.UtcNow,
                            NumberOfLikes = 10,
                            //OwnerId = "546c9eb0-9f33-4774-87a0-4c0d24d37cdd",
                            //HoleId = 2
                        },
                        new Comment()
                        {
                            Text = "Love it!",
                            CreatedAt = DateTime.UtcNow,
                            NumberOfLikes = 5,
                            //OwnerId = "b23d9d20-e626-46ec-b21f-af7af707246a",
                            //HoleId = 3
                        },
                        new Comment()
                        {
                            Text = "Awesome hole!",
                            CreatedAt = DateTime.UtcNow,
                            NumberOfLikes = 20,
                            //OwnerId = "546c9eb0-9f33-4774-87a0-4c0d24d37cdd",
                            //HoleId = 4
                        },
                        new Comment()
                        {
                            Text = "Fantastic!",
                            CreatedAt = DateTime.UtcNow,
                            NumberOfLikes = 7,
                            //OwnerId = "b23d9d20-e626-46ec-b21f-af7af707246a",
                            //HoleId = 5
                        },
                        new Comment()
                        {
                            Text = "Well done!",
                            CreatedAt = DateTime.UtcNow,
                            NumberOfLikes = 3,
                            //OwnerId = "546c9eb0-9f33-4774-87a0-4c0d24d37cdd",
                            //HoleId = 2
                        }

                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Name = "admin",
                        Surname = "admintos",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        NumberOfHoles = 3,
                        NumberOfComments = 21,
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://www.example.com/mountain-view.jpg",
                        ImageId = "mountain-view.jpg"
                    };
                    await userManager.CreateAsync(newAdminUser, "test123Z.");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        Name = "app",
                        Surname = "useros",

                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        NumberOfHoles = 3,
                        NumberOfComments = 21,
                        CreatedAt = DateTime.Now,
                        ImageUrl = "https://www.example.com/mountain-view.jpg",
                        ImageId = "mountain-view.jpg"

                    };
                    await userManager.CreateAsync(newAppUser, "test123Z.");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}