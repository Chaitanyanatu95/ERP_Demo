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
    public partial class newVendor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
                else
                {
                    LoadValuesInController();
                }
            }
        }

        protected void LoadValuesInController()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM vendor_master", con))
                {
                    Int32 count = (Int32)cmd.ExecuteScalar();
                    System.Diagnostics.Debug.WriteLine(count);
                    vendorIdTextBox.Text = "PBPV#" + (count + 1);
                }
            }
        }

        protected void LoadEditValuesInController()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["vendorId"].ToString() + "')", true);
                con.Open();
                String sqlquery = "SELECT * FROM vendor_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["vendorId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        vendorIdTextBox.Text = reader["vendor_id"].ToString();
                        vendorNameTextBox.Text = reader["vendor_name"].ToString();
                        vendorAddressOneTextBox.Text = reader["vendor_address_one"].ToString();
                        vendorAddressTwoTextBox.Text = reader["vendor_address_two"].ToString();
                        vendorContactNoTextBox.Text = reader["vendor_contact"].ToString();
                        vendorEmailIdTextBox.Text = reader["vendor_email"].ToString();
                        vendorContactPersonTextBox.Text = reader["vendor_contact_person"].ToString();
                        vendorGstDetailsTextBox.Text = reader["vendor_gst_details"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (vendorNameTextBox.Text == "" || vendorAddressOneTextBox.Text == "" || vendorAddressTwoTextBox.Text == "" || vendorContactNoTextBox.Text == "" || vendorEmailIdTextBox.Text == "" || vendorContactPersonTextBox.Text == "" || vendorGstDetailsTextBox.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                if (Application["editFlag"] is true)
                {
                    string query = "UPDATE vendor_master SET vendor_name='" + vendorNameTextBox.Text.ToString() + "',vendor_address_one ='" + vendorAddressOneTextBox.Text.ToString()+ "',vendor_address_two = '" + vendorAddressTwoTextBox.Text.ToString()+ "',vendor_contact = '" + vendorContactNoTextBox.Text.ToString()+ "',vendor_email = '" + vendorEmailIdTextBox.Text.ToString()+ "',vendor_contact_person='" + vendorContactPersonTextBox.Text.ToString()+ "',vendor_gst_details='" + vendorGstDetailsTextBox.Text.ToString()+"' WHERE id='"+Application["vendorId"] +"'";
                    Application["vendorQuery"] = query;
                }
                else
                {
                    string query = "INSERT INTO vendor_master(vendor_id,vendor_name,vendor_address_one,vendor_address_two,vendor_contact,vendor_email,vendor_contact_person,vendor_gst_details)VALUES('" + vendorIdTextBox.Text + "','" + vendorNameTextBox.Text + "','" + vendorAddressOneTextBox.Text + "','" + vendorAddressTwoTextBox.Text + "','" + vendorContactNoTextBox.Text + "','" + vendorEmailIdTextBox.Text + "','" + vendorContactPersonTextBox.Text + "','" + vendorGstDetailsTextBox.Text + "')";
                    Application["vendorQuery"] = query;
                }
                SqlCommand cmd = new SqlCommand(Application["vendorQuery"].ToString(), con);
                cmd.ExecuteNonQuery();
                Application["vendorId"] = null;
                Application["vendorQuery"] = null;
                Application["editFlag"] = null;
                con.Close();
                Response.Redirect("~/displayVendor.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayVendor.aspx");
        }
    }
}