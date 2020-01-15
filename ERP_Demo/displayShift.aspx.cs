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
    public partial class displayShift : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM shift_master", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    shiftGridView.DataSource = dtbl;
                    shiftGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    shiftGridView.DataSource = dtbl;
                    shiftGridView.DataBind();
                    shiftGridView.Rows[0].Cells.Clear();
                    shiftGridView.Rows[0].Cells.Add(new TableCell());
                    shiftGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    shiftGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    shiftGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }


        protected void shiftGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            shiftGridView.EditIndex = e.NewEditIndex;
            //temp = customer.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void shiftGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            shiftGridView.EditIndex = -1;
            PopulateGridview();
        }
        protected void shiftGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM shift_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(shiftGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void shiftButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newShiftMaster.aspx");
        }

        protected void shiftGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["shiftId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newShiftMaster.aspx/");
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