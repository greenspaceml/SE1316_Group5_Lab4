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
    class BorowerDAO {
        static string strConn = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;
        public static DataTable GetDataTable() {
            string cmd = "select * from Borrower";
            return DAO.GetDataTable(cmd);
        }
        public static Borrower GetBorrower(int borrowerNumber) {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Borrower where borrowerNumber = @bn",conn);
            cmd.Parameters.AddWithValue("@bn", borrowerNumber);
            Borrower b = new Borrower();
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while(reader.Read()){
                    b.Name = (string)reader["name"];
                    //b.Sex = (char)reader["sex"];
                    b.Address = (string)reader["address"];
                    b.Telephone = (string)reader["telephone"];
                    b.Email = (string)reader["email"];
                }
                conn.Close();
            }
            return b;
        }

        public DataTable FilterMembers(int borrowerNumber) {
            string cmd = "select * from Borrower where borrowerNumber = " + borrowerNumber;
            return DAO.GetDataTable(cmd);
        }

        public bool Insert(Borrower b) {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Borrower]"
                                           + "([borrowerNumber]"
                                           + ",[name]"
                                           + ",[sex]"
                                           + ",[address]"
                                           + ",[telephone]"
                                           + ",[email])"
                                        + "VALUES"
                                          + "(@borrowerNumber"
                                          + ", @name"
                                          + ", @sex"
                                          + ", @address"
                                          + ", @telephone"
                                          + ", @email)");
            cmd.Parameters.AddWithValue("@borrowerNumber", b.BorrowerNumber);
            cmd.Parameters.AddWithValue("@name", b.Name);
            cmd.Parameters.AddWithValue("@sex", b.Sex);
            cmd.Parameters.AddWithValue("@address", b.Address);
            cmd.Parameters.AddWithValue("@telephone", b.Telephone);
            cmd.Parameters.AddWithValue("@email", b.Email);
            return DAO.UpdateTable(cmd);
        }

        public bool Update(Borrower b) {
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Borrower]"
                                           + "SET[name] = @name "
                                           + ",[sex] = @sex "
                                           + ",[address] = @address "
                                           + ",[telephone] = @telephone "
                                           + ",[email] = @email "
                                           + "WHERE [borrowerNumber] = @borrowerNumber");

            cmd.Parameters.AddWithValue("@name", b.Name);
            cmd.Parameters.AddWithValue("@sex", b.Sex);
            cmd.Parameters.AddWithValue("@address", b.Address);
            cmd.Parameters.AddWithValue("@telephone", b.Telephone);
            cmd.Parameters.AddWithValue("@email", b.Email);
            cmd.Parameters.AddWithValue("@borrowerNumber", b.BorrowerNumber);
            return DAO.UpdateTable(cmd);
        }

        public bool Delete(int borrowerNumber) {
            SqlCommand cmd = new SqlCommand("delete Borrower where borrowerNumber=@borrowerNumber");
            cmd.Parameters.AddWithValue("@borrowerNumber", borrowerNumber);
            return DAO.UpdateTable(cmd);
        }
    }
}
