using Microsoft.AspNetCore.Components;
using Domain.Models;
using Infrastructure.Services;

namespace Common.Component.Pages.Features.Account
{
    public class AuthPageBase : ComponentBase
    {
        [Inject]
        protected Authenticator? Authenticator { get; init; }

        protected User Guest { get; init; } = new User();


    }
}