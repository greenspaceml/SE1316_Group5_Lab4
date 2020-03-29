using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE1316_Group5_Lab4.DAL;
using SE1316_Group5_Lab4.DTL;

namespace SE1316_Group5_Lab4.DAL {
    class CirculatedCopyDAO {
        static string strConn = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
        public static DataTable GetDataTable() {
            string cmd = "select * from CirculatedCopy";
            return DAO.GetDataTable(cmd);
        }

        public static void Insert(CirculatedCopy cc) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into CirculatedCopy(id, copyNumber, borrowerNumber, dueDate, returnDate, fineAmount)" +
                    "values (@id, @copyNumber, @borrowerNumber, @dueDate, @returnDate, @fineAmount)",conn);
            cmd.Parameters.AddWithValue("@id", cc.Id);
            cmd.Parameters.AddWithValue("@copyNumber", cc.CopyNumber);
            cmd.Parameters.AddWithValue("@borrowerNumber", cc.BorrowerNumber);
            cmd.Parameters.AddWithValue("@dueDate", cc.DueDate);
            cmd.Parameters.AddWithValue("@returnDate", cc.ReturnedDate);
            cmd.Parameters.AddWithValue("@fineAmounteDate", cc.FineAmount);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static DataTable GetBorrowedCopies(int borrowerNumber) {
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand("select * from CirculatedCopy where borrowerNumber = @bn and returnedDate is null", conn);
            cmd.Parameters.AddWithValue("@bn", borrowerNumber);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static void Update(CirculatedCopy cc) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update CirculatedCopy set returnedDate=@returnedDate, fineAmount=@fineAmount " +
                    "where id=@id", conn);
            cmd.Parameters.AddWithValue("@id", cc.Id);
            cmd.Parameters.AddWithValue("@returnedDate", cc.ReturnedDate);
            cmd.Parameters.AddWithValue("@fineAmount", cc.FineAmount);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable GetCopiesOfBorrow (int borrowerNumber, string name) {
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(@"SELECT        CirculatedCopy.*
                            FROM Borrower INNER JOIN
                                 CirculatedCopy ON Borrower.borrowerNumber = CirculatedCopy.borrowerNumber
                            where Borrower.borrowerNumber = @bn and Borrower.name like @name and returnedDate is null", conn);
            cmd.Parameters.AddWithValue("@bn", borrowerNumber);
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DateTime GetDueDate(int id) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT [dueDate]
                                              FROM[Library].[dbo].[CirculatedCopy]
                                              WHERE ID = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);

            DateTime date = new DateTime();

            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    date = Convert.ToDateTime(reader["dueDate"]);
                }
                conn.Close();
            }

            return date;
        }

        public int GetCopyNumber(int id) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT [copyNumber]
                                              FROM[Library].[dbo].[CirculatedCopy]
                                              WHERE ID = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);

            int copyNumber = 0;

            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    copyNumber = Convert.ToInt32(reader["copyNumber"]);
                }
                conn.Close();
            }

            return copyNumber;
        }

        public void UpdateReturn(CirculatedCopy cc) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update CirculatedCopy set returnedDate=@returnedDate, fineAmount=@fineAmount " +
                    "where id=@id", conn);
            cmd.Parameters.AddWithValue("@id", cc.Id);
            cmd.Parameters.AddWithValue("@returnedDate", cc.ReturnedDate);
            cmd.Parameters.AddWithValue("@fineAmount", cc.FineAmount);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
