using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTierApplication.Infrastructure.Repository.Interface;
using NTierApplication.Servece.Service.Interface;
using NTierAppliction.Domain.Models;
using NTierAppliction.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Servece.Service
{
    public class TokenService:ITokenService
    {

        private JwtSecurityTokenHandler securityTokenHandler;
        public TokenService()
        {
            securityTokenHandler = new JwtSecurityTokenHandler();
        }

        public async ValueTask<TokenViewModel> TokenAsync(LoginViewModel loginViewModel)
        {
            string word = "";
            var key = Encoding.ASCII.GetBytes(NTierAppliction.Domain.Models.SecurityKey.key);
            var TokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                            new(ClaimTypes.GivenName,loginViewModel.Email),
                            new(ClaimTypes.Name,loginViewModel.Password),
                    
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            SecurityToken securityToken = securityTokenHandler.CreateToken(TokenDescriptior);
            word = securityTokenHandler.WriteToken(securityToken);
            TokenViewModel token = new TokenViewModel()
            {
                access_token = word,
                expires = 172800,
                refresh_token = null,
                token_type = "Bearer"
            };
            return token;
        }
    }
}
