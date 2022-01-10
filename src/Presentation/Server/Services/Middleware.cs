using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Infrastructure.Persistence;
using Application.Services;
using Domain.Models;

namespace Server.Services
{
    public class Middleware
    {
        private readonly Authentication authentication;
        private readonly ProtectedLocalStorage localStorage;
        private readonly AppDbContext dataContext;

        public Middleware(Authentication authentication,
            ProtectedLocalStorage localStorage,
            AppDbContext dataContext)
        {
            this.authentication = authentication;
            this.localStorage = localStorage;
            this.dataContext = dataContext;
        }

        public async Task<bool> TryLoginAsync(string username, string password)
        {
            bool isUserExist = dataContext.Users!
                    .Any( user => user.Username == username);

            if(!isUserExist)
            {
                System.Console.WriteLine("Not Found");
                return false;
            }

            User User = dataContext .Users!
                    .Where(user => user.Username == username)
                    .FirstOrDefault<User>()!;

            bool isAuthenticated = Cryptograph.HashCheck(password, User!.Password!);

            if(!isAuthenticated)
            {
                System.Console.WriteLine("Not Authenticated");
                return false;
            }
            else
            {
                // Claim[] claims = (Claim[]) authentication.AuthState(username).User.Claims;
                // ClaimsIdentity user = authentication.CreateIdentity(username);
                string persistent = JsonSerializer.Serialize<User>(User);
                await localStorage.SetAsync(User.Role!, persistent);
                return true;
            }


        }

        public async Task CheckPersistent()
        {
            var result = await localStorage.GetAsync<string>("Developer");

            if(result.Success)
            {
                string json = result.Value!;
                User user = JsonSerializer.Deserialize<User>(json)!;
                authentication.Authenticate(user.Username!);
            }
        }
    }
}