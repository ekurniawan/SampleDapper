using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBO
{
    public class ShoppingCart
    {
        public int RecordID { get; set; }
        public string CartID { get; set; }
        public int Quantity { get; set; }
        public int BookID { get; set; }
        public DateTime DateCreated { get; set; }

        public Book Book { get; set; }
    }
}
