using NTierAppliction.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Servece.Service.Interface
{
    public interface IItemService
    {
        ValueTask CreateNewAsync(ItemViewModel item);
        ValueTask UpdateAsync(ItemViewModel item);

        ValueTask DeleteAsync(long itemId);
        ValueTask<ICollection<ItemViewModel>> GetItemsAsync();
        ValueTask<ItemViewModel> GetByIdAsync(long id);
        ValueTask<ItemPaginationViewModel> GetItemsPag(int offset, int limit);
    }
}
