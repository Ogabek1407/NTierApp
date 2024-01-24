using NTierAppliction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierAppliction.Domain.ViewModels
{
    public class ItemPaginationViewModel
    {
        public List<ItemViewModel> items { get; set; }
        public int totalCount { get; set; }
    }
}
