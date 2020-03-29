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
    class CopyDAO {
        static string strConn = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
        public static DataTable GetDataTable() {
            string cmd = "select * from Copy";
            return DAO.GetDataTable(cmd);
        }
        public static int GetCopyNumberMax() {
            DataTable dt = GetDataTable();
            if (dt.Rows.Count == 0)
                return 0;
            else
                return (int) (dt.Compute("max(copyNumber)", ""));
        }
        public static int GetSequenceNumberMax(int bookNumber) {
            DataTable dt = GetDataTable();
            if (dt.Rows.Count == 0)
                return 0;
            else
                return (int) (dt.Compute("max(sequenceNumber)", ""));
        }
        public static Copy GetCopy(int copyNumber) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Copy where copyNumber = @cn", conn);
            cmd.Parameters.AddWithValue("@cn", copyNumber);
            Copy c = new Copy();
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    c.BookNumber = (int) reader["bookNumber"];
                    c.SequenceNumber = (int) reader["sequenceNumber"];
                    c.Type = char.Parse(reader["type"].ToString());
                    c.Price = (double) reader["price"];
                }
            }
            conn.Close();
            return c;
        }
        public static bool Insert(Copy b) {
            SqlCommand cmd = new SqlCommand("insert into copy(copyNumber,bookNumber, sequenceNumber, type, price)" +
                    "values (@copyNumber, @bookNumber, @sequenceNumber, @type,@price)");
            cmd.Parameters.AddWithValue("@copyNumber", b.CopyNumber);
            cmd.Parameters.AddWithValue("@bookNumber", b.BookNumber);
            cmd.Parameters.AddWithValue("@sequenceNumber", b.SequenceNumber);
            cmd.Parameters.AddWithValue("@type", b.Type);
            cmd.Parameters.AddWithValue("@price", b.Price);
            return DAO.UpdateTable(cmd);
        }

        public static bool Update(Copy b) {
            SqlCommand cmd = new SqlCommand("update copy set " +
                "type=@type,price=@price " +
                "where copyNumber =@copyNumber");
            cmd.Parameters.AddWithValue("@copyNumber", b.CopyNumber);
            cmd.Parameters.AddWithValue("@type", b.Type);
            cmd.Parameters.AddWithValue("@price", b.Price);

            return DAO.UpdateTable(cmd);
        }
        public static bool Update1(int b, string type) {

            SqlCommand cmd = new SqlCommand("update copy set " +
                " type =@type " +
                " where copyNumber =@copyNumber");

            cmd.Parameters.AddWithValue("@copyNumber", b);
            cmd.Parameters.AddWithValue("@type", type);
            // MessageBox.Show(b + "");
            return DAO.UpdateTable(cmd);
        }

        public static Boolean Delete(int copyNumber) {
            SqlCommand cmd = new SqlCommand("delete copy where copyNumber=@copyNumber");
            cmd.Parameters.AddWithValue("@copyNumber", copyNumber);
            return DAO.UpdateTable(cmd);
        }

        public bool UpdateType(int copyNumber) {
            SqlCommand cmd = new SqlCommand("update copy set " +
                "type=@type " +
                "where copyNumber = @copyNumber");
            cmd.Parameters.AddWithValue("@copyNumber", copyNumber);
            cmd.Parameters.AddWithValue("@type", "A");

            return DAO.UpdateTable(cmd);
        }
    }
}
