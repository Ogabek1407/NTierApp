using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierAppliction.Domain.Models
{
    public class Item
    {
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public DateTime ItemDate { get; set; }
    }
}
