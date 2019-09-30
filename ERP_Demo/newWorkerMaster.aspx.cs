using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newWorkerMaster : System.Web.UI.Page
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
                String sqlquery = "SELECT * FROM worker_master WHERE worker_master.id = @id";
                using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                {
                    cmd.Parameters.AddWithValue("@id", (Application["workerId"]).ToString().Trim());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        empNameTextBox.Text = reader["worker_name"].ToString();
                        empIdTextBox.Text = reader["worker_id"].ToString();
                        userIdTextBox.Text = reader["user_id"].ToString();
                        userPasswordTextBox.Text = reader["user_password"].ToString();
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (empNameTextBox.Text == "" || empIdTextBox.Text == "" || userIdTextBox.Text == "" || userPasswordTextBox.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();

                if (Application["editFlag"] is true)
                {
                    string updateData = "UPDATE worker_master SET Admin='NO',Editor='NO',Worker='NO',Extra='NO' WHERE id='" + Application["workerId"] + "'";
                    SqlCommand comdUpdate = new SqlCommand(updateData, con);
                    comdUpdate.ExecuteNonQuery();

                    foreach (ListItem li in rightsToBeAllocatedCheckBoxList.Items)
                    {
                        if (li.Selected)
                        {
                            if (li.Value.ToString() == "Admin")
                            {
                                string query1 = "UPDATE worker_master SET Admin = 'YES' WHERE id = '" + Application["workerId"] + "'";
                                SqlCommand comd1 = new SqlCommand(query1.ToString(), con);
                                comd1.ExecuteNonQuery();
                            }

                            if (li.Text.Trim() == "Editor")
                            {
                                string query2 = "UPDATE worker_master SET Editor = 'YES' WHERE id = '" + Application["workerId"] + "'";
                                SqlCommand comd2 = new SqlCommand(query2.ToString(), con);
                                comd2.ExecuteNonQuery();
                            }

                            if (li.Text.Trim() == "Worker")
                            {
                                string query3 = "UPDATE worker_master SET Worker = 'YES' WHERE id = '" + Application["workerId"] + "'";
                                SqlCommand comd3 = new SqlCommand(query3.ToString(), con);
                                comd3.ExecuteNonQuery();
                            }

                            if (li.Text.Trim() == "Extra")
                            {
                                string query4 = "UPDATE worker_master SET Extra = 'YES' WHERE id = '" + Application["workerId"] + "'";
                                SqlCommand comd4 = new SqlCommand(query4.ToString(), con);
                                comd4.ExecuteNonQuery();
                            }
                        }
                    }

                    //UPDATE WORKER MASTER 
                    string query = "UPDATE worker_master SET worker_name='" + empNameTextBox.Text.ToString() + "',worker_id ='" + empIdTextBox.Text.ToString() + "',user_id = '" + userIdTextBox.Text.ToString() + "',user_password = '" + userPasswordTextBox.Text.ToString() + "' WHERE id='" + Application["workerId"] + "'";
                    SqlCommand comd = new SqlCommand(query.ToString(), con);
                    comd.ExecuteNonQuery();
                }
                else
                {
                    //INSERT INTO WORKER MASTER
                    string Admin = string.Empty;
                    string Editor = string.Empty;
                    string Worker = string.Empty;
                    string Extra = string.Empty;
                    foreach (ListItem li in rightsToBeAllocatedCheckBoxList.Items)
                    {
                        if (li.Selected)
                        {
                            if (li.Text.Trim() == "Admin")
                            {
                                Admin = "YES";
                            }

                            if (li.Text.Trim() == "Editor")
                            {
                                Editor = "YES";
                            }

                            if (li.Text.Trim() == "Worker")
                            {
                                Worker = "YES";
                            }

                            if (li.Text.Trim() == "Extra")
                            {
                                Extra = "YES";
                            }
                        }
                    }

                    if (Admin.ToString() != "YES") { Admin = "NO"; }
                    if (Editor.ToString() != "YES") { Editor = "NO"; }
                    if (Worker.ToString() != "YES") { Worker = "NO"; }
                    if (Extra.ToString() != "YES") { Extra = "NO"; }

                    string query = "INSERT INTO worker_master(worker_name,worker_id,user_id,user_password,Admin,Editor,Worker,Extra)VALUES('" + empNameTextBox.Text + "','" + empIdTextBox.Text + "','" + userIdTextBox.Text + "','" + userPasswordTextBox.Text + "','" + Admin.ToString() + "','" + Editor.ToString() + "','" + Worker.ToString() + "','" + Extra.ToString() + "')";
                    Application["query"] = query;
                    SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                    cmd.ExecuteNonQuery();
                }
                Application["editFlag"] = null;
                Application["query"] = null;
                Application["workerId"] = null;
                con.Close();
                Response.Redirect("~/displayWorker.aspx");
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