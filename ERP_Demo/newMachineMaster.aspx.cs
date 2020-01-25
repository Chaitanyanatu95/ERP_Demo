using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ERP_Demo
{
    public partial class newMachineMaster : System.Web.UI.Page
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
                    String sqlquery = "SELECT * FROM machine_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["machineId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            machineNoTextBox.Text = reader["machine_no"].ToString();
                            machineNameTextBox.Text = reader["machine_name"].ToString();
                            lblMachineSpec.Text = reader["machine_file_upload"].ToString();
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
                if (machineFileUpload.HasFile)
                {
                    machineFileUpload.SaveAs(Server.MapPath("~/UploadedFiles/Machine/") + machineFileUpload.FileName);
                }
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();
                if (Application["editFlag"] is true)
                {
                    string sqlqueryE = "SELECT machine_no FROM machine_master EXCEPT SELECT machine_no FROM machine_master where id = '" + Application["machineId"].ToString() + "'";
                    Application["queryD"] = sqlqueryE;
                }
                else
                {
                    string sqlqueryN = "SELECT machine_no FROM machine_master";
                    Application["queryD"] = sqlqueryN;
                }

                using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                {
                    SqlDataReader reader = cmmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (machineNoTextBox.Text.ToLower().Trim() == reader["machine_no"].ToString().ToLower().Trim())
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
                    string query = "UPDATE machine_master SET machine_no='" + machineNoTextBox.Text.ToString() + "',machine_name='" + machineNameTextBox.Text.ToString() + "',machine_file_upload='" + machineFileUpload.FileName + "' WHERE Id='" + Application["machineId"] + "'";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else if (Application["Duplicate"] is false)
                {
                    string query = "INSERT INTO machine_master(machine_no,machine_name,machine_file_upload)VALUES('" + machineNoTextBox.Text + "','" + machineNameTextBox.Text + "','" + machineFileUpload.FileName + "')";
                    SqlCommand cmd = new SqlCommand(query.ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Machine no already exists.')", true);
                }
                con.Close();
                if (Application["Duplicate"] is false)
                {
                    Application["Duplicate"] = null;
                    Application["machineId"] = null;
                    Application["editFlag"] = null;
                    Response.Redirect("~/displayMachine.aspx");
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
            Response.Redirect("~/displayMachine.aspx");
        }

        protected void btnMachineSpecs_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());

                if (lblMachineSpec.Text != null)
                {
                    if (Application["editFlag"] is true)
                    {
                        var path = Server.MapPath(lblMachineSpec.Text);
                        //System.Diagnostics.Debug.WriteLine(path);
                        if (File.Exists(path))
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE machine_master set machine_file_upload = '' where id = '" + Application["machineId"] + "'", con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                File.Delete(Server.MapPath(lblMachineSpec.Text));
                            }
                            lblMachineSpec.Text = "";
                            con.Close();
                        }
                        else
                        {
                            lblMachineSpec.Text = "";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}