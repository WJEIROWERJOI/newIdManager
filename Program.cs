using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using newIdManager.Components;
using newIdManager.Components.Account;
using newIdManager.Data;
using newIdManager.Data.ApplicationUsers;
using newIdManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<ApplicationUserService>();
builder.Services.AddScoped<BoardService>();

builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;             // 숫자 필요 X
    options.Password.RequireLowercase = false;         // 소문자 필요 X
    options.Password.RequireUppercase = false;         // 대문자 필요 X
    options.Password.RequireNonAlphanumeric = false;   // 특수문자 필요 X
    options.Password.RequiredLength = 0;               // 최소 길이 줄이기
    options.Password.RequiredUniqueChars = 0;          // 고유 문자 개수
});




builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()//Role 쓸때 추가
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();


//using (var scope = app.Services.CreateScope())
//{
    //BoardService boardService;
    //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //ApplicationUserService applicationUserService = new();
    //await applicationUserService.GetAllUsersAsync();

    //await BoardService.SeedBoardAsync(context);
    //await BoardService.AddInnerBoardAsync(context, 1, 2);
    //await boardService.RemoveAllBoard(context);
//}//board seeding

app.Run();
