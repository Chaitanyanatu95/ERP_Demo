using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class reports : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        SqlDataAdapter da;
        DataSet ds = new DataSet();
        StringBuilder htmlTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindControlsOnPageLoad();
            }
        }
        protected void BindControlsOnPageLoad()
        {
            try
            {
                if (Session["username"] is null)
                {
                    Response.Redirect("~/Login.aspx/");
                }
                else
                {
                    //operatorNameTextBox.Text = Session["username"].ToString();
                    SqlConnection con = new SqlConnection(settings.ToString());
                    using (con)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT worker_name FROM worker_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            workerNameDropDownList.DataSource = reader;
                            workerNameDropDownList.DataBind();
                            reader.Close();
                            workerNameDropDownList.Items.Insert(0, new ListItem("Select Worker Name", ""));
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT shift_time FROM shift_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            shiftDropDownList.DataSource = reader;
                            shiftDropDownList.DataBind();
                            reader.Close();
                            shiftDropDownList.Items.Insert(0, new ListItem("Select Shift Time", ""));
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM parts_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            partNameDropDownList.DataSource = reader;
                            partNameDropDownList.DataBind();
                            reader.Close();
                            partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
                        }
                        con.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        private void BindData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = settings.ToString();
                SqlCommand cmd = new SqlCommand("SELECT material_grade,machine_no FROM production WHERE date_dpr BETWEEN '" + dateSelectionTextBox.Text + "' AND '" + dateSelectionTextBox2.Text + "'" +
                    "AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "'", con);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                htmlTable.Append("<table class='tableClass'>");
                htmlTable.Append("<tr style='background-color:skyblue; color: Black;'><th colspan='2'>Production Worker Report</th></tr><tr><th>Material Grade</th><th>Machine No</th></tr>");

                if (!object.Equals(ds.Tables[0], null))
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            htmlTable.Append("<tr>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["material_grade"] + "</td>");
                            htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["machine_no"] + "</td>");
                            htmlTable.Append("</tr>");
                        }
                        htmlTable.Append("</table>");
                        DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                    else
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td align='center' colspan='4'>There is no Record.</td>");
                        htmlTable.Append("</tr>");
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void generateReportsBtn_Click(object sender, EventArgs e)
        {
            if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" || shiftDropDownList.SelectedItem.Text != "Select Shift Time" || partNameDropDownList.SelectedItem.Text != "Select Part Name")
            {
                BindData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select all to generate report!')", true);
            }
        }
    }
}