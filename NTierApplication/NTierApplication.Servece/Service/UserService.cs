using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTierApplication.Core.Errors;
using NTierApplication.Infrastructure.Repository;
using NTierApplication.Infrastructure.Repository.Interface;
using NTierApplication.Servece.Service.Interface;
using NTierAppliction.Domain.Models;
using NTierAppliction.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Servece.Service
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async ValueTask CreateNewAsync(UserViewModel userView)
        {
            if (userView == null)
            {
                throw new ArgumentNullException(nameof(userView));
            }
            if (string.IsNullOrWhiteSpace(userView.UserEmail) || string.IsNullOrWhiteSpace(userView.Password))
            {
                throw new ParameterInvalidException("UserEmail cannot be empty");
            }
            var data = UserRepository.
                GetAll().
                FirstOrDefault(x => 
                x.UserEmail == userView.UserEmail);
            if (data is not null)
            {
                throw new ParameterInvalidException("email was previously used");
            }
            
            if (userView.Password.Length < 8)
            {
                throw new ParameterInvalidException("Item type must be equal or greater than 0");
            }

            string password =await PasswordHesh(userView.Password);

            var entity = new User
            {
                UserEmail = userView.UserEmail,
                UserName = userView.UserName,
                Password = password,
            };
            UserRepository.Insert(entity);
            UserRepository.SaveChanges();
            userView.UserId = entity.UserId;
        }

        public async ValueTask DeleteAsync(long userId)
        {
            var entity = UserRepository.
                GetAll().
                FirstOrDefault(x =>
                x.UserId == userId);
            if (entity is null)
            {
                throw new ParameterInvalidException("no such item");
            }
            UserRepository.Delete(entity);
            UserRepository.SaveChanges();
        }

        public async ValueTask<UserViewModel> GetByIdAsync(long id)
        {
            var result = UserRepository.GetAll()
               .Select(x => new UserViewModel
               {
                   UserId = x.UserId,
                   UserName = x.UserName,
                   UserEmail = x.UserEmail,
                   Password = x.Password
               })
               .FirstOrDefault(x => x.UserId == id);

            if (result == null)
            {
                throw new EntryNotFoundException("No such User");
            }
            return result;
        }

        public async ValueTask<ICollection<UserViewModel>> GetUsersAsync()
        {
            return UserRepository.GetAll().Select(x => new UserViewModel
            {
                UserId = x.UserId,
                UserName = x.UserName,
                UserEmail = x.UserEmail,
                Password = x.Password
            }).ToList();
        }

        public async ValueTask UpdateAsync(UserViewModel userView)
        {
            if (userView is null)
            {
                throw new ArgumentNullException(nameof(userView));
            }
            if (userView.UserId == null)
            {
                throw new ParameterInvalidException("no such item");
            }
            var data = UserRepository.GetAll().FirstOrDefault(x => x.UserId == userView.UserId);
            if (data is null)
            {
                throw new ParameterInvalidException("no such item");
            }
            string password = await PasswordHesh(userView.Password);

            data.UserName = userView.UserName;
            data.UserEmail = userView.UserEmail;
            data.Password = password;
            UserRepository.Update(data);
            UserRepository.SaveChanges();
        }

        public async ValueTask LoginAsync(LoginViewModel login)
        {
            if(string.IsNullOrWhiteSpace(login.Email))
            {
                throw new ParameterInvalidException(nameof(login)); 
            }
            if(string.IsNullOrWhiteSpace(login.Password))
            {
                throw new ParameterInvalidException(nameof(login.Password));
            }
            string password = await PasswordHesh(login.Password);
            Console.WriteLine(password);
            var entity = UserRepository.GetAll().
                Where( x => 
                x.Password == password &&
                x.UserEmail == login.Email).
                FirstOrDefault();

            if (entity is null)
            {
                throw new ParameterInvalidException(nameof(entity));
            }
            Console.WriteLine(entity.UserId);
        }

        public async ValueTask<string> PasswordHesh(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
