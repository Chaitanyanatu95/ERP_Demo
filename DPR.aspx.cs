using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;

namespace ERP_Demo
{
    public partial class DPR : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(settings.ToString());
            con.Open();

            if (!IsPostBack)
            {
                LoadDownTimeGrid();

                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
                else
                {
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

                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM production ORDER BY ID DESC", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["input"] = reader["dpr_no"];
                        }
                        reader.Close();
                        if (Application["input"] == null)
                            Application["input"] = 0;
                        int input = int.Parse(Regex.Replace(Application["input"].ToString(), "[^0-9]+", string.Empty));
                        dprNo.Text = "DPRTR#" + (input + 1);
                    }
                    noShotsStartTextBox.ReadOnly = true;
                    noShotsEndTextBox.ReadOnly = true;
                    rejectionPCSTextBox.Text = null;
                    expQuantityTextBox.Text = null;
                    actQuantityTextBox.Text = null;
                }

                string postSelectQuery = "SELECT dpr_no FROM production where dpr_no= '" + dprNo.Text.Trim() + "'";
                SqlCommand selCmd = new SqlCommand(postSelectQuery, con);
                Application["downTimeSelectData"] = "";
                SqlDataReader selReader = selCmd.ExecuteReader();
                while (selReader.Read())
                {
                    if (selReader["dpr_no"].ToString() != "")
                    {
                        Application["downTimeSelectData"] = selReader["dpr_no"].ToString();
                    }
                }
                selReader.Close();
                
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["rowCommand"].ToString() + "')", true);

                if (Application["downTimeSelectData"].ToString() == "" && Application["rowCommand"] is false)
                {
                    string postQuery = "DELETE FROM down_time_production_master where dpr_no= '" + dprNo.Text.Trim() + "'";
                    SqlCommand cmmd = new SqlCommand(postQuery, con);
                    cmmd.ExecuteNonQuery();

                    string postQuery2 = "DELETE FROM down_time_production where dpr_no= '" + dprNo.Text.Trim() + "'";
                    SqlCommand cmmd2 = new SqlCommand(postQuery2, con);
                    cmmd2.ExecuteNonQuery();
                    Application["downTimeSelectData"] = null;
                }
            }
            else
            {
                Application["rowCommand"] = false;
            }
            con.Close();
        }

        protected void LoadDownTimeGrid()
        {
            try
            {
                /************** DOWNTIME ****************/
                DataTable dtDownTime = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    SqlDataAdapter sqlDa;
                    if (Application["editFlag"] is true)
                    {
                        sqlDa = new SqlDataAdapter("SELECT * FROM down_time_production WHERE dpr_no = '"+Application["dprNo"].ToString().Trim()+"'", sqlCon);
                    }
                    else
                    {
                        sqlDa = new SqlDataAdapter("SELECT * FROM down_time_production_master WHERE dpr_no = '"+dprNo.Text.ToString()+"'", sqlCon);
                    }
                    sqlDa.Fill(dtDownTime);
                }
                if (dtDownTime.Rows.Count > 0)
                {
                    downTimeGridView.DataSource = dtDownTime;
                    downTimeGridView.DataBind();
                    var downTimeDropDown = (DropDownList)downTimeGridView.FooterRow.FindControl("downTimeDropDownListFooter");
                    var qty = (TextBox)downTimeGridView.FooterRow.FindControl("txtdownTimeFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    downTimeDropDown.Items.Insert(0, new ListItem("Select Code"));
                    downTimeDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)downTimeGridView.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                }
                else
                {
                    dtDownTime.Rows.Add(dtDownTime.NewRow());
                    downTimeGridView.DataSource = dtDownTime;
                    downTimeGridView.DataBind();
                    var downTimeDropDown = (DropDownList)downTimeGridView.FooterRow.FindControl("downTimeDropDownListFooter");
                    var qty = (TextBox)downTimeGridView.FooterRow.FindControl("txtdownTimeFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    downTimeDropDown.Items.Insert(0, new ListItem("Select Code"));
                    downTimeDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)downTimeGridView.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                    downTimeGridView.Rows[0].Cells.Clear();
                    downTimeGridView.Rows[0].Cells.Add(new TableCell());
                    downTimeGridView.Rows[0].Cells[0].ColumnSpan = dtDownTime.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    downTimeGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadEditValuesInController()
        {
            try
            {
                Application["Duplicate"] = false;
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    //part_name = @partName AND date_dpr = @dateDpr AND shift_details = @shiftDetails AND operator_name = @operatorName
                    String sqlquery = "SELECT * FROM production where dpr_no = @dprNo";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        /*DateTime stdate;
                        if (DateTime.TryParse(Application["dateDpr"].ToString(), out stdate))
                        {
                            SqlParameter projectStartingDateParam = new SqlParameter("@dateDpr", SqlDbType.DateTime);
                            projectStartingDateParam.Value = stdate;
                            cmd.Parameters.Add(projectStartingDateParam);
                        }
                        cmd.Parameters.AddWithValue("@partName", (Application["partName"]).ToString().Trim());
                        cmd.Parameters.AddWithValue("@shiftDetails", (Application["shiftDetails"]).ToString().Trim());*/

                        cmd.Parameters.AddWithValue("@dprNo", (Application["dprNo"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dprNo.Text = reader["dpr_no"].ToString();
                            var date = DateTime.Parse(reader["date_dpr"].ToString()).Date;
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+date+"')", true);
                            dateTextBox.Text = date.ToString("yyyy-MM-dd");
                            Application["oprName"] = reader["operator_name"].ToString();
                            Application["materialGrade"] = reader["material_grade"].ToString();
                            Application["machineNo"] = reader["machine_no"].ToString();
                            Application["shiftDetails"] = reader["shift_details"].ToString();
                            expQuantityTextBox.Text = reader["exp_qty"].ToString();
                            noShotsStartTextBox.Text = reader["no_of_shots_start"].ToString();
                            noShotsEndTextBox.Text = reader["no_of_shots_end"].ToString();
                            noShotsTextBox.Text = reader["no_of_shots"].ToString();
                            rejectionPCSTextBox.Text = reader["rejection_pcs"].ToString();
                            rejectionKGSTextBox.Text = reader["rejection_kgs"].ToString();
                            actQuantityTextBox.Text = reader["act_qty"].ToString();
                            efficiencyTextBox.Text = reader["efficiency"].ToString();
                        }
                        reader.Close();
                    }

                    using (SqlCommand cmd2 = new SqlCommand("SELECT worker_name FROM worker_master where worker_name NOT IN('" + Application["oprName"].ToString().Trim() + "')", con))
                    {
                        SqlDataReader reader2 = cmd2.ExecuteReader();
                        operatorNameDropDownList.DataSource = reader2;
                        operatorNameDropDownList.DataBind();
                        reader2.Close();
                        operatorNameDropDownList.Items.Insert(0, new ListItem(Application["oprName"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT part_name FROM parts_master where part_name NOT IN('" + Application["partName"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        partNameDropDownList.DataSource = reader;
                        partNameDropDownList.DataBind();
                        reader.Close();
                        partNameDropDownList.Items.Insert(0, new ListItem(Application["partName"].ToString()));
                    }

                    RequiredFieldValidator2.Enabled = false;

                    using (SqlCommand cmd2 = new SqlCommand("SELECT DISTINCT rm_grade,alt_rm_grade FROM parts_master WHERE part_name = '" + partNameDropDownList.SelectedItem.Value + "'", con))
                    {
                        string item1 = "";
                        string item2 = "";
                        DataTable dt = new DataTable();
                        SqlDataAdapter ad = new SqlDataAdapter(cmd2);
                        ad.Fill(dt);
                        materialGradeDropDownList.Items.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                item1 = dt.Rows[i]["rm_grade"].ToString();
                                item2 = dt.Rows[i]["alt_rm_grade"].ToString();

                                materialGradeDropDownList.Items.Insert(0, new ListItem(Application["materialGrade"].ToString(), ""));
                                if (item1.Trim() != Application["materialGrade"].ToString().Trim())
                                {
                                    materialGradeDropDownList.Items.Add(new ListItem(item1, dt.Rows[i]["rm_grade"].ToString()));
                                }
                                if (item2.ToString() != "")
                                    materialGradeDropDownList.Items.Add(new ListItem(item2, dt.Rows[i]["alt_rm_grade"].ToString()));
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT machine_no FROM machine_master where machine_no NOT IN('" + Application["machineNo"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        machineUsedDropDownList.DataSource = reader;
                        machineUsedDropDownList.DataBind();
                        reader.Close();
                        machineUsedDropDownList.Items.Insert(0, new ListItem(Application["machineNo"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT shift_time FROM shift_master where shift_time NOT IN('" + Application["shiftDetails"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        shiftDetailsDropDownList.DataSource = reader;
                        shiftDetailsDropDownList.DataBind();
                        reader.Close();
                        shiftDetailsDropDownList.Items.Insert(0, new ListItem(Application["shiftDetails"].ToString()));
                    }

                    if (Application["fpaStatus"].ToString() == "OPEN" || Application["fpaStatus"].ToString() == "CLOSED")
                    {
                        operatorNameDropDownList.Enabled = false;
                        operatorNameDropDownList.BackColor = Color.WhiteSmoke;
                        dateTextBox.ReadOnly = true;
                        dateTextBox.BackColor = Color.WhiteSmoke;
                        partNameDropDownList.Enabled = false;
                        partNameDropDownList.BackColor = Color.WhiteSmoke;
                        materialGradeDropDownList.Enabled = false;
                        materialGradeDropDownList.BackColor = Color.WhiteSmoke;
                        machineUsedDropDownList.Enabled = false;
                        machineUsedDropDownList.BackColor = Color.WhiteSmoke;
                        shiftDetailsDropDownList.Enabled = false;
                        shiftDetailsDropDownList.BackColor = Color.WhiteSmoke;
                        noShotsStartTextBox.ReadOnly = false;
                        noShotsEndTextBox.ReadOnly = false;
                    }
                    con.Close();
                    LoadValuesInController();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadValuesInController()
        {
            try
            {
                /************** ASSEMBLY OPERATION ****************/
                DataTable dtTbl = new DataTable();
                dtTbl.Columns.AddRange(new DataColumn[2] { new DataColumn("DOWN TIME CODE"), new DataColumn("DOWN TIME(HRS)") });
                Application["DownTimeOperation"] = dtTbl;
                Application["tempDownTimeNo"] = "";
                BindDownTimeGrid();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    if (Application["editFlag"] is true)
                    {
                        string query = "SELECT dpr_no FROM production where Id = '" + Application["id"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["tempDownTimeNo"] = reader["dpr_no"];
                        }
                        reader.Close();
                        if (Application["tempDownTimeNo"].ToString() != "")
                        {
                            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM down_time_production where dpr_no = '" + Application["dprNo"].ToString() + "'", sqlCon);
                            Application["tempDownTimeNo"] = null;
                            sqlDa.Fill(dtTbl);
                        }
                    }
                    else
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM down_time_production_master where dpr_no = '" + dprNo.Text.ToString() + "'", sqlCon);
                        sqlDa.Fill(dtTbl);
                        lblErrorMessage.Text = "";
                    }
                }
                if (dtTbl.Rows.Count > 0)
                {
                    BindDownTimeGrid();
                    var downTimeDropDown = (DropDownList)downTimeGridView.FooterRow.FindControl("downTimeDropDownListFooter");
                    var qty = (TextBox)downTimeGridView.FooterRow.FindControl("txtdownTimeFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    downTimeDropDown.Items.Insert(0, new ListItem("Select Code"));
                    downTimeDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)downTimeGridView.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                }
                else
                {
                    dtTbl.Rows.Add(dtTbl.NewRow());
                    BindDownTimeGrid();
                    var downTimeDropDown = (DropDownList)downTimeGridView.FooterRow.FindControl("downTimeDropDownListFooter");
                    var qty = (TextBox)downTimeGridView.FooterRow.FindControl("txtdownTimeFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    downTimeDropDown.Items.Insert(0, new ListItem("Select Code"));
                    downTimeDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)downTimeGridView.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                    downTimeGridView.Rows[0].Cells.Clear();
                    downTimeGridView.Rows[0].Cells.Add(new TableCell());
                    downTimeGridView.Rows[0].Cells[0].ColumnSpan = dtTbl.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    downTimeGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void BindDownTimeGrid()
        {
            downTimeGridView.DataSource = (DataTable)Application["DownTimeOperation"];
            downTimeGridView.DataBind();
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
            catch (Exception ex)
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

                        using (SqlCommand cmd2 = new SqlCommand("SELECT DISTINCT rm_grade,alt_rm_grade FROM parts_master WHERE part_name = '" + partNameDropDownList.SelectedItem.Value + "'", con))
                        {
                            string item1 = "";
                            string item2 = "";
                            DataTable dt = new DataTable();
                            SqlDataAdapter ad = new SqlDataAdapter(cmd2);
                            ad.Fill(dt);
                            materialGradeDropDownList.Items.Clear();
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    item1 = dt.Rows[i]["rm_grade"].ToString();
                                    item2 = dt.Rows[i]["alt_rm_grade"].ToString();

                                    materialGradeDropDownList.Items.Insert(0, new ListItem("Select Material Grade", ""));
                                    materialGradeDropDownList.Items.Add(new ListItem(item1, dt.Rows[i]["rm_grade"].ToString()));
                                    if (item2.ToString() != "")
                                        materialGradeDropDownList.Items.Add(new ListItem(item2, dt.Rows[i]["alt_rm_grade"].ToString()));
                                }
                            }
                            con.Close();
                            shiftDetailsDropDownList.Items.Clear();
                            //downTimeCodeDropDownList.Items.Clear();
                            noShotsStartTextBox.Text = "";
                            noShotsEndTextBox.Text = "";
                            noShotsTextBox.Text = "";
                            expQuantityTextBox.Text = "";
                            actQuantityTextBox.Text = "";
                            rejectionPCSTextBox.Text = "";
                            //downTimeTextBox.Text = "";
                            efficiencyTextBox.Text = "";
                            noShotsStartTextBox.ReadOnly = true;
                            noShotsEndTextBox.ReadOnly = true;
                            rejectionPCSTextBox.ReadOnly = true;
                            //downTimeTextBox.ReadOnly = true;
                        }
                    }
                }
                else
                {
                    machineUsedDropDownList.Items.Clear();
                    materialGradeDropDownList.Items.Clear();
                    shiftDetailsDropDownList.Items.Clear();
                    //downTimeCodeDropDownList.Items.Clear();
                    actQuantityTextBox.Text = "";
                    rejectionPCSTextBox.Text = "";
                    expQuantityTextBox.Text = "";
                    noShotsStartTextBox.Text = "";
                    noShotsEndTextBox.Text = "";
                    noShotsTextBox.Text = "";
                    //downTimeTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    Application["postOprReq"] = "";
                    noShotsStartTextBox.ReadOnly = true;
                    noShotsEndTextBox.ReadOnly = true;
                    rejectionPCSTextBox.ReadOnly = true;
                    //downTimeTextBox.ReadOnly = true;
                }
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                    if (Application["editFlag"] is true)
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
                        }
                            Application["newActualQty"] = true;
                    }
                    else
                    {
                        Application["newActualQty"] = false;
                    }
                    actQuantityTextBox.Text = Convert.ToString(((int.Parse(noShotsTextBox.Text) * Convert.ToInt32(Application["noOfCavities"])) - int.Parse(rejectionPCSTextBox.Text)));
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void calculateEfficiency()
        {
            SqlConnection con = new SqlConnection(settings.ToString());

            int downTime = 0;
            foreach (GridViewRow row in downTimeGridView.Rows)
            {
                Label Lblqty = (Label)row.FindControl("downTimeLbl");
                if (Lblqty.Text != "")
                {
                    downTime += int.Parse(Lblqty.Text);
                }
            }
            using (con)
            {
                con.Open();
                if (Application["noOfCavities"] is null)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT no_of_cavities FROM parts_master WHERE part_name= '" + partNameDropDownList.SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["noOfCavities"] = reader["no_of_cavities"].ToString();
                        }
                        reader.Close();
                    }
                }
                if (Application["cycleTime"] is null)
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT cycle_time FROM parts_master WHERE part_name= '" + partNameDropDownList.SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["cycleTime"] = reader["cycle_time"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            con.Close();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["cycleTime"] + "')", true);
            if (Application["cycleTime"].ToString() != "" && Application["noOfCavities"].ToString() != "")
            {
                efficiencyTextBox.Text = Convert.ToString(Math.Round(((float.Parse(actQuantityTextBox.Text) + ((float.Parse(downTime.ToString()) * (3600 / Convert.ToDouble(Application["cycleTime"]))) * 0.9) * Convert.ToDouble(Application["noOfCavities"])) / float.Parse(expQuantityTextBox.Text)) * 100, 2));
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (partNameDropDownList.SelectedItem.Text == "Select Part Name" || materialGradeDropDownList.SelectedItem.Text == "Select Material Grade" || noShotsTextBox.Text == "" || actQuantityTextBox.Text == "" || machineUsedDropDownList.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
                }
                else
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    if (Application["editFlag"] is true)
                    {
                        if (Application["postOprReq"] is null)
                        {
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
                        }
                        /************FPA CHANGES************/
                        if (Application["newActualQty"] is true)
                        {
                            int newQty = int.Parse(actQuantityTextBox.Text);
                            int prevQty = 0;
                            int diff = 0;
                            SqlCommand cmdFpaFetch = new SqlCommand("SELECT prev_qty FROM fpa_wip_check WHERE prev_qty = (SELECT MAX(prev_qty) FROM fpa_wip_check WHERE operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "' AND date_dpr = @dateDpr AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "')", con);
                            cmdFpaFetch.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateTextBox.Text.ToString()));
                            SqlDataReader rdr = cmdFpaFetch.ExecuteReader();
                            while (rdr.Read())
                            {
                                prevQty = int.Parse(rdr["prev_qty"].ToString());
                            }
                            rdr.Close();
                            if (prevQty != 0)
                            {
                                diff = int.Parse(actQuantityTextBox.Text) - prevQty;
                            }
                            SqlCommand cmdFpaSelect = new SqlCommand("SELECT fpa_no,wip_qty,prev_qty FROM fpa_wip_check WHERE operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "' AND date_dpr = @dateDpr AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name = '"+partNameDropDownList.SelectedItem.Text+"'", con);
                            cmdFpaSelect.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateTextBox.Text.ToString()));
                            SqlDataAdapter sda = new SqlDataAdapter(cmdFpaSelect);
                            DataSet myDataSet = new DataSet();
                            sda.Fill(myDataSet);
                            ArrayList arr = new ArrayList();
                            foreach (DataRow dtRow in myDataSet.Tables[0].Rows)
                            {
                                arr.Add(dtRow);
                            }
                            foreach (object data in arr)
                            {
                                int checkWip = 0;
                                checkWip = int.Parse(((DataRow)data)["wip_qty"].ToString()) + diff;
                                if (checkWip >= 0)
                                {
                                    Application["status"] = "success";
                                }
                                else
                                {
                                    Application["status"] = "failure";
                                    break;
                                }
                            }
                            if (Application["status"].ToString() == "success")
                            {
                                foreach (object data in arr)
                                {
                                    int calculatedWip = 0;
                                    calculatedWip = int.Parse(((DataRow)data)["wip_qty"].ToString()) + diff;
                                    int newPrevQty = 0;
                                    newPrevQty = int.Parse(((DataRow)data)["prev_qty"].ToString()) + diff;
                                    SqlCommand cmdUpdateWip = new SqlCommand("UPDATE fpa_wip_check SET wip_qty = '" + calculatedWip + "', prev_qty = '" + newPrevQty + "' WHERE fpa_no = '" + ((DataRow)data)["fpa_no"].ToString() + "' AND operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "' AND date_dpr = @dateDpr AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name = '" + partNameDropDownList.SelectedItem.Text + "'", con);
                                    cmdUpdateWip.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateTextBox.Text.ToString()));
                                    cmdUpdateWip.ExecuteNonQuery();
                                    lblErrorMessage.Text = "";
                                }
                            }
                            else
                            {
                                lblErrorMessage.Text = "Quantity exceeds WIP quantity";
                            }
                        }
                        SqlCommand cmd2 = new SqlCommand("UPDATE production SET operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "', part_name = '" + partNameDropDownList.SelectedItem.Text + "', material_grade = '" + materialGradeDropDownList.SelectedItem.Text + "', machine_no = '" + machineUsedDropDownList.SelectedItem.Text + "', shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "', exp_qty = '" + expQuantityTextBox.Text + "', no_of_shots_start = '" + noShotsStartTextBox.Text + "', no_of_shots_end = '" + noShotsEndTextBox.Text + "', no_of_shots = '" + noShotsTextBox.Text + "', rejection_pcs = '" + rejectionPCSTextBox.Text + "', rejection_kgs = '" + rejectionKGSTextBox.Text + "', act_qty = '" + actQuantityTextBox.Text + "', efficiency = '" + efficiencyTextBox.Text + "', date_dpr = @dateDpr, post_opr_req = '" + Application["postOprReq"].ToString() + "' WHERE id = '" + Application["id"] + "'", con);
                        cmd2.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateTextBox.Text.ToString()));
                        cmd2.ExecuteNonQuery();

                        Application["newActualQty"] = null;
                        Application["editFlag"] = null;
                        Application["id"] = null;
                        Application["partName"] = null;
                        Application["materialGrade"] = null;
                        Application["operatorName"] = null;
                        Application["dateDpr"] = null;
                        Application["dprNo"] = null;
                        Application["shiftDetails"] = null;
                        Application["status"] = null;
                        Response.Redirect("~/DPRList.aspx");
                    }
                    else
                    {
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO production(dpr_no,operator_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots_start,no_of_shots_end,no_of_shots,rejection_pcs,rejection_kgs,act_qty,efficiency,date_dpr,post_opr_req,fpa_status)VALUES('"+dprNo.Text.Trim()+"', '" + operatorNameDropDownList.SelectedItem.Text + "', '" + partNameDropDownList.SelectedItem.Text + "', '" + materialGradeDropDownList.SelectedItem.Text + "','" + machineUsedDropDownList.SelectedItem.Text + "', '" + shiftDetailsDropDownList.SelectedItem.Text + "', '" + expQuantityTextBox.Text + "', '" + noShotsStartTextBox.Text + "','" + noShotsEndTextBox.Text + "','" + noShotsTextBox.Text + "', '" + rejectionPCSTextBox.Text + "', '" + rejectionKGSTextBox.Text + "', '" + actQuantityTextBox.Text + "', '" + efficiencyTextBox.Text + "', @dateDpr, '"+Application["postOprReq"].ToString()+"', '')", con);
                        cmd2.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateTextBox.Text.ToString()));
                        cmd2.ExecuteNonQuery();

                        Application["id"] = null;
                        Application["partName"] = null;
                        Application["materialGrade"] = null;
                        Application["operatorName"] = null;
                        Application["dateDpr"] = null;
                        Application["dprNo"] = null;
                        Application["shiftDetails"] = null;
                        Response.Redirect("~/DPRList.aspx");
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
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
                    if (int.Parse(noShotsStartTextBox.Text.ToString().Trim()) > int.Parse(noShotsEndTextBox.Text.ToString().Trim()) || int.Parse(noShotsEndTextBox.Text.ToString().Trim()) == 0 || int.Parse(noShotsStartTextBox.Text.ToString().Trim()) == int.Parse(noShotsEndTextBox.Text.ToString().Trim()))
                    {
                        noShotsEndTextBox.Text = string.Empty;
                        noShotsTextBox.Text = string.Empty;
                        validationShots.Text = "Value cannot be 0 or less than starting value of shots";
                        validationShots.Font.Size = 8;
                    }
                    else
                    {
                        noShotsTextBox.Text = (int.Parse(noShotsEndTextBox.Text.ToString().Trim()) - int.Parse(noShotsStartTextBox.Text.ToString().Trim())).ToString();
                        validationShots.Text = string.Empty;
                        rejectionPCSTextBox.ReadOnly = false;
                        rejectionPCSTextBox.Text = string.Empty;
                        actQuantityTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    noShotsTextBox.Text = string.Empty;
                    rejectionPCSTextBox.Text = string.Empty;
                    actQuantityTextBox.Text = string.Empty;
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
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                string query = "";
                sqlCon.Open();
                query = "DELETE FROM down_time_production_master WHERE dpr_no = @dprNo";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@dprNo", dprNo.Text.ToString());
                sqlCmd.ExecuteNonQuery();

            }
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;

            Application["dateDpr"] = null;
            Application["partName"] = null;
            Application["shiftDetails"] = null;
            Application["operatorName"] = null;
            Response.Redirect("~/DPRList.aspx");
        }

        protected void rejectionPCSTextBox_TextChanged(object sender, EventArgs e)
        {
            if (noShotsTextBox.Text != "" && rejectionPCSTextBox.Text != "")
            {
                calculateActualQuantity();
            }
            else
            {
                actQuantityTextBox.Text = "";
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

        protected void downTimeGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    //string partName = Application["partName"].ToString();
                    GridViewRow row = downTimeGridView.FooterRow;
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "'"+Application["PostOperation"].ToString()+"'", true);
                    using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO down_time_production_master (down_time_code,down_time_hours,dpr_no) OUTPUT INSERTED.Id VALUES (@downTimeCode,@downTimeHours,@dprNo)";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        
                        cmd.Parameters.AddWithValue("@downTimeCode", (row.FindControl("downTimeDropDownListFooter") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@downTimeHours", (row.FindControl("txtdownTimeFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@dprNo", dprNo.Text.ToString().Trim());
                        //cmd.ExecuteNonQuery();

                        var downTimeId = (int)cmd.ExecuteScalar();

                        string query2 = "INSERT INTO down_time_production (down_time_code,down_time_hours,dpr_no,down_time_id) VALUES (@downTimeCode,@downTimeHours,@dprNo,@downTimeId)";
                        SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
                        
                        cmd2.Parameters.AddWithValue("@downTimeCode", (row.FindControl("downTimeDropDownListFooter") as DropDownList).SelectedItem.Text.Trim());
                        cmd2.Parameters.AddWithValue("@downTimeHours", (row.FindControl("txtdownTimeFooter") as TextBox).Text.Trim());
                        cmd2.Parameters.AddWithValue("@dprNo", dprNo.Text.ToString().Trim());
                        cmd2.Parameters.AddWithValue("@downTimeId", downTimeId);
                        cmd2.ExecuteNonQuery();

                        sqlCon.Close();

                        if (Application["editFlag"] is true)
                        {
                            LoadEditValuesInController();
                        }
                        else
                        {
                            Application["rowCommand"] = true;
                            LoadValuesInController();
                        }
                        lblSuccessMessage.Text = "Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        protected void downTimeGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ assemblyGridView.DataKeys[e.RowIndex].Value.ToString() + "')", true);

                if (Application["editFlag"] is true)
                {
                    string query = "DELETE FROM down_time_production WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(downTimeGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                }
                string query2 = "DELETE FROM down_time_production WHERE down_time_id = @id";
                SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
                sqlCmd2.Parameters.AddWithValue("@id", Convert.ToInt32(downTimeGridView.DataKeys[e.RowIndex].Value.ToString()));
                sqlCmd2.ExecuteNonQuery();

                string query3 = "DELETE FROM down_time_production_master WHERE id = @id";
                SqlCommand sqlCmd3 = new SqlCommand(query3, sqlCon);
                sqlCmd3.Parameters.AddWithValue("@id", Convert.ToInt32(downTimeGridView.DataKeys[e.RowIndex].Value.ToString()));
                sqlCmd3.ExecuteNonQuery();

                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
                else
                {
                    LoadValuesInController();
                }
                lblSuccessMessage.Text = "Selected Record Deleted";
                lblErrorMessage.Text = "";
            }
        }

        protected void downTimeCodeDropDownList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            var drop = (DropDownList)downTimeGridView.FooterRow.FindControl("downTimeDropDownListFooter");
            var T = (TextBox)downTimeGridView.FooterRow.FindControl("txtdownTimeFooter");
            var img = (ImageButton)downTimeGridView.FooterRow.FindControl("AddImgBtn");

            if (drop.SelectedItem.Text == "N/A")
            {
                if (noShotsEndTextBox.Text.ToString() != "" && expQuantityTextBox.Text.ToString() != "")
                {
                    T.ReadOnly = true;
                    T.Text = "";
                    T.BackColor = Color.WhiteSmoke;
                    img.Visible = false;
                    calculateEfficiency();
                }
            }
            else if (drop.SelectedItem.Text == "Select Code")
            {
                T.ReadOnly = true;
                T.BackColor = Color.WhiteSmoke;
                img.Visible = false;
            }
            else
            {
                T.ReadOnly = false;
                T.BackColor = Color.White;
                img.Visible = true;
            }
        }
    }
}