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
    public partial class ReturnGUI : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Session["id"] = "";
            }
        }

        private void loadData() {
            try {
                if (string.IsNullOrEmpty(txtNumber.Text)) {
                    lblError.Text = "Borower number is not null!";
                    return;
                } else if (string.IsNullOrEmpty(txtName.Text)) {
                    lblError.Text = "Name is not null!";
                    return;
                }

                CirculatedCopyDAO cdb = new CirculatedCopyDAO();
                DataTable dt = cdb.GetCopiesOfBorrow(Convert.ToInt32(txtNumber.Text), txtName.Text);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex) {
                lblError.Text = ex.Message;
            }


            if (GridView1.PageCount == 0) {
                lblError.Text = "Not found data";
            } else {
                lblError.Text = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e) {
            loadData();
        }

        protected void LinkButton1_Click(object sender, EventArgs e) {
            LinkButton btn = sender as LinkButton;

            for (int i = 0; i < GridView1.Rows.Count; i++) {
                if (GridView1.Rows[i].Cells[1].Text.Equals(btn.CommandArgument)) {
                    GridView1.Rows[i].BackColor = Color.Yellow;
                } else {
                    GridView1.Rows[i].BackColor = Color.White;
                }
            }

            Session["id"] = btn.CommandArgument;
            btnConfirm.Enabled = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e) {
            try {
                DateTime dateReturn = Calendar1.SelectedDate;
                CirculatedCopyDAO cidb = new CirculatedCopyDAO();
                DateTime dueDate = cidb.GetDueDate(Convert.ToInt32(Session["id"]));
                TimeSpan time = dateReturn.Subtract(dueDate);

                int fineAmount = 0;

                if (time.Days > 0) {
                    fineAmount += time.Days;
                }

                TextBox1.Text = fineAmount + "";
                btnReturn.Enabled = true;
                Session["copyNumber"] = cidb.GetCopyNumber(Convert.ToInt32(Session["id"]));
            } catch(Exception ex) {
                lblError.Text = ex.Message;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e) {
            try {
                DateTime dateReturn = Calendar1.SelectedDate;
                CirculatedCopyDAO cidb = new CirculatedCopyDAO();
                CirculatedCopy cir = new CirculatedCopy();
                cir.Id = Convert.ToInt32(Session["id"]);
                cir.ReturnedDate = dateReturn;
                cir.FineAmount = Convert.ToInt32(TextBox1.Text);
                cidb.UpdateReturn(cir);

                CopyDAO copydb = new CopyDAO();
                copydb.UpdateType(Convert.ToInt32(Session["copyNumber"]));

                btnConfirm.Enabled = false;
                btnReturn.Enabled = false;
                loadData();
            }
            catch (Exception ex) {
                lblError.Text = ex.Message;
            }
        }
    }
}