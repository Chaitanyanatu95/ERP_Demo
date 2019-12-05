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
    public partial class displayCustomer : System.Web.UI.Page
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


        protected void customer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string temp;
            //customerGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            //PopulateGridview();
            if (e.CommandName == "Edit")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Application["custId"] = commandArgs[0];
                //Application["custId"] = commandArgs[1];
                bool editFlag = true;
                Application["editFlag"] = editFlag;
                Response.Redirect("~/newCustomer.aspx/");
            }

        }

        protected void customer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            customerGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void customer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "UPDATE customer_master SET customer_name=@customer_name,customer_address_one=@customer_address_one,customer_address_two=@customer_address_two,customer_contact=@customer_contact,customer_email=@customer_email,customer_contact_person=@customer_contact_person,customer_gst_details=@customer_gst_details WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@customer_name", (customerGridView.Rows[e.RowIndex].FindControl("txtCName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_address_one", (customerGridView.Rows[e.RowIndex].FindControl("txtCAddOne") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_address_two", (customerGridView.Rows[e.RowIndex].FindControl("txtCAddTwo") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_contact", (customerGridView.Rows[e.RowIndex].FindControl("txtCContact") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_email", (customerGridView.Rows[e.RowIndex].FindControl("txtCEmail") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_contact_person", (customerGridView.Rows[e.RowIndex].FindControl("txtCContactPerson") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@customer_gst_details", (customerGridView.Rows[e.RowIndex].FindControl("txtCGstDetails") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(customerGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    customerGridView.EditIndex = -1;
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

    }
}