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
                var identity = CreateIdentity(user);
                principal = new ClaimsPrincipal(identity);

                var format = new JsonSerializerOptions { WriteIndented = true };
                string credential = JsonSerializer.Serialize<User>(user, format);
                await localStorage.SetAsync("identity", credential);
            }

            var autheticationState = new AuthenticationState(principal);
            NotifyAuthenticationStateChanged(Task.FromResult(autheticationState));
        }

        public async Task LogoutAsync()
        {
            await localStorage.DeleteAsync("identity");
            var principal = new ClaimsPrincipal();
            var autheticationState = new AuthenticationState(principal);

            NotifyAuthenticationStateChanged(Task.FromResult(autheticationState));
        }

        public async Task PersistentLoginAsync()
        {
            var result = await localStorage!.GetAsync<string>("identity");
            if(result.Success)
            {
                User user = JsonSerializer.Deserialize<User>(result.Value!)!;
                var identity = CreateIdentity(user);
                var principal = new ClaimsPrincipal(identity);
                var autheticationState = new AuthenticationState(principal);
                NotifyAuthenticationStateChanged(Task.FromResult(autheticationState));

            }
        }

        private ClaimsIdentity CreateIdentity(User user)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Role, user.Role!)
            };
            var authType = "apiauth_type";
            return new ClaimsIdentity(claims, authType);
        }
    }
}