using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class displayMasterBatch : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
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
            try
            {
                DataTable dtbl = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM masterbatch_master", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    masterbatchGridView.DataSource = dtbl;
                    masterbatchGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    masterbatchGridView.DataSource = dtbl;
                    masterbatchGridView.DataBind();
                    masterbatchGridView.Rows[0].Cells.Clear();
                    masterbatchGridView.Rows[0].Cells.Add(new TableCell());
                    masterbatchGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    masterbatchGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    masterbatchGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        
        protected void masterbatchGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            masterbatchGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void masterbatchGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            masterbatchGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void masterbatchGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM masterbatch_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(masterbatchGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void masterbatchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newMasterBatch.aspx");
        }

        protected void masterbatchGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["masterbatchId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newMasterBatch.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}