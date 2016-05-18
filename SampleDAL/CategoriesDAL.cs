using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using Dapper.FastCrud;
using SampleBO;
using SampleBO.Mapping;
using Dapper.FluentMap;

namespace SampleDAL
{
    public class CategoriesDAL : IDisposable
    {
        private readonly string connStr;

        public CategoriesDAL()
        {
            connStr = ConfigurationManager.ConnectionStrings["CommerceConnectionString"].ConnectionString;
        }

        public IEnumerable<Category> GetAllData()
        {
            var sql = @"select * from Categories order by CategoryName";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                var results = conn.Query<Category>(sql, conn);
                return results;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
