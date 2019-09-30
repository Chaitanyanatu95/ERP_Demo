using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newRejection : System.Web.UI.Page
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
                String sqlquery = "SELECT * FROM rejection_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["rejectionId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rejectionTypeTextBox.Text = reader["rejection_type"].ToString();
                        codeTextBox.Text = reader["code"].ToString();
                        descTextBox.Text = reader["description"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (rejectionTypeTextBox.Text == "" || codeTextBox.Text == "" || descTextBox.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                if (Application["editFlag"] is true)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                    string query = "UPDATE rejection_master SET rejection_type='" + rejectionTypeTextBox.Text.ToString() + "',code='"+ codeTextBox.Text.ToString()+ "',description='" + descTextBox.Text.ToString() + "' WHERE Id='" + Application["rejectionId"] + "'";
                    Application["query"] = query;
                }
                else
                {
                    string query = "INSERT INTO rejection_master(rejection_type,code,description)VALUES('" + rejectionTypeTextBox.Text + "','"+ codeTextBox.Text + "','" + descTextBox.Text + "')";
                    Application["query"] = query;
                }
                SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                cmd.ExecuteNonQuery();
                //System.Threading.Thread.Sleep(1500);
                Application["rejectionId"] = null;
                Application["query"] = null;
                Application["editFlag"] = null;
                con.Close();
                Response.Redirect("~/displayRejection.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayRejection.aspx");
        }

    }
}