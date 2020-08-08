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
    public partial class newMasterBatch : System.Web.UI.Page
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
                    String sqlquery = "SELECT * FROM masterbatch_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["masterbatchId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            mbnameTextBox.Text = reader["mb_name"].ToString();
                            mbgradeTextBox.Text = reader["mb_grade"].ToString();
                            mbmfgTextBox.Text = reader["mb_mfg"].ToString();
                            mbcolorTextBox.Text = reader["mb_color"].ToString();
                            mbcolorcodeTextBox.Text = reader["mb_color_code"].ToString();
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
                Application["Duplicate"] = false;
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();
                if (Application["editFlag"] is true)
                {
                    string sqlqueryE = "SELECT mb_name FROM masterbatch_master EXCEPT SELECT mb_name FROM masterbatch_master where id = '" + Application["masterbatchId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT mb_name FROM masterbatch_master";
                    Application["queryD"] = sqlqueryN;
                }

                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (mbnameTextBox.Text.ToLower().Trim() == reader["mb_name"].ToString().ToLower().Trim())
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
                    string query = "UPDATE masterbatch_master SET mb_name='" + mbnameTextBox.Text.ToString() + "',mb_grade='" + mbgradeTextBox.Text.ToString() + "',mb_mfg='" + mbmfgTextBox.Text.ToString() + "',mb_color='" + mbcolorTextBox.Text.ToString() + "',mb_color_code='" + mbcolorcodeTextBox.Text.ToString() + "' WHERE Id='" + Application["masterbatchId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO masterbatch_master(mb_name,mb_grade,mb_mfg,mb_color,mb_color_code)VALUES('" + mbnameTextBox.Text + "','" + mbgradeTextBox.Text + "','" + mbmfgTextBox.Text + "','" + mbcolorTextBox.Text + "','" + mbcolorcodeTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Masterbatch Already Exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["masterbatchId"] = null;
                    Application["editFlag"] = null;
                    Response.Redirect("~/displayMasterBatch.aspx");
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
            Response.Redirect("~/displayMasterBatch.aspx");
        }
    }
}