using Microsoft.AspNetCore.Components.Authorization;
using newIdManager.Components.Account;
using newIdManager.Services;

namespace newIdManager.Extensions;
public static class ServiceExtensions
{

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ApplicationUserService>();
        services.AddScoped<BoardService>();
        services.AddScoped<CommentService>();
        services.AddScoped<PostService>();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<Seeding>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        services.AddCascadingAuthenticationState();
        return services;

    }
}