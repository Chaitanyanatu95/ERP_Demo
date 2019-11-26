using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newShiftMaster : System.Web.UI.Page
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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            if (Application["editFlag"] is true)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                string query = "UPDATE shift_master SET shift_time='" + shiftNameTextBox.Text.ToString() + "',hours='" + workingHoursTextBox.Text.ToString() + "' WHERE Id='" + Application["shiftId"] + "'";
                Application["query"] = query;
            }
            else
            {
                string query = "INSERT INTO shift_master(shift_time, hours)VALUES('" + shiftNameTextBox.Text + "', '" + workingHoursTextBox.Text + "')";
                Application["query"] = query;
            }
            SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
            cmd.ExecuteNonQuery();
            Application["editFlag"] = null;
            Application["query"] = null;
            Application["shiftId"] = null;
            con.Close();
            Response.Redirect("~/displayShift.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayShift.aspx");
        }
    }
}