using Microsoft.AspNetCore.Mvc;
using NTierApplication.Core.Errors;
using NTierApplication.Servece.Service;
using NTierApplication.Servece.Service.Interface;
using NTierAppliction.Domain.ViewModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Principal;

namespace NTierApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController
    {
        private readonly ITokenService TokenService;
        public readonly IUserService UserService;
        public LoginController(IUserService userService,ITokenService tokenService)
        {
            this.UserService = userService;
            this.TokenService = tokenService;
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(OperationId = "login")]
        public async ValueTask<TokenViewModel> Login(LoginViewModel loginViewModel)
        {
            Thread.Sleep(1000);
            await this.UserService.LoginAsync(loginViewModel);     
            return await this.TokenService.TokenAsync(loginViewModel);
        }

    }
}
