using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newPackaging : System.Web.UI.Page
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
                    con.Open();
                    String sqlquery = "SELECT * FROM packaging_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["packagingId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            packagingTypeTextBox.Text = reader["packaging_type"].ToString();
                            sizeTextBox.Text = reader["size"].ToString();
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
                    string sqlqueryE = "SELECT packaging_type FROM packaging_master EXCEPT SELECT packaging_type FROM packaging_master where id = '" + Application["packagingId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT packaging_type FROM packaging_master";
                    Application["queryD"] = sqlqueryN;
                }

                //ArrayList al = new ArrayList();
                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                    {
                        SqlDataReader reader = cmmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (packagingTypeTextBox.Text.ToLower().Trim() == reader["packaging_type"].ToString().ToLower().Trim())
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
                    string query = "UPDATE packaging_master SET packaging_type='" + packagingTypeTextBox.Text.ToString() + "',size='" + sizeTextBox.Text.ToString() + "' WHERE Id='" + Application["packagingId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO packaging_master(packaging_type,size)VALUES('" + packagingTypeTextBox.Text + "','" + sizeTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Packaging type already exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["queryD"] = null;
                    Application["packagingId"] = null;
                    Application["editFlag"] = null;
                    Response.Redirect("~/displayPackaging.aspx");
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
            Response.Redirect("~/displayPackaging.aspx");
        }
    }
}