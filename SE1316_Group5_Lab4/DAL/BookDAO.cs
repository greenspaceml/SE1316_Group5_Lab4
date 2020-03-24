using SE1316_Group5_Lab4.DTL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE1316_Group5_Lab4.DAL {
    class BookDAO {
        public static DataTable GetDataTable() {
            string cmd = "select * from Book";
            return DAO.GetDataTable(cmd);
        }

        public static int GetBookNumberMax() {
            DataTable dt = GetDataTable();
            if (dt.Rows.Count == 0) return 0;
            else return (int)(dt.Compute("max(bookNumber)", ""));
        }

        public static bool Insert(Book b) {
            SqlCommand cmd = new SqlCommand("insert into book(title, authors, publisher)" +
                    "values (@title, @authors, @publisher)");
            cmd.Parameters.AddWithValue("@title", b.Title);
            cmd.Parameters.AddWithValue("@authors", b.Authors);
            cmd.Parameters.AddWithValue("@publisher", b.Publisher);
            return DAO.UpdateTable(cmd);
        }

        public static bool Update(Book b) {
            SqlCommand cmd = new SqlCommand("update Book set title=@title, authors = @authors, publisher = @publisher where bookNumber=@bookNumber");
            cmd.Parameters.AddWithValue("@bookNumber", b.BookNumber);
            cmd.Parameters.AddWithValue("@title", b.Title);
            cmd.Parameters.AddWithValue("@authors", b.Authors);
            cmd.Parameters.AddWithValue("@publisher", b.Publisher);
            return DAO.UpdateTable(cmd);
        }

        public static Boolean Delete(int bookNumber) {
            SqlCommand cmd = new SqlCommand("delete Book where bookNumber=@bookNumber");
            cmd.Parameters.AddWithValue("@bookNumber", bookNumber);
            return DAO.UpdateTable(cmd);
        }
    }
}
