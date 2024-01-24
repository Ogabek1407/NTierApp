using Microsoft.EntityFrameworkCore;
using NTierAppliction.Domain.Models;
using NTierApplication.Infrastructure.Repository.Interface;
using NTierApplication.Infrastructure;

namespace NTierApplication.Infrastructure.Repository
{
    public class ItemRepository:IItemRepository
    {
        private MianContext DbContext { get; set; }
        public ItemRepository(MianContext context)
        {
            DbContext = context;
        }

        public IQueryable<Item> GetAll()
        {
            return DbContext.Items;
        }

        public void Delete(Item item)
        {
            if (DbContext.Entry(item).State != EntityState.Deleted)
            {
                DbContext.Items.Remove(item);
            }
        }

        public void Insert(Item item)
        {
            DbContext.Items.Add(item);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Update(Item item)
        {
            if (DbContext.Entry(item).State != EntityState.Modified)
            {
                DbContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
