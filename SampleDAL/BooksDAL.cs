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
    public class BooksDAL : IDisposable
    {
        private readonly string connStr;

        public BooksDAL()
        {
            connStr = ConfigurationManager.ConnectionStrings["CommerceConnectionString"].ConnectionString;
        }

        /*public IEnumerable<Book> GetAll(bool withPengarang=false, bool withCategory=false)
        {
            IEnumerable<Book> books;
            Pengarang pengarang;
            Category category;

            string strSql = @"select * from Books order by Title";
            using (var conn = new SqlConnection(connStr))
            {
                books = conn.Query<Book>(strSql);
                foreach (var b in books)
                {
                    if (withPengarang)
                    {
                        strSql = @"select * from Authors where AuthorID=@AuthorID";
                        pengarang = conn.Query<Pengarang>(strSql, new { AuthorID = b.AuthorID }).SingleOrDefault();
                        b.Pengarang = pengarang;
                    }
                    if(withCategory)
                    {
                        strSql = @"select * from Categories where CategoryID=@CategoryID";
                        category = conn.Query<Category>(strSql, new { CategoryID = b.CategoryID }).SingleOrDefault();
                        b.Category = category;
                    }
                }
            }
            return books;
        }*/


        public IEnumerable<Book> GetAll(bool withAll=false, 
            bool withPengarang = false, bool withCategory = false)
        {
            string strSql = string.Empty;
            IEnumerable<Book> books = null;
            using (var conn = new SqlConnection(connStr))
            {
                if (withPengarang)
                {
                    strSql = @"select * from Books left join Authors 
                            on Books.AuthorID=Authors.AuthorID 
                            order by Books.Title";
                    books = conn.Query<Book, Pengarang, Book>(strSql, (b, p) =>
                    {
                        b.Pengarang = p;
                        return b;
                    }, splitOn: "AuthorID");
                }
                else if(withCategory)
                {
                    strSql = @"select * from Books left join Categories 
                            on Books.CategoryID=Categories.CategoryID 
                            order by Books.Title";
                    books = conn.Query<Book, Category, Book>(strSql, (b, c) =>
                    {
                        b.Category = c;
                        return b;
                    },splitOn:"CategoryID");
                }
                else if(withAll)
                {
                    strSql = @"select * from Books left join Authors 
                            on Books.AuthorID=Authors.AuthorID 
                            left join Categories on Books.CategoryID=Categories.CategoryID 
                            order by Books.Title";
                    books = conn.Query<Book, Pengarang, Category, Book>(strSql, (b, p, c) =>
                    {
                        b.Pengarang = p;
                        b.Category = c;
                        return b;
                    }, splitOn: "AuthorID,CategoryID");
                }
                else
                {
                    strSql = @"select * from Books 
                            order by Books.Title";
                    books = conn.Query<Book>(strSql);
                }

                return books;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
