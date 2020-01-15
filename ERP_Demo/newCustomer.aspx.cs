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
    public partial class newCustomer : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
            }
        }

        protected void LoadEditValuesInController()
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["custId"].ToString() + "')", true);
                    con.Open();
                    String sqlquery = "SELECT * FROM customer_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["custId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            customerNameTextBox.Text = reader["customer_name"].ToString();
                            customerAddressOneTextBox.Text = reader["customer_address_one"].ToString();
                            customerAddressTwoTextBox.Text = reader["customer_address_two"].ToString();
                            customerContactNoTextBox.Text = reader["customer_contact"].ToString();
                            customerEmailIdTextBox.Text = reader["customer_email"].ToString().ToLower();
                            customerContactPersonTextBox.Text = reader["customer_contact_person"].ToString();
                            customerGstDetailsTextBox.Text = reader["customer_gst_details"].ToString();
                        }
                        reader.Close();
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();
                if (Application["editFlag"] is true)
                {
                    string sqlqueryE = "SELECT customer_gst_details FROM customer_master EXCEPT SELECT customer_gst_details FROM customer_master where id = '" + Application["custId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT customer_gst_details FROM customer_master";
                    Application["queryD"] = sqlqueryN;

                }

                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (customerGstDetailsTextBox.Text.ToLower().Trim() == reader["customer_gst_details"].ToString().ToLower().Trim())
                        {
                            Application["Duplicate"] = true;
                            break;
                        }
                        else
                        {
                            Application["Duplicate"] = false;
                        }
                    }
                    reader.Close();
                }
                if (Application["Duplicate"] is false && Application["editFlag"] is true)
                {
                    string query = "UPDATE customer_master SET customer_name='" + customerNameTextBox.Text.ToString() + "',customer_address_one ='" + customerAddressOneTextBox.Text.ToString() + "',customer_address_two = '" + customerAddressTwoTextBox.Text.ToString() + "',customer_contact = '" + customerContactNoTextBox.Text.ToString() + "',customer_email = '" + customerEmailIdTextBox.Text.ToString() + "',customer_contact_person='" + customerContactPersonTextBox.Text.ToString() + "',customer_gst_details='" + customerGstDetailsTextBox.Text.ToString() + "' WHERE id='" + Application["custId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO customer_master(customer_name,customer_address_one,customer_address_two,customer_contact,customer_email,customer_contact_person,customer_gst_details)VALUES('" + customerNameTextBox.Text + "','" + customerAddressOneTextBox.Text + "','" + customerAddressTwoTextBox.Text + "','" + customerContactNoTextBox.Text + "','" + customerEmailIdTextBox.Text.ToLower() + "','" + customerContactPersonTextBox.Text + "','" + customerGstDetailsTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Customer with GST No Already Exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["custId"] = null;
                    Application["queryD"] = null;
                    Application["editFlag"] = null;
                    Application["Duplicate"] = null;
                    Response.Redirect("~/displayCustomer.aspx");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayCustomer.aspx");
        }
    }
}