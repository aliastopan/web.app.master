using System.Xml;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Infrastructure.Persistence;
using Domain.Models;

namespace Infrastructure.Services
{
    public class Authenticator : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage localStorage;
        private readonly AppDbContext appDbContext;

        public Authenticator(ProtectedLocalStorage localStorage, AppDbContext dbContext)
        {
            this.localStorage = localStorage;
            this.appDbContext = dbContext;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var authetication = new AuthenticationState(user);
            return await Task.FromResult(authetication);
        }

        public async Task LogInAsync(User guest)
        {
            var (user, isVerified) = appDbContext.LookUpUser(guest.Username!, guest.Password!);
            var principal = new ClaimsPrincipal();

            if(isVerified)
            {
                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name, guest.Username!),
                    new Claim(ClaimTypes.Role, guest.Role!)
                };
                var authType = "apiauth_type";
                var identity = new ClaimsIdentity(claims, authType);
                principal = new ClaimsPrincipal(identity);

                var format = new JsonSerializerOptions { WriteIndented = true };
                string credential = JsonSerializer.Serialize<User>(user, format);
                await localStorage.SetAsync(user.Role!, credential);
            }

            var authState = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public void Persistent()
        {

        }
    }
}