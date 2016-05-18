using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Dapper;
using Dapper.FastCrud;
using SampleBO;
using SampleBO.Mapping;
using System.Configuration;

using Dapper.FluentMap;
using System.Data;

namespace SampleDAL
{
    public class PengarangDAL : IDisposable
    {
        private readonly string connStr;

        public PengarangDAL()
        {
            connStr = ConfigurationManager.ConnectionStrings["CommerceConnectionString"].ConnectionString;
        }

        public IEnumerable<Pengarang> GetAllData()
        {
            var sql = @"select * from Authors order by FirstName";
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                var results = conn.Query<Pengarang>(sql, conn);
                return results;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
