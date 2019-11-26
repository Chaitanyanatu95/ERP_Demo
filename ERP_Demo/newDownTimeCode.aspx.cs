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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            if (Application["editFlag"] is true)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                string query = "UPDATE down_time_master SET down_time_code='" + downTimeCodeTextBox.Text.ToString() + "',down_time_type = '" + downTimeTypeTextBox.Text + "' WHERE Id='" + Application["downTimeCodeId"]+"'";
                Application["query"] = query;
            }
            else
            {
                string query = "INSERT INTO down_time_master(down_time_code,down_time_type)VALUES('" + downTimeCodeTextBox.Text + "','" + downTimeTypeTextBox.Text + "')";
                Application["query"] = query;
            }
            SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
            cmd.ExecuteNonQuery();
            //System.Threading.Thread.Sleep(1500);
            Application["downTimeCodeId"] = null;
            Application["query"] = null;
            Application["editFlag"] = null;
            con.Close();
            Response.Redirect("~/displayDownTimeCode.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayDownTimeCode.aspx");
        }
    }
}