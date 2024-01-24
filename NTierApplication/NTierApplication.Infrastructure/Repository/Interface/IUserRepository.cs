using NTierAppliction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
        IQueryable<User> GetAll();
        int SaveChanges();
    }
}
