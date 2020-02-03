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
    public partial class displayDownTimeCode : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM down_time_master EXCEPT SELECT * FROM down_time_master WHERE down_time_type='N/A'", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    downtimeGridView.DataSource = dtbl;
                    downtimeGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    downtimeGridView.DataSource = dtbl;
                    downtimeGridView.DataBind();
                    downtimeGridView.Rows[0].Cells.Clear();
                    downtimeGridView.Rows[0].Cells.Add(new TableCell());
                    downtimeGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    downtimeGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    downtimeGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblDownTimeCodeSuccessMessage.Text = "";
                lblDownTimeCodeErrorMessage.Text = ex.Message;
            }
        }

        protected void downtimeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["downTimeCodeId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newDownTimeCode.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblDownTimeCodeSuccessMessage.Text = "";
                lblDownTimeCodeErrorMessage.Text = ex.Message;
            }
        }

        protected void downtimeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            downtimeGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void downtimeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM down_time_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(downtimeGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblDownTimeCodeSuccessMessage.Text = "Selected Record Deleted";
                    lblDownTimeCodeErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblDownTimeCodeSuccessMessage.Text = "";
                lblDownTimeCodeErrorMessage.Text = ex.Message;
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchDownTimeCode();
        }

        protected void searchDownTimeCode()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM down_time_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE down_time_type LIKE '%' + @DownTime + '%'";
                        cmd.Parameters.AddWithValue("@DownTime", searchTextBox.Text.Trim());
                    }
                    sql += " EXCEPT SELECT * FROM down_time_master WHERE down_time_type='N/A'";
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        downtimeGridView.DataSource = dt;
                        downtimeGridView.DataBind();
                    }
                }
            }
        }

        protected void downTimeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newDownTimeCode.aspx");
        }

        protected void downtimeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            downtimeGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}