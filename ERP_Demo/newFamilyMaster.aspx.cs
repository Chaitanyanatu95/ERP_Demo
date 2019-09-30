using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newFamilyMaster : System.Web.UI.Page
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["familyId"].ToString() + "')", true);
                con.Open();
                String sqlquery = "SELECT * FROM family_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["familyId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        familyTextBox.Text = reader["Family"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (familyTextBox.Text == "")
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
                    string query = "UPDATE family_master SET Family='" + familyTextBox.Text.ToString() + "' WHERE id='" + Application["familyId"] + "'";
                    Application["query"] = query;
                }
                else
                {
                    string query = "INSERT INTO family_master(Family)VALUES('" + familyTextBox.Text + "')";
                    Application["query"] = query;
                }
                SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                cmd.ExecuteNonQuery();
                Application["familyId"] = null;
                Application["query"] = null;
                Application["editFlag"] = null;
                con.Close();
                Response.Redirect("~/displayFamily.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayFamily.aspx");
        }
    }
}