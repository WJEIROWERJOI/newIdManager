using newIdManager.Components;
namespace newIdManager.Extensions;
public static class MiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
    }

    public static void ConfigureMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapAdditionalIdentityEndpoints();
    }
}