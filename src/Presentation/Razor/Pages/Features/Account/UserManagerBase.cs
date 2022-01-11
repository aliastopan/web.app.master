using Microsoft.AspNetCore.Components;
using Domain.Models;

namespace Razor.Pages.Features.Account
{
    public class UserManagerBase : ComponentBase
    {
        protected User? User { get; set; }
        public User? Guest { get; set; }

        protected override void OnInitialized()
        {
            User = new User{
                Id = Guid.NewGuid().ToString(),
                Role = "developer",
                Username = "einharan",
                Password = "248163264",
                EmailAddress = "einharan.protonmail.com",
                FirstName = "Vance",
                LastName = "Einharan",
                DateOfBirth = new DateOnly(1996, 8, 19),
                PictureProfile = "einharan.png"
            };
        }
    }
}