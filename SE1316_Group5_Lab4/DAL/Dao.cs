using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SE1316_Group5_Lab4.DAL {
    class DAO {
        static string strConn = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
        static public DataTable GetDataTable(string sqlSelect) {
            try {
                SqlConnection conn = new SqlConnection(strConn);
                SqlDataAdapter da = new SqlDataAdapter(sqlSelect, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            } catch (Exception ex) {
                //MessageBox.Show(ex.Message);
                return null;
            }
        }

        static public bool UpdateTable(SqlCommand cmd) {
            try {
                SqlConnection conn = new SqlConnection(strConn);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;

            } catch (Exception ex) {

                //MessageBox.Show(ex.Message);
                return false;

            }

        }
    }

}
