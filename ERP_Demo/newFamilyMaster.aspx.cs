using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["familyId"].ToString() + "')", true);
                con.Open();
                string sqlquery = "SELECT * FROM family_master where id = @id";
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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            
            if (Application["editFlag"] is true)
            {
                string sqlqueryE = "SELECT Family FROM family_master EXCEPT SELECT Family FROM family_master where id = '" + Application["familyId"].ToString() + "'";
                Application["queryD"] = sqlqueryE;
            }
            else
            {
                string sqlqueryN = "SELECT Family FROM family_master";
                Application["queryD"] = sqlqueryN;

            }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);

            using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
            {
                SqlDataReader reader = cmmd.ExecuteReader();
                while (reader.Read())
                {
                    if(familyTextBox.Text.ToLower().Trim() == reader["Family"].ToString().ToLower().Trim())
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
                string query = "UPDATE family_master SET Family='" + familyTextBox.Text.ToString() + "' WHERE id='" + Application["familyId"] + "'";
                SqlCommand cmd = new SqlCommand(query.ToString(), con);
                cmd.ExecuteNonQuery();
                
            }
            else if(Application["Duplicate"] is false)
            {
                string query = "INSERT INTO family_master(Family)VALUES('" + familyTextBox.Text.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query.ToString(), con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product category already exists.')", true);

            }
            con.Close();
            if (Application["Duplicate"] is false)
            {
                Application["Duplicate"] = null;
                Application["familyId"] = null;
                Application["queryD"] = null;
                Application["editFlag"] = null;
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