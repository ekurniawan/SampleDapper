using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SampleBO;
using Dapper.FluentMap.Mapping;

namespace SampleBO.Mapping
{
    public class PengarangMap : EntityMap<Pengarang>
    {
        public PengarangMap()
        {
            Map(p => p.PengarangID).ToColumn("AuthorID");
        }
    }
}
