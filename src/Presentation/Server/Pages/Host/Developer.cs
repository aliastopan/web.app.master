using Microsoft.AspNetCore.Components;
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
        protected Authenticator? Authenticator { get; init; }

        protected override async Task OnInitializedAsync()
        {
            User user = new User{
                Id = Guid.NewGuid().ToString(),
                Role = "developer",
                Username = "einharan",
                Password = "248163264"
            };

            // string x = user.Username.ToString().UpperCaseFirstCharacter();
            await Authenticator!.LogInAsync(user);

            // var (_, isUserExist) = Context!
            //     .LookUpUser(user.Username, user.Password);

            // if (!isUserExist)
            //     await Context!.InsertUserAsync(user);
            // else
            //     await Authenticator!.LogInAsync(user);
        }
    }
}