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
    public partial class displayMachine : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM machine_master", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    machineGridView.DataSource = dtbl;
                    machineGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    machineGridView.DataSource = dtbl;
                    machineGridView.DataBind();
                    machineGridView.Rows[0].Cells.Clear();
                    machineGridView.Rows[0].Cells.Add(new TableCell());
                    machineGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    machineGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    machineGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void machineGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            machineGridView.EditIndex = e.NewEditIndex;
            //temp = customer.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void machineGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            machineGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void machineGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM machine_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(machineGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void machineButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newMachineMaster.aspx");
        }

        protected void machineGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["machineId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newMachineMaster.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchMC();
        }
        protected void searchMC()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM machine_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE machine_name LIKE '%' + @machineName + '%'";
                        cmd.Parameters.AddWithValue("@machineName", searchTextBox.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        machineGridView.DataSource = dt;
                        machineGridView.DataBind();
                    }
                }
            }
        }
        protected void machineGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            machineGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}