using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newUnitMeasurementMaster : System.Web.UI.Page
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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (unitofmeasurementTextBox.Text == "" || abbreviationTextBox.Text == "" )
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
                    string query = "UPDATE unit_of_measurement_master SET unit_of_measurement='" + unitofmeasurementTextBox.Text.ToString() + "',abbreviation='" + abbreviationTextBox.Text.ToString() + "' WHERE Id='" + Application["unitOfMeasurementId"] + "'";
                    Application["query"] = query;
                }
                else
                {
                    string query = "INSERT INTO unit_of_measurement_master(unit_of_measurement,abbreviation)VALUES('" + unitofmeasurementTextBox.Text + "','" + abbreviationTextBox.Text + "')";
                    Application["query"] = query;
                }
                SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                cmd.ExecuteNonQuery();
                Application["query"] = null;
                Application["unitOfMeasurementId"] = null;
                Application["editFlag"] = null;
                con.Close();
                Response.Redirect("~/displayUnitOfMeasurement.aspx");
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