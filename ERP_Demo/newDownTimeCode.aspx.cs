using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newDownTimeCode : System.Web.UI.Page
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
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                con.Open();
                String sqlquery = "SELECT * FROM down_time_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["downTimeCodeId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        downTimeCodeTextBox.Text = reader["down_time_code"].ToString();
                        downTimeTypeTextBox.Text = reader["down_time_type"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();

                if (Application["editFlag"] is true)
                {
                    string sqlqueryE = "SELECT down_time_code FROM down_time_master EXCEPT SELECT down_time_code FROM down_time_master where id = '" + Application["downTimeCodeId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT down_time_code FROM down_time_master";
                    Application["queryD"] = sqlqueryN;

                }

                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (downTimeCodeTextBox.Text.ToLower().Trim() == reader["down_time_code"].ToString().ToLower().Trim())
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
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                    string query = "UPDATE down_time_master SET down_time_code='" + downTimeCodeTextBox.Text.ToString() + "',down_time_type = '" + downTimeTypeTextBox.Text + "' WHERE Id='" + Application["downTimeCodeId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO down_time_master(down_time_code,down_time_type)VALUES('" + downTimeCodeTextBox.Text + "','" + downTimeTypeTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Down time code already exists.')", true);
                }
                //System.Threading.Thread.Sleep(1500);
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["downTimeCodeId"] = null;
                    Application["queryD"] = null;
                    Application["editFlag"] = null;
                    Application["Duplicate"] = null;
                    Response.Redirect("~/displayDownTimeCode.aspx");
                }
            }
            catch(Exception ex)
            {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert("+ex.Message+")", true);
                
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayDownTimeCode.aspx");
        }
    }
}