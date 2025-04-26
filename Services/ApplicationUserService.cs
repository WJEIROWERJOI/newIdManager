using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using newIdManager.Data;
using newIdManager.Data.ApplicationUsers;

namespace newIdManager.Services;
public class ApplicationUserService
{

    private readonly UserManager<ApplicationUser> _userManager;
    //private readonly SignInManager<ApplicationUser> _signinManager;
    private SignInManager<ApplicationUser> _signinManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signinManager,
    ApplicationDbContext context,
    RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _signinManager = signinManager;
        _roleManager = roleManager;
    }

    //Create
    public async Task<IdentityResult> RegisterAsync(ApplicationRegisterDto dto)
    {
        if (await IsUserExistAsync(dto.UserName))
        {
            return IdentityResult.Failed(new IdentityError { Description = "이미 존재하는 UserName 입니다." });
        }
        var user = new ApplicationUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) { return result; }

        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!roleResult.Succeeded)
            {
                return roleResult; // 롤 생성 실패 시 반환
            }
        }
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
            if (!roleResult.Succeeded)
            {
                return roleResult; // 롤 생성 실패 시 반환
            }
        }

        result = await _userManager.AddToRoleAsync(user, dto.Role.ToString());

        if (!result.Succeeded) { return result; }

        //Console.WriteLine(dto.Role.ToString());
        return IdentityResult.Success;
    }
    //Read
    /// userName, Email, Role 로 찾아서 List 로 넣는거 ㄱㄱ

    public async Task<List<ApplicationViewDto>> SearchUsersAsync(string str, string target, int Page, int amount)
    {

        //int Page = await _context.ApplicationUsers.Where(p => p.UserName == str).CountAsync();
        var users = new List<ApplicationUser>();
        //if (str == "Admin")
        //{
        //        users = await _context.ApplicationUsers.Where(p => p.Role == UserRole.Admin).ToListAsync();

        //    }
        switch (target)
        {
            case "UserName":
                users = await _context.ApplicationUsers
                    .Where(p => p.UserName == str)
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((Page - 1) * amount)
                    .Take(amount)
                    .ToListAsync();
                break;
            case "Email":
                users = await _context.ApplicationUsers
                    .Where(p => p.Email == str)
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((Page - 1) * amount)
                    .Take(amount)
                    .ToListAsync();
                break;
            case "Role":
                if (str == "Admin")
                {
                    users = await _context.ApplicationUsers
                    .Where(p => p.Role == UserRole.Admin)
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((Page - 1) * amount)
                    .Take(amount)
                    .ToListAsync();
                    break;
                }
                else if (str == "User")
                {
                    users = await _context.ApplicationUsers
                    .Where(p => p.Role == UserRole.User)
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((Page - 1) * amount)
                    .Take(amount)
                    .ToListAsync();
                }
                break;
            default:
                break;
        }

        return users.Select(user => new ApplicationViewDto
        {
            Id = user.Id ?? string.Empty,  // string이라면 기본값 처리
            UserName = user.UserName ?? "(알 수 없음)",
            Email = user.Email ?? "(이메일 없음)",
            Role = user.Role, // enum은 기본값 가짐
            CreatedAt = user.CreatedAt == default ? DateTime.MinValue : user.CreatedAt,
            LastUpdatedAt = user.LastUpdatedAt == default ? DateTime.MinValue : user.LastUpdatedAt
        }).ToList();
    }

    public async Task<int> GetNumberOfSearchUser(string str, string target)
    {
        switch (target)
        {
            case "UserName":
                return await _context.ApplicationUsers.Where(p => p.UserName == str).CountAsync();
            case "Email":
                return await _context.ApplicationUsers.Where(p => p.Email == str).CountAsync();
            case "Role":
                if (str == "Admin")
                {
                    return await _context.ApplicationUsers.Where(p => p.Role == UserRole.Admin).CountAsync();
                }
                else if (str == "User")
                {
                    return await _context.ApplicationUsers.Where(p => p.Role == UserRole.User).CountAsync();
                }
                break;
            default:
                break;
        }
        return 0;
    }
    public bool IsUserLogin(ClaimsPrincipal principal)
    {
        return _signinManager.IsSignedIn(principal);
    }
    public async Task<ApplicationUser?> FindByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    public async Task<ApplicationUser?> FindByNameAsync(string id)
    {
        return await _userManager.FindByNameAsync(id);
    }
    public async Task<bool> IsUserExistAsync(string name)
    {
        return await _userManager.FindByNameAsync(name) is not null;
    }
    public async Task<List<ApplicationViewDto>> GetAllUsersAsync()
    {
        var users = await _context.ApplicationUsers.ToListAsync();
        return users.Select(user => new ApplicationViewDto
        {
            Id = user.Id ?? string.Empty,  // string이라면 기본값 처리
            UserName = user.UserName ?? "(알 수 없음)",
            Email = user.Email ?? "(이메일 없음)",
            Role = user.Role, // enum은 기본값 가짐
            CreatedAt = user.CreatedAt == default ? DateTime.MinValue : user.CreatedAt,
            LastUpdatedAt = user.LastUpdatedAt == default ? DateTime.MinValue : user.LastUpdatedAt
        }).ToList();
    }
    public async Task<List<ApplicationViewDto>> GetPageUsersAsync(int Page, int amount)
    {

        var users = await _context.ApplicationUsers.OrderByDescending(p => p.CreatedAt).Skip((Page - 1) * amount).Take(amount).ToListAsync();
        return users.Select(user => new ApplicationViewDto
        {
            Id = user.Id ?? string.Empty,  // string이라면 기본값 처리
            UserName = user.UserName ?? "(알 수 없음)",
            Email = user.Email ?? "(이메일 없음)",
            Role = user.Role, // enum은 기본값 가짐
            CreatedAt = user.CreatedAt == default ? DateTime.MinValue : user.CreatedAt,
            LastUpdatedAt = user.LastUpdatedAt == default ? DateTime.MinValue : user.LastUpdatedAt
        }).ToList();
    }

    public async Task<int> GetNumberOfUser()
    {
        return await _context.ApplicationUsers.CountAsync();
    }

    //Update
    public async Task<IdentityResult> UpdateAsync(ApplicationUpdateDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.Id);
        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "사용자가 존재하지 않습니다." });
        }

        user.UserName = dto.UserName;
        user.Email = dto.Email;
        user.LastUpdatedAt = DateTime.UtcNow;

        if (user.Role != dto.Role)
        {
            user.Role = dto.Role;
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRoleAsync(user, dto.Role.ToString());
        }
        return await _userManager.UpdateAsync(user);

    }
    public async Task<IdentityResult> UpdatePasswordAsync(ApplicationUpdateDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.Id);
        if (user is null)
        { return IdentityResult.Failed(new IdentityError { Description = "해당 사용자를 찾을 수 없습니다." }); }
        if ((await _userManager.RemovePasswordAsync(user)).Succeeded)
        { return await _userManager.AddPasswordAsync(user, dto.NewPassword); }
        else { return IdentityResult.Failed(); }
    }
    //Delete
    public async Task<IdentityResult> DeleteAsync(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user is null) return IdentityResult.Failed(new IdentityError { Description = "사용자가 없습니다." });
        return await _userManager.DeleteAsync(user);
    }


    //login
    public async Task<IdentityResult> ApplicationUserSignIn(ApplicationLoginDto dto)
    {
        var result = await _signinManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, false);
        Console.WriteLine(result);
        if (result.Succeeded)
        {
            return IdentityResult.Success;
        }
        else
        {
            return IdentityResult.Failed(new IdentityError { Description = "로그인 실패 했습니다." });
        }
    }

    public async Task ApplicationUserLogOut()
    {
        await _signinManager.SignOutAsync();
    }
}