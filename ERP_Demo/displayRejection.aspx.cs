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
    public partial class displayRejection : System.Web.UI.Page
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
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM rejection_master EXCEPT SELECT * FROM rejection_master WHERE code = 'N/A' order by id OFFSET 1 ROWS", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                rejectionGridView.DataSource = dtbl;
                rejectionGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                rejectionGridView.DataSource = dtbl;
                rejectionGridView.DataBind();
                rejectionGridView.Rows[0].Cells.Clear();
                rejectionGridView.Rows[0].Cells.Add(new TableCell());
                rejectionGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                rejectionGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                rejectionGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }


        protected void rejectionGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            rejectionGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void rejectionGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            rejectionGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void rejectionGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM rejection_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(rejectionGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void rejectionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newRejection.aspx");
        }

        protected void rejectionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Application["rejectionId"] = commandArgs[0];
                bool editFlag = true;
                Application["editFlag"] = editFlag;
                Response.Redirect("~/newRejection.aspx/");
            }
        }
    }
}