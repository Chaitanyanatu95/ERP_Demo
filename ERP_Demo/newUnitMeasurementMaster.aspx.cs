using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newUnitMeasurementMaster : System.Web.UI.Page
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
                    String sqlquery = "SELECT * FROM unit_of_measurement_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["unitOfMeasurementId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            unitofmeasurementTextBox.Text = reader["unit_of_measurement"].ToString();
                            abbreviationTextBox.Text = reader["abbreviation"].ToString();
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
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();

                if (Application["editFlag"] is true)
                {
                    string sqlqueryE = "SELECT unit_of_measurement FROM unit_of_measurement_master EXCEPT SELECT unit_of_measurement FROM unit_of_measurement_master where id = '" + Application["unitOfMeasurementId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT unit_of_measurement FROM unit_of_measurement_master";
                    Application["queryD"] = sqlqueryN;
                }

                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (unitofmeasurementTextBox.Text.ToLower().Trim() == reader["unit_of_measurement"].ToString().ToLower().Trim())
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
                if(Application["Duplicate"] is false && Application["editFlag"] is true)
                {
                    string query = "UPDATE unit_of_measurement_master SET unit_of_measurement='" + unitofmeasurementTextBox.Text.ToString() + "',abbreviation='" + abbreviationTextBox.Text.ToString() + "' WHERE Id='" + Application["unitOfMeasurementId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO unit_of_measurement_master(unit_of_measurement,abbreviation)VALUES('" + unitofmeasurementTextBox.Text + "','" + abbreviationTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Unit already exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["unitOfMeasurementId"] = null;
                    Application["queryD"] = null;
                    Application["editFlag"] = null;
                    Response.Redirect("~/displayUnitOfMeasurement.aspx");
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
            Response.Redirect("~/displayUnitOfMeasurement.aspx");
        }
    }
}