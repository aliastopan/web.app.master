using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Application.Services;
using Domain.Models;

namespace Server.Services
{
    public class Middleware
    {
        private readonly ProtectedLocalStorage localStorage;

        public Middleware(ProtectedLocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public bool Login(string username, string password)
        {
            return false;
        }
    }
}