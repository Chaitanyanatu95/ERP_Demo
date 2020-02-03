using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;

namespace ERP_Demo
{
    public partial class fpaReports : Page
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

                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM post_operation_master EXCEPT SELECT * FROM post_operation_master WHERE type = 'N/A' order by id OFFSET 1 ROWS ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            operationTypeDropDownList.DataSource = reader;
                            operationTypeDropDownList.DataBind();
                            reader.Close();
                            operationTypeDropDownList.Items.Insert(0, new ListItem("Select Type", ""));
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void generateReportsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select atleast one option to generate report!')", true);
                }
                else
                {
                    Application["query"] = string.Empty;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = settings.ToString();

                    /*SINGLE SELECTION*/

                    if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE part_name ='" + partNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE operation_type ='" + operationTypeDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }

                    /*DOUBLE SELECTION*/
                    /*1st Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE operation_type='" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    /*2nd Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    /*3rd Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }

                    /*4th Combination*/
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE part_name ='" + partNameDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE operation_type ='" + operationTypeDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }

                    /*TRIPLE SELECTION*/

                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details = '" + shiftDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE operator_name ='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND operation_type = '" + operationTypeDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND operation_type = '" + operationTypeDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND operation_type = '" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name = '" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE part_name ='" + partNameDropDownList.SelectedItem.Text + "' AND operation_type = '" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name = '" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "' AND operator_name = '" + workerNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa WHERE shift_details ='" + shiftDropDownList.SelectedItem.Text + "' AND operation_type = '" + operationTypeDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }

                    /*FOUR SELECTION*/

                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text == "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa where part_name='" + partNameDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text == "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa where operation_type='" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text == "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa where operation_type='" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text == "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa where operation_type='" + operationTypeDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value == "" && dateSelection2.Value == "")
                    {
                        string query = "SELECT * FROM fpa where operation_type='" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details = '" + shiftDropDownList.SelectedItem.Text + "'";
                        Application["query"] = query;
                    }

                    /*ALL SELECTED*/

                    else if (workerNameDropDownList.SelectedItem.Text != "Select Worker Name" && shiftDropDownList.SelectedItem.Text != "Select Shift Time" && partNameDropDownList.SelectedItem.Text != "Select Part Name" && operationTypeDropDownList.SelectedItem.Text != "Select Type" && dateSelection.Value != "" && dateSelection2.Value != "")
                    {
                        string query = "SELECT * FROM fpa where operation_type='" + operationTypeDropDownList.SelectedItem.Text + "' AND operator_name='" + workerNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details = '" + shiftDropDownList.SelectedItem.Text + "' AND date BETWEEN '" + dateSelection.Value + "' AND '" + dateSelection2.Value + "'";
                        Application["query"] = query;
                    }
                    else
                    {
                        lblErrorMessage.Text = "Please select valid entries.";
                    }

                    /* PROCESS */
                    SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                    da = new SqlDataAdapter(cmd);
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "fpa");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=fpa_report.xlsx");
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