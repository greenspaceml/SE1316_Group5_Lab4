using SE1316_Group5_Lab4.DAL;
using SE1316_Group5_Lab4.DTL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE1316_Group5_Lab4.GUI {
    public partial class MemberGUI : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                loadData();
                Session["flag"] = "";
                Session["borrowerNumber"] = "";
            }
        }

        private void loadData() {
            try {
                BorowerDAO bdb = new BorowerDAO();
                DataTable dt = bdb.GetDataTableBorrower();
                GridView1.DataSource = dt;
                GridView1.DataBind();

                lbNumber.Text = coutSizePage() + "";
                btnEdit.Enabled = false;
            }
            catch (Exception ex) {
                lblError.Text = ex.Message;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            //AllowPaging
            GridView1.PageIndex = e.NewPageIndex;
            loadData();
        }

        private int coutSizePage() {
            int count = 0;
            foreach (GridViewRow rows in GridView1.Rows) {
                count++;
            }
            return count;
        }

        private void setEnable(bool state) {
            TextBox1.Enabled = state;
            TextBox2.Enabled = state;
            TextBox3.Enabled = state;
            TextBox4.Enabled = state;
            TextBox5.Enabled = state;
            btnSave.Enabled = state;
            btnCancel.Enabled = state;
        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            setEnable(true);
            Session["flag"] = "add";
        }

        protected void LinkButton2_Click(object sender, EventArgs e) {
            LinkButton btn = sender as LinkButton;

            for (int i = 0; i < GridView1.Rows.Count; i++) {
                if (GridView1.Rows[i].Cells[1].Text.Equals(btn.CommandArgument)) {
                    GridView1.Rows[i].BackColor = Color.Yellow;
                    TextBox1.Text = GridView1.Rows[i].Cells[2].Text.StartsWith("&") ? "" : GridView1.Rows[i].Cells[2].Text;
                    TextBox2.Text = GridView1.Rows[i].Cells[3].Text.StartsWith("&") ? "" : GridView1.Rows[i].Cells[3].Text;
                    TextBox3.Text = GridView1.Rows[i].Cells[4].Text.StartsWith("&") ? "" : GridView1.Rows[i].Cells[4].Text;
                    TextBox4.Text = GridView1.Rows[i].Cells[5].Text.StartsWith("&") ? "" : GridView1.Rows[i].Cells[5].Text;
                    TextBox5.Text = GridView1.Rows[i].Cells[6].Text.StartsWith("&") ? "" : GridView1.Rows[i].Cells[6].Text;
                } else {
                    GridView1.Rows[i].BackColor = Color.White;
                }
            }

            Session["borrowerNumber"] = btn.CommandArgument;
            btnEdit.Enabled = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            setEnable(false);
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            BorowerDAO bdb = new BorowerDAO();
            string name = TextBox1.Text;
            string sex = TextBox2.Text;
            string address = TextBox3.Text;
            string telephone = TextBox4.Text;
            string email = TextBox5.Text;

            bool isCorrect = true;
            if (string.IsNullOrEmpty(name)) {
                lbNameRequired.Text = "The Name must not be empty";
                isCorrect = false;
            } else {
                lbNameRequired.Text = "";
            }

            if (!sex.Equals("F") && !sex.Equals("M")) {
                lbSexRequired.Text = "Sex must be 'F' or 'M'";
                isCorrect = false;
            } else {
                lbSexRequired.Text = "";
            }

            if (!isCorrect) {
                return;
            }

            Borrower borrower = new Borrower();
            borrower.Name = name;
            borrower.Sex = Convert.ToChar(sex);
            borrower.Address = address;
            borrower.Telephone = telephone;
            borrower.Email = email;

            if (Session["flag"].Equals("add")) {
                try {
                    bdb.Insert(borrower);
                }
                catch (Exception ex) {
                    lblError.Text = ex.Message;
                }
            }

            if(Session["flag"].Equals("edit")) {
                borrower.BorrowerNumber = Convert.ToInt32(Session["borrowerNumber"]);
                try {
                    bdb.Update(borrower);
                }
                catch (Exception ex) {
                    lblError.Text = ex.Message;
                }
            }

            loadData();
            setEnable(false);
        }

        protected void LinkButton1_Click(object sender, EventArgs e) {
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            BorowerDAO bdb = new BorowerDAO();

            try {
                bdb.Delete(id);
            }
            catch (Exception ex) {
                lblError.Text = ex.Message;
            }

            loadData();
        }

        protected void btnEdit_Click(object sender, EventArgs e) {
            if (!Session["borrowerNumber"].Equals("")) {
                setEnable(true);
                Session["flag"] = "edit";
            }
        }
    }
}