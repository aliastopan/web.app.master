using Microsoft.AspNetCore.Components;
using Infrastructure.Persistence;
using Domain.Models;

namespace Server.Pages.Host
{
    public partial class Developer
    {
        [Inject]
        public AppDbContext? Context { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User user = new User{
                Id = Guid.NewGuid().ToString(),
                Role = "developer",
                Username = "einharan",
                Password = "248163264"
            };

            var (_, isUserExist) = Context!
                .LookUpUser(user.Username, user.Password);

            if (!isUserExist)
            {
                await Context!.InsertUserAsync(user);
            }
        }
    }
}