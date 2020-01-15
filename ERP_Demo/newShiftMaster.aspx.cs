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
    public partial class newShiftMaster : System.Web.UI.Page
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
                    String sqlquery = "SELECT * FROM shift_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["shiftId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            shiftNameTextBox.Text = reader["shift_time"].ToString();
                            workingHoursTextBox.Text = reader["hours"].ToString();
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
                    string sqlqueryE = "SELECT shift_time FROM shift_master EXCEPT SELECT shift_time FROM shift_master where id = '" + Application["shiftId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT shift_time FROM shift_master";
                    Application["queryD"] = sqlqueryN;
                }
                //ArrayList al = new ArrayList();
                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (shiftNameTextBox.Text.ToLower().Trim() == reader["shift_time"].ToString().ToLower().Trim())
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
                    string query = "UPDATE shift_master SET shift_time='" + shiftNameTextBox.Text.ToString() + "',hours='" + workingHoursTextBox.Text.ToString() + "' WHERE Id='" + Application["shiftId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO shift_master(shift_time, hours)VALUES('" + shiftNameTextBox.Text + "', '" + workingHoursTextBox.Text + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Shift already exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["editFlag"] = null;
                    Application["queryD"] = null;
                    Application["shiftId"] = null;
                    Response.Redirect("~/displayShift.aspx");
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
            Response.Redirect("~/displayShift.aspx");
        }
    }
}