using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class displayVendor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
                Application["editFlag"] = false;
            }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM vendor_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                vendorGridView.DataSource = dtbl;
                vendorGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                vendorGridView.DataSource = dtbl;
                vendorGridView.DataBind();
                vendorGridView.Rows[0].Cells.Clear();
                vendorGridView.Rows[0].Cells.Add(new TableCell());
                vendorGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                vendorGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                vendorGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }


        protected void vendorGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Application["vendorId"] = commandArgs[0];
                bool editFlag = true;
                Application["editFlag"] = editFlag;
                Response.Redirect("~/newVendor.aspx/");
            }

        }

        protected void vendorGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            vendorGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void vendorGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM vendor_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(vendorGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void vendorButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newVendor.aspx");
        }

    }
}