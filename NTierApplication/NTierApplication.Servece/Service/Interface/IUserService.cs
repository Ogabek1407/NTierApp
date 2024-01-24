using NTierAppliction.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Servece.Service.Interface
{
    public interface IUserService
    {
        ValueTask CreateNewAsync(UserViewModel userView);
        ValueTask UpdateAsync(UserViewModel userView);
        ValueTask DeleteAsync(long userId);
        ValueTask<ICollection<UserViewModel>> GetUsersAsync();
        ValueTask<UserViewModel> GetByIdAsync(long id);
        ValueTask LoginAsync(LoginViewModel login);
        ValueTask<string> PasswordHesh(string password);
    }
}
