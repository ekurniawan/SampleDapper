using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;
using SampleDAL.Models;
using Dapper;

namespace SampleDAL
{
    public class AuthorsDAL
    {
        private string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["CommerceConnectionString"].ConnectionString;
        }

        public IEnumerable<Author> GetAllAuthor()
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<Author> lstAuthor = new List<Author>();
                string strSql = @"select * from Authors order by FirstName";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstAuthor.Add(new Author
                        {
                            AuthorID = Convert.ToInt32(dr[0].ToString()),
                            FirstName = dr[1].ToString(),
                            LastName = dr[2].ToString(),
                            Email = dr[3].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();

                return lstAuthor;
            }
        }

        public IEnumerable<Author> GetAllAuthorDapper()
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                string strSql = @"select * from Authors order by FirstName";
                var results = conn.Query<Author>(strSql);
                return results;
            }
        }

        public void InsertAuthor(Author author)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                string strSql = @"insert into Authors(AuthorID,FirstName,LastName,Email) values(@AuthorID,@FirstName,@LastName,@Email)";
                int result = conn.Execute(strSql, new { AuthorID = author.AuthorID, FirstName = author.FirstName, LastName = author.LastName, Email = author.Email });

            }
        }
    }
}
