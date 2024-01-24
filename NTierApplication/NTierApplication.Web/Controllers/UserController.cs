using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NTierApplication.Servece.Service;
using NTierApplication.Servece.Service.Interface;
using NTierAppliction.Domain.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserService UserService;
        public UserController(IUserService userService)
        {
            this.UserService = userService;
        }


        [HttpGet]
        [Route("")]
        [SwaggerOperation(OperationId = "GetAll")]
        public async ValueTask<ICollection<UserViewModel>> GetAllAsync()
        {
            return await UserService.GetUsersAsync();
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(OperationId = "create")]
        public async ValueTask<UserViewModel> CreateNew(UserViewModel userViewModel)
        {
            Thread.Sleep(1000);
            await UserService.CreateNewAsync(userViewModel);
            return userViewModel;
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(OperationId = "Delete")]
        public async ValueTask<UserViewModel> Delete(long id)
        {
            var user = await UserService.GetByIdAsync(id);
            UserService.DeleteAsync(id);
            return user;
        }



    }
}
