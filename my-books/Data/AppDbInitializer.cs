using Microsoft.AspNetCore.Identity;
using my_books.Data.Models;
using my_books.Data.ViewModels.Authentication;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        //public static void Seed(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();




        //        if (!context.Books.Any())
        //        {
        //            context.Books.AddRange(new List<Book>()
        //            {
        //                new Book()
        //                {
        //                    Title = "Harry Potter",
        //                    Description = "Just read",
        //                    IsRead = true,
        //                    DateRead = DateTime.Now.AddDays(-3),
        //                    Rate = 8,
        //                    Genre = "Fantastic",
        //                    CoverUrl = "shit",
        //                    DateAdded = DateTime.Now.AddDays(-10),
        //                },
        //                new Book()
        //                {
        //                    Title = "Metro 2034",
        //                    Description = "Just read",
        //                    IsRead = false,
        //                    Genre = "Thriller",
        //                    CoverUrl = "shit",
        //                    DateAdded = DateTime.Now.AddDays(-7),
        //                }
        //            });

        //            context.SaveChanges();
        //        }
        //    }
        //}


        public static async Task SeedRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.Publisher))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Publisher));

                if (!await roleManager.RoleExistsAsync(UserRoles.Author))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Author));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            }
        }
    }
}
