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
    public partial class displayVendor : System.Web.UI.Page
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
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }


        protected void vendorGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
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
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
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
        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchVE();
        }
        protected void searchVE()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM vendor_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE vendor_name LIKE '%' + @VName + '%'";
                        cmd.Parameters.AddWithValue("@VName", searchTextBox.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        vendorGridView.DataSource = dt;
                        vendorGridView.DataBind();
                    }
                }
            }
        }
        protected void vendorGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            vendorGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}