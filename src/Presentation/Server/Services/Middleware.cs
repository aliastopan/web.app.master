using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Infrastructure.Data;
using Application.Services;
using Domain.Models;

namespace Server.Services
{
    public class Middleware
    {
        private readonly Authentication authentication;
        private readonly ProtectedLocalStorage localStorage;
        private readonly DataContext dataContext;

        public Middleware(Authentication authentication,
            ProtectedLocalStorage localStorage,
            DataContext dataContext)
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
                ClaimsIdentity user = authentication.CreateIdentity(username);
                string persistent = JsonSerializer.Serialize<ClaimsIdentity>(user);
                await localStorage.SetAsync(username, persistent);
                return true;
            }


        }
    }
}