using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VibeHome.UI
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (AuthState.CurrentUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, AuthState.CurrentUser.Username)
                };

                // Add each role as a separate claim
                if (AuthState.CurrentUser.Role != null)
                {
                    //TODO: really need to rethinkg this part.  Putting in the wrong DB was a mistake.  Need to fix this locally and fix the roles. Need to one user to many rows. 
                    foreach (var role in AuthState.CurrentUser.Role.Split(','))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim().ToLower()));
                    }
                }

                identity = new ClaimsIdentity(claims, "apiauth");
            }

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }

        public void NotifyUserAuthentication()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
} 