using NTierAppliction.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Servece.Service.Interface
{
    public interface ITokenService
    {
        public ValueTask<TokenViewModel> TokenAsync(LoginViewModel loginViewModel);
    }
}
