using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBO
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int BookID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int? Rating { get; set; }
        public string Comments { get; set; }

        public Book Book { get; set; }
    }
}
