using Microsoft.AspNetCore.Identity;
using newIdManager.Data;
using newIdManager.Data.ApplicationUsers;

namespace newIdManager.Services
{
    public static class Seeding
    {
        public static async Task SeedingBoard(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!context.Boards.Any())
            {
                List<Board> brds = new List<Board> {
                    new Board { Title = "User List", Url = "/list", Img = "bi bi-person-fill-nav-menu" },
                    new Board { Title = "User Register", Url = "/register", Img = "bi bi-lock-nav-menu" },
                    new Board { Title = "Post", Url = "/posts/lists", Img = "bi bi-list-nested-nav-menu" }
                };
                context.Boards.AddRange(brds);
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!context.ApplicationUsers.Any())
            {
                List<ApplicationUser> usrs = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        PasswordHash = string.Empty,
                        Role = UserRole.Admin
                    },
                    new ApplicationUser
                    {
                        UserName = "test1",
                        Email = "test1@test1.com",
                        PasswordHash = string.Empty,
                        Role = UserRole.Admin
                    }
                };
                context.ApplicationUsers.AddRange(usrs);
                await context.SaveChangesAsync();            }

            var users = context.ApplicationUsers.ToList();
            foreach (var usr in users)
            {
                var identityUser = await userManager.FindByIdAsync(usr.Id);
                if (identityUser != null && !await userManager.IsInRoleAsync(identityUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(identityUser, "Admin");
                }
            }
        }
    }
}
