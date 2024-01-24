using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierAppliction.Domain.ViewModels
{
    public class ItemViewModel
    {
        public string ItemName { get; set; }
        public int? ItemId { get; set; }
        public int ItemType { get; set; }
        public DateTime ItemDate { get; set; }
    }
}
