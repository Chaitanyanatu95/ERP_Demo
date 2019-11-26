using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newPackaging : System.Web.UI.Page
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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            if (Application["editFlag"] is true)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                string query = "UPDATE packaging_master SET packaging_type='" + packagingTypeTextBox.Text.ToString() + "',size='"+ sizeTextBox.Text.ToString()+"' WHERE Id='" + Application["packagingId"] + "'";
                Application["query"] = query;
            }
            else
            {
                string query = "INSERT INTO packaging_master(packaging_type,size)VALUES('" + packagingTypeTextBox.Text + "','"+ sizeTextBox.Text + "')";
                Application["query"] = query;
            }
            SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
            cmd.ExecuteNonQuery();
            //System.Threading.Thread.Sleep(1500);
            Application["packagingId"] = null;
            Application["query"] = null;
            Application["editFlag"] = null;
            con.Close();
            Response.Redirect("~/displayPackaging.aspx");
            
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayPackaging.aspx");
        }
    }
}