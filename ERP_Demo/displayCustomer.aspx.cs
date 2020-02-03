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
    public partial class displayCustomer : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM customer_master", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    customerGridView.DataSource = dtbl;
                    customerGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    customerGridView.DataSource = dtbl;
                    customerGridView.DataBind();
                    customerGridView.Rows[0].Cells.Clear();
                    customerGridView.Rows[0].Cells.Add(new TableCell());
                    customerGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    customerGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    customerGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }


        protected void customer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["custId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newCustomer.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void customer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            customerGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void customer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM customer_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(customerGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void customerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newCustomer.aspx");
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchCustomer();
        }

        protected void searchCustomer()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM customer_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE customer_name LIKE '%' + @CustName + '%'";
                        cmd.Parameters.AddWithValue("@CustName", searchTextBox.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        customerGridView.DataSource = dt;
                        customerGridView.DataBind();
                    }
                }
            }
        }

        protected void customerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            customerGridView.PageIndex = e.NewPageIndex;
            //this.searchCustomer();
        }
    }
}