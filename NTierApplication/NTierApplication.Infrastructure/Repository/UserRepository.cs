using Microsoft.EntityFrameworkCore;
using NTierAppliction.Domain.Models;
using NTierApplication.Infrastructure;
using NTierApplication.Infrastructure.Repository.Interface;
namespace NTierApplication.Infrastructure.Repository
{
    public class UserRepository:IUserRepository
    {
        private MianContext DbContext;

        public UserRepository(MianContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<User> GetAll()
        {
            return DbContext.Users;
        }

        public void Delete(User user)
        {
            if (DbContext.Entry(user).State != EntityState.Deleted)
            {
                DbContext.Users.Remove(user);
            }
        }

        public void Insert(User user)
        {
            DbContext.Users.Add(user);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Update(User user)
        {
            if (DbContext.Entry(user).State != EntityState.Modified)
            {
                DbContext.Entry(user).State = EntityState.Modified;
            }
        }
    }
}
