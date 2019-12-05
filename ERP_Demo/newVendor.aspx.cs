using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
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
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM parts_master ORDER BY ID DESC", con))
                {
                    //Int32 count = (Int32)cmd.ExecuteScalar();
                    //System.Diagnostics.Debug.WriteLine(count);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["input"] = reader["part_no"];
                    }
                    reader.Close();
                    if (Application["input"] == null)
                        Application["input"] = 0;
                    int input = int.Parse(Regex.Replace(Application["input"].ToString(), "[^0-9]+", string.Empty));
                    vendorIdTextBox.Text = "PBPV#" + (input + 1);

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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            if (Application["editFlag"] is true)
            {
                Application["Duplicate"] = false;
                string query = "UPDATE vendor_master SET vendor_name='" + vendorNameTextBox.Text.ToString() + "',vendor_address_one ='" + vendorAddressOneTextBox.Text.ToString()+ "',vendor_address_two = '" + vendorAddressTwoTextBox.Text.ToString()+ "',vendor_contact = '" + vendorContactNoTextBox.Text.ToString()+ "',vendor_email = '" + vendorEmailIdTextBox.Text.ToString()+ "',vendor_contact_person='" + vendorContactPersonTextBox.Text.ToString()+ "',vendor_gst_details='" + vendorGstDetailsTextBox.Text.ToString()+"' WHERE id='"+Application["vendorId"] +"'";
                SqlCommand cmd = new SqlCommand(query.ToString(), con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sqlquery = "SELECT vendor_gst_details FROM vendor_master";
                //ArrayList al = new ArrayList();
                using (SqlCommand cmmd = new SqlCommand(sqlquery, con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (vendorGstDetailsTextBox.Text.ToLower() == reader["vendor_gst_details"].ToString().ToLower())
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
                if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO vendor_master(vendor_id,vendor_name,vendor_address_one,vendor_address_two,vendor_contact,vendor_email,vendor_contact_person,vendor_gst_details)VALUES('" + vendorIdTextBox.Text + "','" + vendorNameTextBox.Text + "','" + vendorAddressOneTextBox.Text + "','" + vendorAddressTwoTextBox.Text + "','" + vendorContactNoTextBox.Text + "','" + vendorEmailIdTextBox.Text + "','" + vendorContactPersonTextBox.Text + "','" + vendorGstDetailsTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor with GST No Already Exists.')", true);

                }
            }
            
            Application["vendorId"] = null;
            Application["editFlag"] = null;
            con.Close();
            if (Application["Duplicate"] is false)
            {
                Application["Duplicate"] = null;
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