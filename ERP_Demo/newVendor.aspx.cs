using System;
using System.Collections.Generic;
using System.Configuration;
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
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
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
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
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
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadEditValuesInController()
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
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
                    string sqlqueryE = "SELECT vendor_gst_details FROM vendor_master EXCEPT SELECT vendor_gst_details FROM vendor_master where id = '" + Application["vendorId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT vendor_gst_details FROM vendor_master";
                    Application["queryD"] = sqlqueryN;
                }
                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (vendorGstDetailsTextBox.Text.ToLower().Trim() == reader["vendor_gst_details"].ToString().ToLower().Trim())
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
                if(Application["Duplicate"] is false && Application["editFlag"] is true)
                {
                    string query = "UPDATE vendor_master SET vendor_name='" + vendorNameTextBox.Text.ToString() + "',vendor_address_one ='" + vendorAddressOneTextBox.Text.ToString() + "',vendor_address_two = '" + vendorAddressTwoTextBox.Text.ToString() + "',vendor_contact = '" + vendorContactNoTextBox.Text.ToString() + "',vendor_email = '" + vendorEmailIdTextBox.Text.ToString() + "',vendor_contact_person='" + vendorContactPersonTextBox.Text.ToString() + "',vendor_gst_details='" + vendorGstDetailsTextBox.Text.ToString() + "' WHERE id='" + Application["vendorId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO vendor_master(vendor_id,vendor_name,vendor_address_one,vendor_address_two,vendor_contact,vendor_email,vendor_contact_person,vendor_gst_details)VALUES('" + vendorIdTextBox.Text + "','" + vendorNameTextBox.Text + "','" + vendorAddressOneTextBox.Text + "','" + vendorAddressTwoTextBox.Text + "','" + vendorContactNoTextBox.Text + "','" + vendorEmailIdTextBox.Text + "','" + vendorContactPersonTextBox.Text + "','" + vendorGstDetailsTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor with GST No Already Exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["vendorId"] = null;
                    Application["editFlag"] = null;
                    Application["queryD"] = null;
                    Response.Redirect("~/displayVendor.aspx");
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
            Response.Redirect("~/displayVendor.aspx");
        }
    }
}