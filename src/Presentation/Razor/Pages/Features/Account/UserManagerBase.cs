using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Infrastructure.Services;
using Infrastructure.Persistence;
using Domain.Models;

namespace Razor.Pages.Features.Account
{
    public class UserManagerBase : ComponentBase
    {
        [Inject]
        protected FileManager? FileManager { get; init; }

        [Inject]
        protected AppDbContext? AppDbContext { get; init; }

        protected User? User { get; set; }
        protected User? Guest { get; init; } = new User(){
            Id = Guid.NewGuid().ToString()
        };

        protected string? Password { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = new User{
                Id = Guid.NewGuid().ToString(),
                Role = "developer",
                Username = "einharan",
                Password = "248163264",
                EmailAddress = "einharan.protonmail.com",
                FirstName = "Vance",
                LastName = "Einharan",
                DateOfBirthFormat = new DateOnly(1996, 8, 19),
                ContactNumber = "-",
                PictureProfile = "einharan.png"
            };

            var(_, isUserExist) = AppDbContext!.LookUpUser(User.Username, User.Password);

            if(!isUserExist)
            {
                await AppDbContext.InsertUserAsync(User);
            }
        }
        protected override void OnInitialized()
        {
            // string subfolder = $"images/avatars";
            // System.Console.WriteLine($"Path: {FileManager!.GetDataStoragePath(subfolder)}");
        }

    }
}