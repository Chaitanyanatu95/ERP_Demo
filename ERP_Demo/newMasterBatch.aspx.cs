using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newMasterBatch : System.Web.UI.Page
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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            if (Application["editFlag"] is true)
            {
                Application["Duplicate"] = false;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                string query = "UPDATE masterbatch_master SET mb_name='" + mbnameTextBox.Text.ToString() + "',mb_grade='" + mbgradeTextBox.Text.ToString() + "',mb_mfg='" + mbmfgTextBox.Text.ToString() + "',mb_color='" + mbcolorTextBox.Text.ToString() + "',mb_color_code='" + mbcolorcodeTextBox.Text.ToString() + "' WHERE Id='" + Application["masterbatchId"] + "'";
                SqlCommand cmd = new SqlCommand(query.ToString(), con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sqlquery = "SELECT mb_name FROM masterbatch_master";
                //ArrayList al = new ArrayList();
                using (SqlCommand cmmd = new SqlCommand(sqlquery, con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (mbnameTextBox.Text == reader["mb_name"].ToString())
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
                    string query = "INSERT INTO masterbatch_master(mb_name,mb_grade,mb_mfg,mb_color,mb_color_code)VALUES('" + mbnameTextBox.Text + "','" + mbgradeTextBox.Text + "','" + mbmfgTextBox.Text + "','" + mbcolorTextBox.Text + "','" + mbcolorcodeTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Masterbatch Already Exists.')", true);
                }
            }
            
            Application["masterbatchId"] = null;
            Application["editFlag"] = null;
            con.Close();
            if (Application["Duplicate"] is false)
            {
                Application["Duplicate"] = null;
                Response.Redirect("~/displayMasterBatch.aspx");
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