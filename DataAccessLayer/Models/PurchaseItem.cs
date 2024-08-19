using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PurchaseItem : BaseAudit
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public int purchase_id { get; set; }
        public decimal qty { get; set; }

        public Item? Item { get; set; }
        public Purchase? Purchase { get; set; }
    }
}
