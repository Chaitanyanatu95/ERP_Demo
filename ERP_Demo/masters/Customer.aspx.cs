using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo.masters
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM customer_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                customer.DataSource = dtbl;
                customer.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                customer.DataSource = dtbl;
                customer.DataBind();
                customer.Rows[0].Cells.Clear();
                customer.Rows[0].Cells.Add(new TableCell());
                customer.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                customer.Rows[0].Cells[0].Text = "No Data Found ..!";
                customer.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void customer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO customer_master (customer_name,customer_address,customer_contact,customer_email) VALUES (@customer_name,@customer_address,@customer_contact,@customer_email)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@customer_name", (customer.FooterRow.FindControl("txtMnameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@customer_address", (customer.FooterRow.FindControl("txtSizeFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@customer_contact", (customer.FooterRow.FindControl("txtQuantityFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@customer_email", (customer.FooterRow.FindControl("txtColorFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview();
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void customer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            customer.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void customer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            customer.EditIndex = -1;
            PopulateGridview();
        }

        protected void customer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "UPDATE customer_master SET customer_name=@customer_name,customer_address=@customer_address,customer_contact=@customer_contact,customer_email=@customer_email WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@customer_name", (customer.Rows[e.RowIndex].FindControl("txtMName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_address", (customer.Rows[e.RowIndex].FindControl("txtMSize") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_contact", (customer.Rows[e.RowIndex].FindControl("txtMQuantity") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_email", (customer.Rows[e.RowIndex].FindControl("txtMColor") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(customer.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    customer.EditIndex = -1;
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void customer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM customer_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(customer.DataKeys[e.RowIndex].Value.ToString()));
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
    }
}