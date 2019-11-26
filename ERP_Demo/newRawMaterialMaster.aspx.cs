using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ERP_Demo
{
    public partial class newRawMaterialMaster : System.Web.UI.Page
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
                String sqlquery = "SELECT * FROM raw_material_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["rawMaterialId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rmName.Text = reader["material_name"].ToString();
                        rmGrade.Text = reader["material_grade"].ToString();
                        rmColor.Text = reader["material_color"].ToString();
                        rmMake.Text = reader["material_make"].ToString();
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
                string query = "UPDATE raw_material_master SET material_name='" + rmName.Text.ToString() + "',material_grade='" + rmGrade.Text.ToString() + "',material_color='" + rmColor.Text.ToString() + "',material_make='" + rmMake.Text.ToString() + "' WHERE Id='" + Application["rawMaterialId"] + "'";
                Application["query"] = query;
            }
            else
            {
                string query = "INSERT INTO raw_material_master(material_name,material_grade,material_color,material_make)VALUES('" + rmName.Text + "','" + rmGrade.Text + "','" + rmColor.Text + "','" + rmMake.Text + "')";
                Application["query"] = query;
            }
            SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
            cmd.ExecuteNonQuery();
            Application["editFlag"] = null;
            Application["query"] = null;
            Application["rawMaterialId"] = null;
            con.Close();
            Response.Redirect("~/displayRawMaterial.aspx");
                    
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayRawMaterial.aspx");
        }

    }
}