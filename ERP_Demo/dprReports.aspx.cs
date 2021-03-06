﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;

namespace ERP_Demo
{
    public partial class dprReports : Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        SqlDataAdapter da;
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

                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT machine_no FROM machine_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            machineNoDropDownList.DataSource = reader;
                            machineNoDropDownList.DataBind();
                            reader.Close();
                            machineNoDropDownList.Items.Insert(0, new ListItem("Select Machine No", ""));
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

        protected void generateReportsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select atleast one option to generate report!')", true);
                }
                else
                {
                    Application["query"] = string.Empty;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = settings.ToString();

                    /*SINGLE SELECTION*/

                    if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE part_name ='" + partNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE machine_no ='" + machineNoDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE date_dpr BETWEEN '" + dateSelection.Value+ "' AND '"+dateSelection2.Value+"'";
                        Application["query"] = query;
                    }

                    /*DOUBLE SELECTION*/
                        /*1st Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE machine_no='" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                        /*2nd Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND machine_no='" + machineNoDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                        /*3rd Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "' AND machine_no='" + machineNoDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }

                        /*4th Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '"+dateSelection.Value+"' AND '"+dateSelection2.Value+"'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '"+dateSelection.Value+"' AND '"+dateSelection2.Value+"'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE part_name ='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '"+dateSelection.Value+"' AND '"+dateSelection2.Value+"'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE machine_no ='" + machineNoDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }

                    /*TRIPLE SELECTION*/

                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details = '"+shiftDropDownList.SelectedItem.Text+"' AND date_dpr BETWEEN '"+dateSelection.Value+"' AND '"+dateSelection2.Value+"'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND machine_no = '"+machineNoDropDownList.SelectedItem.Text+"' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND machine_no = '" + machineNoDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND machine_no = '" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name = '"+workerNameDropDownList.SelectedItem.Text+"'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE part_name ='" + partNameDropDownList.SelectedItem.Text + "' AND machine_no = '" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name = '" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND operator_name = '" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND machine_no = '" + machineNoDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }

                    /*FOUR SELECTION*/

                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text == "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production where part_name='"+partNameDropDownList.SelectedItem.Text+"' AND operator_name='"+workerNameDropDownList.SelectedItem.Text+"' AND shift_details='"+shiftDropDownList.SelectedItem.Text+"' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production where machine_no='" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production where machine_no='" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production where machine_no='" + machineNoDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM production where machine_no='" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details = '"+shiftDropDownList.SelectedItem.Text+"'";
                        Application["query"] = query;
                    }

                    /*ALL SELECTED*/

                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && machineNoDropDownList.SelectedItem.Text != "Select Machine No" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM production where machine_no='" + machineNoDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details = '" + shiftDropDownList.SelectedItem.Text + "' AND date_dpr BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else
                    {
                        lblErrorMessage.Text = "Please select valid entries.";
                    }

                    /* PROCESS */
                    SqlCommand cmd = new SqlCommand(Application["query"].ToString(),con);
                    da = new SqlDataAdapter(cmd);
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "production");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=production_report.xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}