using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ERP_Demo
{
    public partial class newRawMaterialMaster : System.Web.UI.Page
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
                    string sqlqueryE = "SELECT material_grade FROM raw_material_master EXCEPT SELECT material_grade FROM raw_material_master where id = '" + Application["rawMaterialId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT material_grade FROM raw_material_master";
                    Application["queryD"] = sqlqueryN;
                }

                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (rmGrade.Text.ToLower().Trim() == reader["material_grade"].ToString().ToLower().Trim())
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
                    if (Application["Duplicate"] is false && Application["editFlag"] is true)
                    {
                        string query = "UPDATE raw_material_master SET material_name='" + rmName.Text.ToString() + "',material_grade='" + rmGrade.Text.ToString() + "',material_color='" + rmColor.Text.ToString() + "',material_make='" + rmMake.Text.ToString() + "' WHERE Id='" + Application["rawMaterialId"] + "'";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }
                    else if (Application["Duplicate"] is false)
                    {
                        string query = "INSERT INTO raw_material_master(material_name,material_grade,material_color,material_make)VALUES('" + rmName.Text + "','" + rmGrade.Text + "','" + rmColor.Text + "','" + rmMake.Text + "')";
                        SqlCommand cmd = new SqlCommand(query.ToString(), con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Raw material grade already exists.')", true);
                    }
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["editFlag"] = null;
                    Application["queryD"] = null;
                    Application["rawMaterialId"] = null;
                    Response.Redirect("~/displayRawMaterial.aspx");
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
            Response.Redirect("~/displayRawMaterial.aspx");
        }

    }
}