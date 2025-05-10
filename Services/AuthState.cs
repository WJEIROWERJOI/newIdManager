
using Microsoft.AspNetCore.Components.Authorization;

namespace newIdManager.Services
{
    public class AuthState
    {
        private static AuthenticationStateProvider? _auth;
        public void Initialize(AuthenticationStateProvider authProvider)
        {
            _auth = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
        }
        
    }
}
