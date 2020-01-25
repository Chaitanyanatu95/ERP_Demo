using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class FPA : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
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
                    SqlConnection connection1 = new SqlConnection(settings.ToString());
                    connection1.Open();

                    //Delete rejection history from master
                    using (SqlCommand sqlCmd = new SqlCommand("DELETE FROM production_rejection_history_master", connection1))
                    {
                        sqlCmd.ExecuteNonQuery();
                    }
                    actualQtyTextBox.ReadOnly = true;
                    LoadProductRejHisValues();

                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT worker_name FROM worker_master", connection1))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        workerNameDropDownList.DataSource = reader;
                        workerNameDropDownList.DataBind();
                        reader.Close();
                        workerNameDropDownList.Items.Insert(0, new ListItem("Select Worker Name", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT shift_time FROM shift_master", connection1))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        workerShiftDetails.DataSource = reader;
                        workerShiftDetails.DataBind();
                        reader.Close();
                        workerShiftDetails.Items.Insert(0, new ListItem("Select Shift", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM parts_master", connection1))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        partNameDropDownList.DataSource = reader;
                        partNameDropDownList.DataBind();
                        reader.Close();
                        partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
                    }
                    connection1.Close();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void partNameChanged(object sender, EventArgs e)
        {
            try
            {
                if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
                {
                    Application["tempPartName"] = string.Empty;
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["tempPartName"] = reader["part_name"].ToString();
                        }
                        reader.Close();

                        if (Application["tempPartName"].ToString() != "")
                        {
                            operatorNameDropDownList.Items.Clear();
                            dateDropDownList.Items.Clear();
                            noOfPartsTextBox.Text = "";
                            LoadShiftDetails();
                        }
                        else
                        {
                            shiftDetailsDropDownList.Items.Clear();
                            operatorNameDropDownList.Items.Clear();
                            operationTypeList.Items.Clear();
                            dateDropDownList.Items.Clear();
                            noOfPartsTextBox.Text = "";
                        }
                    }
                    using (SqlCommand cmmd = new SqlCommand("SELECT DISTINCT type FROM post_operation_details WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                    {
                        SqlDataReader Rreader = cmmd.ExecuteReader();
                        operationTypeList.DataSource = Rreader;
                        operationTypeList.DataBind();
                        Rreader.Close();
                        operationTypeList.Items.Insert(0, new ListItem("Select Operation", ""));
                        operationTypeList.Items.Insert(1, new ListItem("N/A", ""));
                    }
                    con.Close();
                }
                else
                {
                    shiftDetailsDropDownList.Items.Clear();
                    operatorNameDropDownList.Items.Clear();
                    operationTypeList.Items.Clear();
                    dateDropDownList.Items.Clear();
                    noOfPartsTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadShiftDetails()
        {
            try
            {
                if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT shift_details FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        shiftDetailsDropDownList.DataSource = reader;
                        shiftDetailsDropDownList.DataBind();
                        reader.Close();
                        shiftDetailsDropDownList.Items.Insert(0, new ListItem("Select Shift", ""));
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

        protected void shiftChanged(object sender, EventArgs e)
        {
            try
            {
                if (shiftDetailsDropDownList.SelectedItem.Text != "Select Shift")
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT operator_name FROM production WHERE shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        operatorNameDropDownList.DataSource = reader;
                        operatorNameDropDownList.DataBind();
                        reader.Close();
                        operatorNameDropDownList.Items.Insert(0, new ListItem("Select Operator Name", ""));
                    }
                    con.Close();
                }
                else
                {
                    operatorNameDropDownList.Items.Clear();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = true;
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                    dateDropDownList.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void workerChanged(object sender, EventArgs e)
        {
            try
            {
                if (workerNameDropDownList.SelectedItem.Text != "Select Worker")
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT date_dpr FROM production WHERE shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND operator_name ='" + operatorNameDropDownList.SelectedItem.Text + "'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        dateDropDownList.DataSource = reader;
                        dateDropDownList.DataTextFormatString = "{0:d}";
                        dateDropDownList.DataBind();
                        reader.Close();
                        dateDropDownList.Items.Insert(0, new ListItem("Select Date", ""));
                    }
                    con.Close();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = true;
                }
                else
                {
                    operatorNameDropDownList.Items.Clear();
                    operationTypeList.Items.Clear();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = true;
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                    dateDropDownList.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void dateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateDropDownList.SelectedItem.Text != "Select Date")
                {
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = true;
                }
                else
                {
                    dateDropDownList.Items.Clear();
                    operationTypeList.Items.Clear();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = true;
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void operationTypeListChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateDropDownList.SelectedItem.Text != null)
                {
                    Application["newWip"] = string.Empty;
                    Application["totalQty"] = string.Empty;
                    Application["totalQtyFpa"] = string.Empty;
                    Application["newTotalQty"] = false;
                    Application["fpaStatus"] = false;
                    actualQtyTextBox.ReadOnly = false;

                    SqlConnection sqlCon = new SqlConnection(settings.ToString());
                    sqlCon.Open();
                    using (SqlCommand sqlnewCmd = new SqlCommand("SELECT target_quantity FROM post_operation_details WHERE type='" + operationTypeList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "'", sqlCon))
                    {
                        SqlDataReader reader = sqlnewCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            noOfPartsTextBox.Text = reader["target_quantity"].ToString();
                        }
                        //System.Diagnostics.Debug.WriteLine(noOfPartsTextBox.Text.ToString());
                        reader.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT act_qty FROM production WHERE date_dpr = CONVERT(DATETIME, @DateDpr , 105)", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["totalQty"] = reader["act_qty"].ToString();
                        }
                        reader.Close();
                    }

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["totalQty"] + "')", true);

                    using (SqlCommand sqlCmd = new SqlCommand("SELECT wip_qty FROM fpa_operation WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date = CONVERT(DATETIME, @DateDpr , 105) AND shift='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeList.SelectedItem.Text + "'", sqlCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["newWip"] = reader["wip_qty"].ToString();
                        }
                        reader.Close();
                    }
                    
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["newWip"] + "')", true);

                    if (Application["newWip"].ToString() != "")
                    {
                        //SET WIP AS NEW TOTAL QTY
                        Application["newTotalQty"] = true;
                        Application["totalQtyFpa"] = int.Parse(Application["newWip"].ToString());
                    }
                    else
                    {
                        Application["newTotalQty"] = false;
                        Application["totalQtyFpa"] = int.Parse(Application["totalQty"].ToString());
                    }
                    
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["newTotalQty"] + "')", true);
                    
                    sqlCon.Close();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                }
                else
                {
                    operationTypeList.Items.Clear();
                    noOfPartsTextBox.Text = "";
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = true;
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void FpaRejectionQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Application["prodRejQty"] = 0;
                Application["otherWipQty"] = string.Empty;
                SqlConnection sqlCon = new SqlConnection(settings.ToString());
                sqlCon.Open();
                int okQty = int.Parse(actualQtyTextBox.Text);
                int fpaRejectedQty = int.Parse(FpaRejectionQtyTextBox.Text);
                int prodRejectionQty = int.Parse(Application["prodRejQty"].ToString());
                actualQtyTextBox.ReadOnly = false;

                foreach (GridViewRow row in prodRejHisGrid.Rows)
                {
                    Label Lblbasic = (Label)row.FindControl("rejectionQuantityLbl");
                    if (Lblbasic.Text != "")
                    {
                        prodRejectionQty += int.Parse(Lblbasic.Text);
                    }
                }
                if (Application["totalQtyFpa"].ToString() != "0" && okQty != 0)
                {
                    Application["wipQty"] = (int.Parse(Application["totalQtyFpa"].ToString()) - (okQty + (prodRejectionQty + fpaRejectedQty))).ToString();
                }

                if ((okQty + fpaRejectedQty + prodRejectionQty) <= int.Parse(Application["totalQtyFpa"].ToString()))
                {
                    if (Application["wipQty"].ToString() != "")
                    {
                        int targetQty = int.Parse(noOfPartsTextBox.Text);
                        int totalTime = int.Parse(timeTextBox.Text);
                        if (Application["totalQtyFpa"].ToString() != "0" && okQty != 0 && targetQty != 0)
                        {
                            efficiencyTextBox.Text = Math.Floor((((okQty + prodRejectionQty) - (fpaRejectedQty)) / (targetQty * totalTime * 0.9) * 100)).ToString();
                        }
                        if (Application["newTotalQty"] is true)
                        {
                            //GET POST OPERATION SET FOR THE PART
                            using (SqlCommand sqlCmd = new SqlCommand("SELECT type FROM post_operation_details WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", sqlCon))
                            {
                                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                                DataSet myDataSet = new DataSet();
                                sda.Fill(myDataSet);
                                ArrayList arr = new ArrayList();
                                foreach (DataRow dtRow in myDataSet.Tables[0].Rows)
                                {
                                    arr.Add(dtRow);
                                }

                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ arr.Count + "')", true);

                                foreach (object data in arr)
                                {
                                    if (((DataRow)data)["type"].ToString() != operationTypeList.SelectedItem.Text)
                                    {
                                        using (SqlCommand sqlCmd2 = new SqlCommand("SELECT wip_qty FROM fpa_operation WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date=CONVERT(DATETIME, @DateDpr , 105) AND shift='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type = '" + ((DataRow)data)["type"].ToString() + "'", sqlCon))
                                        {
                                            sqlCmd2.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                                            SqlDataReader reader = sqlCmd2.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                Application["otherWipQty"] = reader["wip_qty"].ToString();
                                            }
                                            reader.Close();
                                        }
                                        if (Application["otherWipQty"].ToString() != "")
                                        {
                                            if (int.Parse(Application["otherWipQty"].ToString()) == 0 && int.Parse(Application["wipQty"].ToString()) == 0)
                                            {
                                                Application["fpaStatus"] = true;
                                            }
                                            else
                                            {
                                                Application["fpaStatus"] = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Application["fpaStatus"] = false;
                                            break;
                                        }
                                    }
                                    else if (((DataRow)data)["type"].ToString() == operationTypeList.SelectedItem.Text)
                                    {
                                        if (arr.Count > 1)
                                        {
                                            using (SqlCommand sqlCmd3 = new SqlCommand("SELECT wip_qty FROM fpa_operation WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date= CONVERT(DATETIME, @DateDpr , 105) AND shift='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type != '" + ((DataRow)data)["type"].ToString() + "'", sqlCon))
                                            {
                                                sqlCmd3.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                                                SqlDataReader reader = sqlCmd3.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    Application["otherWipQty"] += reader["wip_qty"].ToString();
                                                }
                                                reader.Close();
                                            }
                                        }
                                        else
                                        {
                                            Application["otherWipQty"] = 0;
                                        }

                                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["otherWipQty"].ToString() + "')", true);
                                        
                                        if (Application["otherWipQty"].ToString() != "")
                                        {
                                            if (int.Parse(Application["otherWipQty"].ToString()) == 0 && int.Parse(Application["wipQty"].ToString()) == 0)
                                            {
                                                Application["fpaStatus"] = true;
                                            }
                                            else
                                            {
                                                Application["fpaStatus"] = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Application["fpaStatus"] = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quantity exceeds WIP quantity')", true);
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.Text = "";

                    string query = "DELETE FROM production_rejection_history_master;";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void prodRejHisGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string partName = partNameDropDownList.SelectedItem.Text.ToString().Trim();
                    using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO production_rejection_history_master (rejection_code,rejection_quantity,part_name) VALUES(@rejectionCode,@rejectionQuantity,'" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@rejectionCode", (prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@rejectionQuantity", (prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter") as TextBox).Text.Trim());
                        cmd.ExecuteNonQuery();

                        string query2 = "INSERT INTO production_rejection_history (rejection_code,rejection_quantity,part_name) VALUES(@rejectionCode,@rejectionQuantity,'" + partName + "')";
                        SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
                        cmd2.Parameters.AddWithValue("@rejectionCode", (prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd2.Parameters.AddWithValue("@rejectionQuantity", (prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter") as TextBox).Text.Trim());
                        cmd2.ExecuteNonQuery();

                        sqlCon.Close();
                        LoadProductRejHisValues();
                        lblSuccessMessage.Text = "Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void prodRejHisGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM production_rejection_history_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(prodRejHisGrid.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();

                    string query2 = "DELETE FROM production_rejection_history WHERE id = @id";
                    SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
                    sqlCmd2.Parameters.AddWithValue("@id", Convert.ToInt32(prodRejHisGrid.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd2.ExecuteNonQuery();

                    sqlCon.Close();
                    LoadProductRejHisValues();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void BindProductionRejHistoryGrid()
        {
            prodRejHisGrid.DataSource = (DataTable)Application["ProductionRejectionDetails"];
            prodRejHisGrid.DataBind();
        }

        protected void LoadProductRejHisValues()
        {
            try
            {
                /************* PRODUCTION TAG DETAILS *****************/
                DataTable dtProductionRejHis = new DataTable();
                dtProductionRejHis.Columns.AddRange(new DataColumn[2] { new DataColumn("REJECTION CODE"), new DataColumn("REJECTION QUANTITY") });
                Application["ProductionRejectionDetails"] = dtProductionRejHis;
                BindProductionRejHistoryGrid();

                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history_master", sqlCon);
                    sqlDa.Fill(dtProductionRejHis);
                    sqlCon.Close();
                }
                if (dtProductionRejHis.Rows.Count > 0)
                {
                    BindProductionRejHistoryGrid();
                }
                else
                {
                    dtProductionRejHis.Rows.Add(dtProductionRejHis.NewRow());
                    BindProductionRejHistoryGrid();
                    prodRejHisGrid.Rows[0].Cells.Clear();
                    prodRejHisGrid.Rows[0].Cells.Add(new TableCell());
                    prodRejHisGrid.Rows[0].Cells[0].ColumnSpan = dtProductionRejHis.Columns.Count;
                    //prodRejHisGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    prodRejHisGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application["wipQty"].ToString() == "" || partNameDropDownList.SelectedItem.Text == "" || FpaRejectionQtyTextBox.Text == "" || efficiencyTextBox.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
                }
                else
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();

                    //Delete rejection history from master
                    string query = "DELETE FROM production_rejection_history_master;";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.ExecuteNonQuery();

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["totalQtyFpa"] + "'", true);

                    if (Application["newTotalQty"] is true && Application["okQty"] is true)
                    {
                        if (Application["fpaStatus"] is true)
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE production set fpa_status='CLOSED' WHERE date_dpr = @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                            cmd1.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd1.ExecuteNonQuery();

                            SqlCommand cmd2 = new SqlCommand("UPDATE production set post_opr_req='CLOSED' WHERE date_dpr =  @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                            cmd2.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd2.ExecuteNonQuery();
                        }
                        SqlCommand cmd3 = new SqlCommand("UPDATE fpa_operation set wip_qty='" + Application["wipQty"] + "' WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date =  @DateDpr AND shift = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeList.SelectedItem.Text + "'", con);
                        cmd3.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd3.ExecuteNonQuery();

                        SqlCommand cmd4 = new SqlCommand("INSERT INTO fpa(operator_name,worker_shift,worker_date,worker_name,part_name,date,shift,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,efficiency)VALUES('" + operatorNameDropDownList.SelectedItem.Text + "','" + workerShiftDetails.SelectedItem.Text + "', @WorkerDate, '"+workerNameDropDownList.SelectedItem.Text+"','"+partNameDropDownList.SelectedItem.Text+ "', @DateDpr,'" + shiftDetailsDropDownList.SelectedItem.Text + "','" + operationTypeList.SelectedItem.Text + "','" + Application["totalQty"].ToString() + "','" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "','" + actualQtyTextBox.Text + "','" + FpaRejectionQtyTextBox.Text + "','" + efficiencyTextBox.Text + "')", con);
                        cmd4.Parameters.AddWithValue("@WorkerDate", DateTime.Parse(dateSelection.Value));
                        cmd4.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd4.ExecuteNonQuery();

                        Application["okQty"] = false;
                        Application["newTotalQty"] = string.Empty;
                        con.Close();
                        Response.Redirect("~/Default.aspx");
                    }
                    else if (Application["newTotalQty"] is false && Application["okQty"] is true)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO fpa(operator_name,worker_shift,worker_date,worker_name,part_name,date,shift,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,efficiency)VALUES('" + operatorNameDropDownList.SelectedItem.Text + "','" + workerShiftDetails.SelectedItem.Text + "',@WorkerDate,'" + workerNameDropDownList.SelectedItem.Text + "','" + partNameDropDownList.SelectedItem.Text + "',@DateDpr,'" + shiftDetailsDropDownList.SelectedItem.Text + "', '" + operationTypeList.SelectedItem.Text + "', '" + Application["totalQty"].ToString() + "', '" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "', '" + actualQtyTextBox.Text + "', '" + FpaRejectionQtyTextBox.Text + "', '" + efficiencyTextBox.Text + "')", con);
                        cmd.Parameters.AddWithValue("@WorkerDate", DateTime.Parse(dateSelection.Value));
                        cmd.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("INSERT INTO fpa_operation(operator_name,worker_name,part_name,date,shift,operation_type,wip_qty,efficiency)VALUES('" + operatorNameDropDownList.SelectedItem.Text + "', '" + workerNameDropDownList.SelectedItem.Text + "', '" + partNameDropDownList.SelectedItem.Text + "', @DateDpr, '" + shiftDetailsDropDownList.SelectedItem.Text + "', '" + operationTypeList.SelectedItem.Text + "', '" + Application["wipQty"].ToString() + "', '" + efficiencyTextBox.Text + "')", con);
                        cmd2.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd2.ExecuteNonQuery();

                        SqlCommand cmd3 = new SqlCommand("UPDATE production set fpa_status='OPEN' WHERE date_dpr = @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                        cmd3.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd3.ExecuteNonQuery();


                        Application["okQty"] = false;
                        Application["newTotalQty"] = string.Empty;

                        con.Close();
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(settings.ToString());
            con.Open();
            // Delete rejection history from master
            string query = "DELETE FROM production_rejection_history_master;";
            SqlCommand sqlCmd = new SqlCommand(query, con);
            sqlCmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("~/Default.aspx");
        }

        protected void rejectionCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = prodRejHisGrid.FooterRow;

                if (((DropDownList)row.FindControl("rejectionCodeDropDownList")).SelectedItem.Text == "N/A")
                {
                    ((ImageButton)row.FindControl("AddImgBtn")).Visible = false;
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).Text = "0";
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).ReadOnly = true;
                }
                else
                {
                    ((ImageButton)row.FindControl("AddImgBtn")).Visible = true;
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).ReadOnly = false;
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void actualQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Application["okQty"] = string.Empty;
                if (actualQtyTextBox.Text != "")
                {
                    if (int.Parse(actualQtyTextBox.Text.Trim()) > int.Parse(Application["totalQtyFpa"].ToString()))
                    {
                        errorLbl.Text = "Quantity should not exceed total quantity!";
                        errorLbl.ForeColor = System.Drawing.Color.Red;
                        actualQtyTextBox.Text = "";
                        FpaRejectionQtyTextBox.Text = "";
                        efficiencyTextBox.Text = "";
                        Application["okQty"] = false;
                    }
                    else if (int.Parse(actualQtyTextBox.Text.Trim()) == 0)
                    {
                        errorLbl.Text = "Quantity cannot be zero!";
                        errorLbl.ForeColor = System.Drawing.Color.Red;
                        actualQtyTextBox.Text = "";
                        FpaRejectionQtyTextBox.Text = "";
                        efficiencyTextBox.Text = "";
                        Application["okQty"] = false;
                    }
                    else
                    {
                        Application["okQty"] = true;
                        errorLbl.Text = "";
                        FpaRejectionQtyTextBox.Text = "";
                        efficiencyTextBox.Text = "";
                    }
                }
                else
                {
                    actualQtyTextBox.Text = "";
                    FpaRejectionQtyTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    Application["okQty"] = false;
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