using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Infrastructure.Persistence;

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

        public void Auth()
        {
   
        }
    }
}