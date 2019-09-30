using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newMachineMaster : System.Web.UI.Page
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
                String sqlquery = "SELECT * FROM machine_master where id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["machineId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        machineNoTextBox.Text = reader["machine_no"].ToString();
                        machineNameTextBox.Text = reader["machine_name"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (machineNoTextBox.Text == "" || machineNameTextBox.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
            }
            else
            {
                if (machineFileUpload.HasFile)
                {
                    machineFileUpload.SaveAs(Server.MapPath("~/UploadedFiles/Machine/") + machineFileUpload.FileName);
                    lblAfterProcess.Text = "FileUploaded";
                    lblAfterProcess.ForeColor = System.Drawing.Color.Green;
                    //Application["FileUploaded"] = true;
                }
                else
                {
                    lblAfterProcess.Text = "Please select a file to upload";
                    lblAfterProcess.ForeColor = System.Drawing.Color.Red;
                   //Application["FileUploaded"] = false;
                }
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                if (Application["editFlag"] is true)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["downTimeCodeId"].ToString() + "')", true);
                    string query = "UPDATE machine_master SET machine_no='" + machineNoTextBox.Text.ToString() + "',machine_name='" + machineNameTextBox.Text.ToString() + "',machine_file_upload='" + Server.MapPath("~/UploadedFiles/Machine/") + machineFileUpload.FileName + "' WHERE Id='" + Application["machineId"] + "'";
                    Application["query"] = query;
                }
                else
                {
                    string query = "INSERT INTO machine_master(machine_no,machine_name,machine_file_upload)VALUES('" + machineNoTextBox.Text + "','" + machineNameTextBox.Text + "','" + Server.MapPath("~/UploadedFiles/Machine/") + machineFileUpload.FileName + "')";
                    Application["query"] = query;
                }
                SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                cmd.ExecuteNonQuery();
                Application["machineId"] = null;
                Application["query"] = null;
                Application["editFlag"] = null;
                //Application["FileUploaded"] = null;
                con.Close();
                Response.Redirect("~/displayMachine.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayWorker.aspx");
        }
    }
}