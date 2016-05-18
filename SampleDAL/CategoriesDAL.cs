using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;
using Dapper;
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

        public IEnumerable<Category> GetAll()
        {
            var sql = @"select * from Categories order by CategoryName";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                var results = conn.Query<Category>(sql, conn);
                return results;
            }
        }

        public IEnumerable<Category> GetWihPaging(string orderby, int pageIndex = 0, int maximumRow = 500)
        {
            int startRowIndex = pageIndex * maximumRow;
            int endRowIndex = startRowIndex + maximumRow;
            string strSql = string.Format(@" SELECT  
                                                *
                                            FROM    
	                                            ( 
	                                                SELECT 
		                                                ROW_NUMBER() OVER ( ORDER BY {0} ) AS RowNum, 
                                                        CategoryID,CategoryName
		                                            FROM 
			                                            Categories
	                                            ) AS RowConstrainedResult
                                            WHERE   
	                                            RowNum > @startRowIndex
                                                AND RowNum <= @endRowIndex
                                            ORDER BY 
	                                            RowNum",orderby);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                var results = conn.Query<Category>(strSql,
                    new { startRowIndex = startRowIndex, endRowIndex = endRowIndex });
                return results;
            }
        }

        public void Create(Category category)
        {


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"insert into Categories(CategoryID,CategoryName) 
                              values(@CategoryID,@CategoryName)";
                var parameter = new
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName
                };

                try
                {
                    int result = conn.Execute(strSql,parameter);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
