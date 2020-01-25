using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ERP_Demo
{
    public partial class DPR : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadValuesInControlls();
                Application["editFlag"] = false;
            }
        }

        protected void LoadValuesInControlls()
        {
            try
            {
                if (Session["username"] is null)
                {
                    Response.Redirect("~/Login.aspx/");
                }
                else
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT worker_name FROM worker_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        operatorNameDropDownList.DataSource = reader;
                        operatorNameDropDownList.DataBind();
                        reader.Close();
                        operatorNameDropDownList.Items.Insert(0, new ListItem("Select Worker Name", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM parts_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        partNameDropDownList.DataSource = reader;
                        partNameDropDownList.DataBind();
                        reader.Close();
                        partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
                    }
                    noShotsStartTextBox.ReadOnly = true;
                    noShotsEndTextBox.ReadOnly = true;
                    rejectionPCSTextBox.Text = null;
                    expQuantityTextBox.Text = null;
                    actQuantityTextBox.Text = null;
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void machineUsedChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT cycle_time FROM parts_master WHERE part_name= '" + partNameDropDownList.SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["cycleTime"] = reader["cycle_time"].ToString();
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT shift_time FROM shift_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        shiftDetailsDropDownList.DataSource = reader;
                        shiftDetailsDropDownList.DataBind();
                        reader.Close();
                        shiftDetailsDropDownList.Items.Insert(0, new ListItem("Select Shift Details", ""));
                    }
                    con.Close();
                    noShotsStartTextBox.ReadOnly = true;
                    noShotsEndTextBox.ReadOnly = true;
                    expQuantityTextBox.Text = null;
                }
            }
            catch(Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        protected void partNameChanged(object sender, EventArgs e)
        {
            try
            {
                if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    using (con)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT no_of_cavities FROM parts_master WHERE part_name= '" + partNameDropDownList.SelectedItem.Value + "' ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Application["noOfCavities"] = reader["no_of_cavities"].ToString();
                            }
                            reader.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT post_operation_required FROM parts_master where part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Application["tempPostReq"] = reader["post_operation_required"];
                            }
                            reader.Close();
                            if (Application["tempPostReq"].ToString() == "YES")
                                Application["postOprReq"] = "OPEN";
                            else
                                Application["postOprReq"] = "CLOSED";
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT machine_no FROM machine_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            machineUsedDropDownList.DataSource = reader;
                            machineUsedDropDownList.DataBind();
                            reader.Close();
                            machineUsedDropDownList.Items.Insert(0, new ListItem("Select Machine Used", ""));
                        }

                        using (SqlCommand cmd2 = new SqlCommand("SELECT DISTINCT rm_grade,alt_rm_grade FROM parts_master where part_name = '" + partNameDropDownList.SelectedItem.Value + "'", con))
                        {
                            DataTable dt = new DataTable();
                            SqlDataAdapter ad = new SqlDataAdapter(cmd2);
                            ad.Fill(dt);

                            materialGradeDropDownList.Items.Clear();
                            if (dt.Rows.Count > 0)
                            {

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    string item1 = dt.Rows[i]["rm_grade"].ToString();
                                    string item2 = dt.Rows[i]["alt_rm_grade"].ToString();

                                    materialGradeDropDownList.Items.Insert(0, new ListItem("Select Material Grade", ""));
                                    materialGradeDropDownList.Items.Add(new ListItem(item1, dt.Rows[i]["rm_grade"].ToString()));
                                    if (item2.ToString() != "")
                                        materialGradeDropDownList.Items.Add(new ListItem(item2, dt.Rows[i]["alt_rm_grade"].ToString()));
                                }
                            }
                            con.Close();
                            shiftDetailsDropDownList.Items.Clear();
                            downTimeCodeDropDownList.Items.Clear();
                            noShotsStartTextBox.Text = "";
                            noShotsEndTextBox.Text = "";
                            noShotsTextBox.Text = "";
                            expQuantityTextBox.Text = "";
                            actQuantityTextBox.Text = "";
                            rejectionPCSTextBox.Text = "";
                            downTimeTextBox.Text = "";
                            efficiencyTextBox.Text = "";
                            noShotsStartTextBox.ReadOnly = true;
                            noShotsEndTextBox.ReadOnly = true;
                            rejectionPCSTextBox.ReadOnly = true;
                            downTimeTextBox.ReadOnly = true;
                        }
                    }
                }
                else
                {
                    machineUsedDropDownList.Items.Clear();
                    materialGradeDropDownList.Items.Clear();
                    shiftDetailsDropDownList.Items.Clear();
                    downTimeCodeDropDownList.Items.Clear();
                    actQuantityTextBox.Text = "";
                    rejectionPCSTextBox.Text = "";
                    expQuantityTextBox.Text = "";
                    noShotsStartTextBox.Text = "";
                    noShotsEndTextBox.Text = "";
                    noShotsTextBox.Text = "";
                    downTimeTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    Application["postOprReq"] = "";
                    noShotsStartTextBox.ReadOnly = true;
                    noShotsEndTextBox.ReadOnly = true;
                    rejectionPCSTextBox.ReadOnly = true;
                    downTimeTextBox.ReadOnly = true;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void shiftDetailsChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT hours FROM shift_master WHERE shift_time= '" + shiftDetailsDropDownList.SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["shiftTime"] = reader["hours"].ToString();
                        }
                        calculateExpectedQuantity();
                        reader.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void calculateExpectedQuantity()
        {
            try
            {
                if (machineUsedDropDownList.SelectedItem.Text.ToString() != "Select Machine Used" || partNameDropDownList.SelectedItem.Text.ToString() != "Select Part Name" || shiftDetailsDropDownList.SelectedItem.Text.ToString() != "Select Shift Details")
                {
                    string cycleTime = Application["cycleTime"].ToString();
                    string noOfCavities = Application["noOfCavities"].ToString();
                    string shiftTime = Application["shiftTime"].ToString();
                    expQuantityTextBox.Text = Math.Ceiling((3600 / double.Parse(cycleTime)) * (double.Parse(noOfCavities) * double.Parse(shiftTime)) * 0.9).ToString();
                    noShotsStartTextBox.ReadOnly = false;
                    noShotsEndTextBox.ReadOnly = false;
                }
                else
                {
                    noShotsStartTextBox.ReadOnly = true;
                    noShotsEndTextBox.ReadOnly = true;
                    expQuantityTextBox.Text = null;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void calculateActualQuantity()
        {
            try
            {
                if (actQuantityTextBox.Text.ToString() != null)
                {
                    actQuantityTextBox.Text = Convert.ToString(((int.Parse(noShotsTextBox.Text) * Convert.ToInt32(Application["noOfCavities"])) - int.Parse(rejectionPCSTextBox.Text)));

                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT down_time_type FROM down_time_master", con))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        downTimeCodeDropDownList.DataSource = reader;
                        downTimeCodeDropDownList.DataBind();
                        reader.Close();
                        downTimeCodeDropDownList.Items.Insert(0, new ListItem("Select Down Time Type", ""));
                    }
                    con.Close();
                }
                else
                {
                    downTimeCodeDropDownList.Items.Clear();
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void calculateEfficiency()
        {
            efficiencyTextBox.Text = Convert.ToString(Math.Round(((float.Parse(actQuantityTextBox.Text) + ((float.Parse(downTimeTextBox.Text) * (3600 / Convert.ToDouble(Application["cycleTime"])))*0.9) * Convert.ToDouble(Application["noOfCavities"])) / float.Parse(expQuantityTextBox.Text)) * 100, 2));
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application["noOfCavities"].ToString() == "" || Application["cycleTime"].ToString() == "" || noShotsTextBox.Text == "" || actQuantityTextBox.Text == "" || machineUsedDropDownList.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
                }
                else
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO production(operator_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots,rejection_pcs,rejection_kgs,act_qty,downtime_hrs,down_time_code,efficiency,date_dpr,post_opr_req,fpa_status)VALUES('" + operatorNameDropDownList.SelectedItem.Text + "','" + partNameDropDownList.SelectedItem.Text + "','" + materialGradeDropDownList.SelectedItem.Text + "','" + machineUsedDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "','" + expQuantityTextBox.Text + "','" + noShotsTextBox.Text + "','" + rejectionPCSTextBox.Text + "','" + rejectionKGSTextBox.Text + "','" + actQuantityTextBox.Text + "','" + downTimeTextBox.Text + "','" + downTimeCodeDropDownList.SelectedItem.Text + "','" + efficiencyTextBox.Text + "',@DateDpr,'" + Application["postOprReq"].ToString() +"','')", con);
                    cmd.Parameters.AddWithValue("@DateDpr", dateSelection.Value);
                    cmd.ExecuteNonQuery();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void noShotsEndTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (noShotsStartTextBox.Text != "" && noShotsEndTextBox.Text != "")
                {
                    if (int.Parse(noShotsStartTextBox.Text.ToString().Trim()) > int.Parse(noShotsEndTextBox.Text.ToString().Trim()) || int.Parse(noShotsEndTextBox.Text.ToString().Trim()) == '0' || int.Parse(noShotsStartTextBox.Text.ToString().Trim()) == '0')
                    {
                        noShotsEndTextBox.Text = string.Empty;
                        noShotsTextBox.Text = string.Empty;
                        validationShots.Text = "Value cannot be 0 or less than starting value of shots";
                    }
                    else
                    {
                        noShotsTextBox.Text = (int.Parse(noShotsEndTextBox.Text.ToString().Trim()) - int.Parse(noShotsStartTextBox.Text.ToString().Trim())).ToString();
                        validationShots.Text = string.Empty;
                        rejectionPCSTextBox.ReadOnly = false;
                        rejectionPCSTextBox.Text = "";
                        actQuantityTextBox.Text = "";
                    }
                }
                else
                {
                    noShotsTextBox.Text = "";
                    rejectionPCSTextBox.Text = "";
                    actQuantityTextBox.Text = "";
                    rejectionPCSTextBox.ReadOnly = true;
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
            Response.Redirect("~/Default.aspx");
        }

        protected void downTimeCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (downTimeCodeDropDownList.SelectedItem.Text == "N/A" && actQuantityTextBox.Text.ToString() != "")
                {
                    downTimeTextBox.ReadOnly = true;
                    downTimeTextBox.Text = "0";
                    if (noShotsEndTextBox.Text.ToString() != "" && expQuantityTextBox.Text.ToString() != "")
                        calculateEfficiency();
                }
                else
                {
                    downTimeTextBox.ReadOnly = false;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void downTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (actQuantityTextBox.Text.ToString() != "" && noShotsEndTextBox.Text.ToString() != "" && expQuantityTextBox.Text.ToString() != "")
                calculateEfficiency();
            else
                downTimeTextBox.Text = "";
        }

        protected void rejectionPCSTextBox_TextChanged(object sender, EventArgs e)
        {
            if(noShotsTextBox.Text != "" && rejectionPCSTextBox.Text != "")
            {
                calculateActualQuantity();
            }
            else
            {
                actQuantityTextBox.Text = "";
                downTimeCodeDropDownList.Items.Clear();
            }
        }

        protected void noShotsStartTextBox_TextChanged(object sender, EventArgs e)
        {
            noShotsEndTextBox.Text = "";
            noShotsTextBox.Text = "";
            rejectionPCSTextBox.Text = "";
            actQuantityTextBox.Text = "";
            rejectionPCSTextBox.ReadOnly = true;
        }
    }
}