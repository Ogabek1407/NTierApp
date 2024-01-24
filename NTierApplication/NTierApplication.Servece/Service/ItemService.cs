using NTierApplication.Core.Errors;
using NTierApplication.Infrastructure.Repository;
using NTierApplication.Infrastructure.Repository.Interface;
using NTierApplication.Servece.Service.Interface;
using NTierAppliction.Domain.Models;
using NTierAppliction.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Servece.Service
{
    public class ItemService : IItemService
    {
        private IItemRepository ItemRepository;


        public ItemService(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        public async ValueTask CreateNewAsync(ItemViewModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                throw new ParameterInvalidException("ItemName cannot be empty");
            }
            if (item.ItemType < 0)
            {
                throw new ParameterInvalidException("Item type must be equal or greater than 0");
            }

            var entity = new Item
            {
                ItemDate = item.ItemDate,
                ItemName = item.ItemName,
                ItemType = item.ItemType
            };
            ItemRepository.Insert(entity);
            ItemRepository.SaveChanges();
            item.ItemId = entity.ItemId;

        }

        public async ValueTask DeleteAsync(long itemId)
        {
            var entity = ItemRepository.
                GetAll().
                FirstOrDefault(x =>
                x.ItemId == itemId);
            if (entity is null)
            {
                throw new ParameterInvalidException("no such item");
            }
            ItemRepository.Delete(entity);
            ItemRepository.SaveChanges();
        }

        public async ValueTask<ItemViewModel> GetByIdAsync(long id)
        {
            var result = ItemRepository.GetAll()
               .Select(x => new ItemViewModel
               {
                   ItemId = x.ItemId,
                   ItemDate = x.ItemDate,
                   ItemName = x.ItemName,
                   ItemType = x.ItemType
               })
               .FirstOrDefault(x => x.ItemId == id);

            if (result == null)
            {
                throw new EntryNotFoundException("No such item");
            }
            return result;
        }

        public async ValueTask<ICollection<ItemViewModel>> GetItemsAsync()
        {
            return ItemRepository.GetAll().Select(x => new ItemViewModel
            {
                ItemId = x.ItemId,
                ItemDate = x.ItemDate,
                ItemName = x.ItemName,
                ItemType = x.ItemType
            }).ToList();
        }

        public async ValueTask<ItemPaginationViewModel> GetItemsPag(int offset, int limit)
        {
            if(offset < 0)
            {
                throw new ParameterInvalidException("offset cannot be manif");
            }

            if( limit < 0 )
            { 
                throw new ParameterInvalidException("limit cannot be manif");
            }

            if(limit > 100)
            {
                limit = 100;
            }

            var entityItems = ItemRepository.GetAll().Skip(offset).Take(limit).ToArray();
            var entityCount = ItemRepository.GetAll().Count();
            var entityResult = new ItemPaginationViewModel()
            {
                items = entityItems.Select(x => new ItemViewModel
                {
                    ItemId = x.ItemId,
                    ItemDate = x.ItemDate,
                    ItemName = x.ItemName,
                    ItemType = x.ItemType
                }).ToList(),
                totalCount = entityCount
            };
            return entityResult;
        }

        public async ValueTask UpdateAsync(ItemViewModel item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var data = ItemRepository.GetAll().Where(x => x.ItemId == item.ItemId).FirstOrDefault();

            if (data == null)
            {
                throw new ParameterInvalidException("no such item");
            }
            data.ItemName = item.ItemName;
            data.ItemDate = item.ItemDate;
            data.ItemType = item.ItemType;
            ItemRepository.Update(data);
            ItemRepository.SaveChanges();
        }
    }
}
