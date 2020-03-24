using SE1316_Group5_Lab4.DTL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE1316_Group5_Lab4.DAL {
    class ReserveDAO {
        static string strConn = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
        public Reservation GetFirstReservation(int bookNumber) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Reservation where bookNumber = @bn and id = ( SELECT MIN(id) FROM Reservation )", conn);
            cmd.Parameters.AddWithValue("@bn", bookNumber);
            Reservation r = new Reservation();
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    r.Id = (int) reader["ID"];
                    r.BookNumber = (int) reader["bookNumber"];
                    r.Status = (char) reader["status"];

                    r.BorrowerNumber = (int)reader["borrowerNumber"];
                    r.Date = (DateTime)reader["date"];
                }
            }
            conn.Close();
            return r;
        }

        public void Update(Reservation r) {
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand("update Reservation set borrowerNumber = @borrowerNumber, bookNumber = @bookNumber, date = @date, status = @status where id=@id",conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id", r.Id);
            cmd.Parameters.AddWithValue("@borrowerNumber", r.BorrowerNumber);
            cmd.Parameters.AddWithValue("@bookNumber", r.BookNumber);
            cmd.Parameters.AddWithValue("@date", r.Date);
            cmd.Parameters.AddWithValue("@status", 'A');
            //cmd.Parameters.AddWithValue("@status", r.Status);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable GetDataTable(string memberCode) {
            string cmd = @"SELECT [ID]
      ,[borrowerNumber]
      ,[bookNumber]
      ,[date]
      ,[status]
  FROM [Library].[dbo].[Reservation]
  WHERE [bookNumber] = " + memberCode + @" and [status] = 'R'";
            return DAO.GetDataTable(cmd);
        }

        public string getNameByID(string memberCode) {
            SqlConnection conn = new SqlConnection(strConn);
            // truy vấn SQL
            var commandText = @"SELECT TOP (1) [name]
FROM [Library].[dbo].[Borrower]
Where [borrowerNumber] = " + memberCode;
            // khởi tạo object của lớp SqlCommand
            var command = new SqlCommand(commandText, conn);

            // lưu ý phải mở kết nối trước khi thực thi truy vấn
            conn.Open();

            // thực thi truy vấn để lấy kết quả scalar
            try {
                string name = (string)command.ExecuteScalar();
                return name;
            } catch (SqlException ex) {
                return null;
            }
        }

        public int countCopy(string bookNumber) {

            SqlConnection conn = new SqlConnection(strConn);
            // truy vấn SQL
            var commandText = @"SELECT COUNT(*)
  FROM [Library].[dbo].[Copy]
  where bookNumber = " + bookNumber;
            // khởi tạo object của lớp SqlCommand
            var command = new SqlCommand(commandText, conn);
            conn.Open();
            try {
                int name = (int)command.ExecuteScalar();
                return name;
            } catch (SqlException ex) {
                return 0;
            }
        }

        public int countCopyBorrowed(string bookNumber) {

            SqlConnection conn = new SqlConnection(strConn);
            // truy vấn SQL
            var commandText = @"  SELECT COUNT(*)
  FROM [Library].[dbo].[Copy]
  where [type] = 'B' and bookNumber = " + bookNumber;
            // khởi tạo object của lớp SqlCommand
            var command = new SqlCommand(commandText, conn);
            conn.Open();
            try {
                int name = (int)command.ExecuteScalar();
                return name;
            } catch (SqlException ex) {
                return 0;
            }
        }

        public bool Insert(Reservation b) {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Reservation]"
                                           + "([borrowerNumber]"
                                           + ",[bookNumber]"
                                           + ",[date]"
                                           + ",[status])"
                                        + "VALUES"
                                          + "(@borrowerNumber"
                                          + ", @name"
                                          + ", @sex"
                                          + ", @address)");
            cmd.Parameters.AddWithValue("@borrowerNumber", b.BorrowerNumber);
            cmd.Parameters.AddWithValue("@name", b.BookNumber);
            cmd.Parameters.AddWithValue("@sex", b.Date);
            cmd.Parameters.AddWithValue("@address", b.Status);
            return DAO.UpdateTable(cmd);
        }

        public int getBookNumber(int copyNumber) {
            SqlConnection conn = new SqlConnection(strConn);
            // truy vấn SQL
            var commandText = @"SELECT [bookNumber]
  FROM [Library].[dbo].[Copy]
  where [copyNumber] = " + copyNumber;
            // khởi tạo object của lớp SqlCommand
            var command = new SqlCommand(commandText, conn);
            conn.Open();
            try {
                int name = (int) command.ExecuteScalar();
                return name;
            }
            catch(SqlException ex) {
                return 0;
            }
        }

    }
}
