using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newIdManager.Data.ApplicationUsers;

namespace newIdManager.Components
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("PerformExternalLogin")]
        public IActionResult PerformExternalLogin([FromForm]string provider, [FromForm]string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromForm] string returnUrl)
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect($"~/{returnUrl}");
        }
        [HttpPost("Hello")]
        public IActionResult GetHello()
        {
            Console.WriteLine("Come to Hello");
            return Ok("Hello from AccountController");
        }


    }
}
