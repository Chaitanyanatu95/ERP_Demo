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
    public partial class displayPackaging : System.Web.UI.Page
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
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM packaging_master EXCEPT SELECT * FROM packaging_master WHERE packaging_type = 'N/A' order by id OFFSET 1 ROWS", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                packagingGridView.DataSource = dtbl;
                packagingGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                packagingGridView.DataSource = dtbl;
                packagingGridView.DataBind();
                packagingGridView.Rows[0].Cells.Clear();
                packagingGridView.Rows[0].Cells.Add(new TableCell());
                packagingGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                packagingGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                packagingGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }


        protected void packagingGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            packagingGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void packagingGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            packagingGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void packagingGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM packaging_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(packagingGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void packagingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newPackaging.aspx");
        }

        protected void packagingGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Application["packagingId"] = commandArgs[0];
                bool editFlag = true;
                Application["editFlag"] = editFlag;
                Response.Redirect("~/newPackaging.aspx/");
            }
        }
    }
}