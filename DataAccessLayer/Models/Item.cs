using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Item : BaseAudit
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
