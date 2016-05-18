using Dapper.FluentMap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleBO.Mapping;

namespace SampleBO
{
    public class Pengarang
    {
        public Pengarang()
        {
            FluentMapper.Initialize(c => c.AddMap(new PengarangMap()));
            this.Books = new HashSet<Book>();
        }

        [Key]
        public int PengarangID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
