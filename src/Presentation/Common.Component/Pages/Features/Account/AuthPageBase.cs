using Microsoft.AspNetCore.Components;
using Domain.Models;
using Infrastructure.Services;

namespace Common.Component.Pages.Features.Account
{
    public class AuthPageBase : ComponentBase
    {
        [Inject]
        protected Authenticator? Authenticator { get; init; }

        [Inject]
        protected NavigationManager? NavigationManager { get; init; }

        protected Guest Guest { get; init; } = new Guest();

        protected async Task TryLoginAsync()
        {
            bool result = await Authenticator!.LogInAsync(Guest);
            if(result)
            {
                NavigationManager!.NavigateTo("/");
            }
        }
    }
}