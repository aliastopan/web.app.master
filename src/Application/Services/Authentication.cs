using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Application.Services
{
    public class Authentication : AuthenticationStateProvider
    {
        private AuthenticationState Guest()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authetication = Guest();
            return await Task.FromResult(authetication);
        }

        public async Task<AuthenticationState> AuthenticateAsync(string username)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, username)
            };
            var authType = "apiauth_type";
            var identity = new ClaimsIdentity(claims, authType);
            var user = new ClaimsPrincipal(identity);
            var authetication = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authetication));
            return await Task.FromResult(authetication);
        }

        public void Authenticate(string username)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, username)
            };
            var authType = "apiauth_type";
            var identity = new ClaimsIdentity(claims, authType);
            var user = new ClaimsPrincipal(identity);
            var authetication = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authetication));
        }

        public AuthenticationState AuthState(string username)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, username)
            };
            var authType = "apiauth_type";
            var identity = new ClaimsIdentity(claims, authType);
            var user = new ClaimsPrincipal(identity);
            var authetication = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authetication));
            return authetication;
        }

        public ClaimsIdentity CreateIdentity(string username)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, username)
            };
            var authType = "apiauth_type";
            return new ClaimsIdentity(claims, authType);
        }



    }
}