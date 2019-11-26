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
    public partial class newCustomer : System.Web.UI.Page
    {
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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
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
                        customerEmailIdTextBox.Text = reader["customer_email"].ToString();
                        customerContactPersonTextBox.Text = reader["customer_contact_person"].ToString();
                        customerGstDetailsTextBox.Text = reader["customer_gst_details"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            if (Application["editFlag"] is true)
            {
                string query = "UPDATE customer_master SET customer_name='" + customerNameTextBox.Text.ToString() + "',customer_address_one ='"+customerAddressOneTextBox.Text.ToString()+"',customer_address_two = '"+customerAddressTwoTextBox.Text.ToString()+"',customer_contact = '"+customerContactNoTextBox.Text.ToString()+"',customer_email = '"+customerEmailIdTextBox.Text.ToString()+"',customer_contact_person='"+customerContactPersonTextBox.Text.ToString()+"',customer_gst_details='"+customerGstDetailsTextBox.Text.ToString()+"' WHERE id='"+Application["custId"]+"'";
                Application["custQuery"] = query;
            }
            else
            {
                string query = "INSERT INTO customer_master(customer_name,customer_address_one,customer_address_two,customer_contact,customer_email,customer_contact_person,customer_gst_details)VALUES('" + customerNameTextBox.Text + "','" + customerAddressOneTextBox.Text + "','" + customerAddressTwoTextBox.Text + "','" + customerContactNoTextBox.Text + "','" + customerEmailIdTextBox.Text + "','" + customerContactPersonTextBox.Text + "','" + customerGstDetailsTextBox.Text + "')";
                Application["custQuery"] = query;
            }
            SqlCommand cmd = new SqlCommand(Application["custQuery"].ToString(), con);
            cmd.ExecuteNonQuery();
            Application["custId"] = null;
            Application["custQuery"] = null;
            Application["editFlag"] = null;
            con.Close();
            Response.Redirect("~/displayCustomer.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayCustomer.aspx");
        }
    }
}