using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Domain.Models;

namespace Server.Pages.Host
{
    public partial class Developer
    {
        [Inject]
        protected AppDbContext? Context { get; init; }

        [Inject]
        protected ProtectedLocalStorage? LocalStorage { get; init; }

        [Inject]
        protected Authenticator? Authenticator { get; init; }

        // protected override async Task OnInitializedAsync()
        // {
        //     User user = new User{
        //         Id = Guid.NewGuid().ToString(),
        //         Role = "developer",
        //         Username = "einharan",
        //         Password = "248163264",
        //         EmailAddress = "einharan.protonmail.com",
        //         FirstName = "Vance",
        //         LastName = "Einharan",
        //         DateOfBirthFormat = new DateOnly(1996, 8, 19),
        //         ContactNumber = "-",
        //         PictureProfile = "einharan.png"
        //     };

        //     await Authenticator!.LogInAsync(user);

        // }

    }
}