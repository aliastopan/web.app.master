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

            await Authenticator!.LogInAsync(user);

        }

    }
}