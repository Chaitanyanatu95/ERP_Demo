using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newPostOperationMaster : System.Web.UI.Page
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
                String sqlquery = "SELECT * FROM post_operation_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["postOperationId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        postOperationTextBox.Text = reader["type"].ToString();
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
                string query = "UPDATE post_operation_master SET type='" + postOperationTextBox.Text.ToString() + "' WHERE id='" + Application["postOperationId"] + "'";
                SqlCommand cmd = new SqlCommand(query.ToString(), con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string sqlquery = "SELECT type FROM post_operation_master";
                //ArrayList al = new ArrayList();
                using (SqlCommand cmmd = new SqlCommand(sqlquery, con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (postOperationTextBox.Text.ToLower() == reader["type"].ToString().ToLower())
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
                    string query = "INSERT INTO post_operation_master(type)VALUES('" + postOperationTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Post Operation Already Exists.')", true);
                }
            }
            Application["postOperationId"] = null;
            Application["query"] = null;
            Application["editFlag"] = null;
            con.Close();
            if (Application["Duplicate"] is false)
            {
                Application["Duplicate"] = null;
                Response.Redirect("~/displayPostOperation.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayPostOperation.aspx");
        }

    }
}