using NTierAppliction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Infrastructure.Repository.Interface
{
    public interface IItemRepository
    {
        void Insert(Item item);
        void Update(Item item);
        void Delete(Item item);
        IQueryable<Item> GetAll();
        int SaveChanges();
    }
}
