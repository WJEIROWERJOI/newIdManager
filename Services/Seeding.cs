using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using newIdManager.Data;
using newIdManager.Data.ApplicationUsers;

namespace newIdManager.Services
{
    public class Seeding
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public Seeding(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            if (!_context.Boards.Any())
            {
                var boards = new List<Board>
                {
                    new Board { Title = "User List", Url = "/list", Img = "bi bi-person-fill-nav-menu" },
                    new Board { Title = "User Register", Url = "/register", Img = "bi bi-lock-nav-menu" },
                    new Board { Title = "Post", Url = "/posts/lists", Img = "bi bi-list-nested-nav-menu" }
                };
                _context.Boards.AddRange(boards);
                await _context.SaveChangesAsync();
            }

            var roles = new List<string> { "SuperAdmin", "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (!await _userManager.Users.AnyAsync())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser { UserName = "admin", Email = "admin@admin.com", PasswordHash = "admin", Role = UserRole.Admin },
                    new ApplicationUser { UserName = "test1", Email = "test1@test1.com", PasswordHash = "test1", Role = UserRole.Admin }
                };

                foreach (var user in users)
                {
                    await _userManager.CreateAsync(user, user.PasswordHash ?? string.Empty);
                    if (user.UserName == "admin")
                    {
                        await _userManager.AddToRolesAsync(user, new[] { "SuperAdmin", "Admin" });
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }

    }
}
