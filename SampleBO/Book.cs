using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBO
{
    public class Book
    {
        public Book()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Reviews = new HashSet<Review>();
            this.ShoppingCarts = new HashSet<ShoppingCart>();
        }

        [Key]
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string ISBN { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }

        public Pengarang Pengarang { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
