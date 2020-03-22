using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
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
            else
            {
                Application["rowCommand"] = false;
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
                    SqlConnection connection1 = new SqlConnection(settings.ToString());
                    connection1.Open();

                    if (Application["editFlag"] is true)
                    {
                        LoadEditValuesInController();
                    }
                    else
                    {
                        //Delete rejection history from master
                        /*using (SqlCommand sqlCmd = new SqlCommand("DELETE FROM production_rejection_history_master", connection1))
                        {
                            sqlCmd.ExecuteNonQuery();
                        }*/

                        actualQtyTextBox.ReadOnly = true;
                        productionTypeDropDownList.Items.Insert(0, new ListItem("Select Type"));

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

                        /*using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM parts_master", connection1))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            partNameDropDownList.DataSource = reader;
                            partNameDropDownList.DataBind();
                            reader.Close();
                            partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
                        }*/

                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM fpa ORDER BY ID DESC", connection1))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Application["input"] = reader["fpa_no"];
                            }
                            reader.Close();
                            if (Application["input"] == null)
                                Application["input"] = "0";
                            int input = int.Parse(Regex.Replace(Application["input"].ToString(), "[^0-9]+", string.Empty));
                            fpaTextBox.Text = "FPATR#" + (input + 1);
                        }

                        string postSelectQuery = "SELECT fpa_no FROM fpa where fpa_no= '" + fpaTextBox.Text.Trim() + "'";
                        SqlCommand selCmd = new SqlCommand(postSelectQuery, connection1);
                        Application["fpaSelectData"] = "";
                        SqlDataReader selReader = selCmd.ExecuteReader();
                        while (selReader.Read())
                        {
                            if (selReader["fpa_no"].ToString() != "")
                            {
                                Application["fpaSelectData"] = selReader["fpa_no"].ToString();
                            }
                        }
                        selReader.Close();

                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["rowCommand"].ToString() + "')", true);

                        if (Application["fpaSelectData"].ToString() == "" && Application["rowCommand"] is false)
                        {
                            string postQuery = "DELETE FROM production_rejection_history_master where fpa_no= '" + fpaTextBox.Text.Trim() + "'";
                            SqlCommand cmmd = new SqlCommand(postQuery, connection1);
                            cmmd.ExecuteNonQuery();

                            string postQuery2 = "DELETE FROM production_rejection_history where fpa_no= '" + fpaTextBox.Text.Trim() + "'";
                            SqlCommand cmmd2 = new SqlCommand(postQuery2, connection1);
                            cmmd2.ExecuteNonQuery();
                            Application["fpaSelectData"] = null;
                        }
                    }
                    LoadProdRejGrid();
                    connection1.Close();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadProdRejGrid()
        {
            try
            {
                /************** DOWNTIME ****************/
                DataTable dtProdRej = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    SqlDataAdapter sqlDa;
                    if (Application["editFlag"] is true)
                    {
                        sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history WHERE fpa_no = '" + Application["fpaNo"].ToString().Trim() + "'", sqlCon);
                    }
                    else
                    {
                        sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history_master WHERE fpa_no = '" + fpaTextBox.Text.ToString() + "'", sqlCon);
                    }
                    sqlDa.Fill(dtProdRej);
                }
                if (dtProdRej.Rows.Count > 0)
                {
                    prodRejHisGrid.DataSource = dtProdRej;
                    prodRejHisGrid.DataBind();
                    var prodRejDropDown = (DropDownList)prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList");
                    var qty = (TextBox)prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    prodRejDropDown.Items.Insert(0, new ListItem("Select Code"));
                    prodRejDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)prodRejHisGrid.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                }
                else
                {
                    dtProdRej.Rows.Add(dtProdRej.NewRow());
                    prodRejHisGrid.DataSource = dtProdRej;
                    prodRejHisGrid.DataBind();
                    var prodRejDropDown = (DropDownList)prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList");
                    var qty = (TextBox)prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    prodRejDropDown.Items.Insert(0, new ListItem("Select Code"));
                    prodRejDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)prodRejHisGrid.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                    prodRejHisGrid.Rows[0].Cells.Clear();
                    prodRejHisGrid.Rows[0].Cells.Add(new TableCell());
                    prodRejHisGrid.Rows[0].Cells[0].ColumnSpan = dtProdRej.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    prodRejHisGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadProductRejHisValues()
        {
            try
            {
                /************** ASSEMBLY OPERATION ****************/
                DataTable dtTbl = new DataTable();
                dtTbl.Columns.AddRange(new DataColumn[2] { new DataColumn("DOWN TIME CODE"), new DataColumn("DOWN TIME(HRS)") });
                Application["ProductionRejectionDetails"] = dtTbl;
                Application["tempProdRej"] = "";
                BindProductionRejHistoryGrid();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    if (Application["editFlag"] is true)
                    {
                        string query = "SELECT fpa_no FROM fpa where Id = '" + Application["id"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["tempProdRej"] = reader["fpa_no"];
                        }
                        reader.Close();
                        if (Application["tempProdRej"].ToString() != "")
                        {
                            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history where fpa_no = '" + Application["fpaNo"].ToString() + "'", sqlCon);
                            Application["tempProdRej"] = null;
                            sqlDa.Fill(dtTbl);
                        }
                    }
                    else
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history_master where fpa_no = '" + fpaTextBox.Text.ToString() + "'", sqlCon);
                        sqlDa.Fill(dtTbl);
                        lblErrorMessage.Text = "";
                    }
                }
                if (dtTbl.Rows.Count > 0)
                {
                    BindProductionRejHistoryGrid();
                    var prodRejDropDown = (DropDownList)prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList");
                    var qty = (TextBox)prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    prodRejDropDown.Items.Insert(0, new ListItem("Select Code"));
                    prodRejDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)prodRejHisGrid.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                }
                else
                {
                    dtTbl.Rows.Add(dtTbl.NewRow());
                    BindProductionRejHistoryGrid();
                    var prodRejDropDown = (DropDownList)prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList");
                    var qty = (TextBox)prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    prodRejDropDown.Items.Insert(0, new ListItem("Select Code"));
                    prodRejDropDown.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)prodRejHisGrid.FooterRow.FindControl("AddImgBtn");
                    img.Visible = false;
                    prodRejHisGrid.Rows[0].Cells.Clear();
                    prodRejHisGrid.Rows[0].Cells.Add(new TableCell());
                    prodRejHisGrid.Rows[0].Cells[0].ColumnSpan = dtTbl.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    prodRejHisGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
                    String sqlquery = "SELECT fpa_no,operation_type,worker_name,worker_shift,worker_date,production_type,part_name,no_of_parts,total_time,exp_qty,rej_qty,efficiency FROM fpa where fpa_no = @fpaNo";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@fpaNo", (Application["fpaNo"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fpaTextBox.Text = reader["fpa_no"].ToString();
                            Application["workerName"] = reader["worker_name"].ToString();
                            Application["workerShift"] = reader["worker_shift"].ToString();
                            var workerDate = DateTime.Parse(reader["worker_date"].ToString()).Date;
                            dateSelectionTextBox.Text = workerDate.ToString("yyyy-MM-dd");
                            Application["prodType"] = reader["production_type"].ToString();
                            Application["partName"] = reader["part_name"].ToString();
                            Application["oprType"] = reader["operation_type"].ToString();
                            noOfPartsTextBox.Text = reader["no_of_parts"].ToString();
                            timeTextBox.Text = reader["total_time"].ToString();
                            actualQtyTextBox.Text = reader["exp_qty"].ToString();
                            FpaRejectionQtyTextBox.Text = reader["rej_qty"].ToString();
                            efficiencyTextBox.Text = reader["efficiency"].ToString();
                        }
                        reader.Close();
                    }

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["oprType"] + "')", true);

                    if (productionTypeDropDownList.SelectedItem.Text == "INHOUSE")
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT date_dpr FROM fpa WHERE fpa_no = '" + Application["fpaNo"].ToString().Trim() + "'", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            dateDropDownList.DataSource = reader;
                            dateDropDownList.DataTextFormatString = "{0:d}";
                            dateDropDownList.DataBind();
                            reader.Close();
                            //workerNameDropDownList.Items.Insert(0, new ListItem(Application["workerName"].ToString()));
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT operator_name FROM fpa WHERE fpa_no = '" + Application["fpaNo"].ToString().Trim() + "'", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            operatorNameDropDownList.DataSource = reader;
                            operatorNameDropDownList.DataBind();
                            reader.Close();
                            //workerNameDropDownList.Items.Insert(0, new ListItem(Application["workerName"].ToString()));
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT shift_details FROM fpa WHERE fpa_no = '" + Application["fpaNo"].ToString().Trim() + "'", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            shiftDetailsDropDownList.DataSource = reader;
                            shiftDetailsDropDownList.DataBind();
                            reader.Close();
                            //workerNameDropDownList.Items.Insert(0, new ListItem(Application["workerName"].ToString()));
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT worker_name FROM worker_master WHERE worker_name NOT IN('" + Application["workerName"].ToString().Trim() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        workerNameDropDownList.DataSource = reader;
                        workerNameDropDownList.DataBind();
                        reader.Close();
                        workerNameDropDownList.Items.Insert(0, new ListItem(Application["workerName"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT part_name FROM parts_master WHERE part_name NOT IN('" + Application["partName"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        partNameDropDownList.DataSource = reader;
                        partNameDropDownList.DataBind();
                        reader.Close();
                        partNameDropDownList.Items.Insert(0, new ListItem(Application["partName"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT type FROM post_operation_details WHERE type NOT IN ('" + Application["oprType"].ToString().Trim() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        operationTypeList.DataSource = reader;
                        operationTypeList.DataBind();
                        reader.Close();
                        operationTypeList.Items.Insert(0, new ListItem(Application["oprType"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT shift_time FROM shift_master WHERE shift_time NOT IN('" + Application["workerShift"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        workerShiftDetails.DataSource = reader;
                        workerShiftDetails.DataBind();
                        reader.Close();
                        workerShiftDetails.Items.Insert(0, new ListItem(Application["workerShift"].ToString()));
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
                            Application["tempPartName"] = reader["part_name"].ToString().Trim();
                        }
                        reader.Close();

                        if (Application["tempPartName"].ToString() != "")
                        {
                            using (SqlCommand cmd2 = new SqlCommand("SELECT DISTINCT part_no FROM parts_master WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                            {
                                SqlDataReader reader2 = cmd2.ExecuteReader();
                                while (reader2.Read())
                                {
                                    Application["tempPartNo"] = reader2["part_no"].ToString().Trim();
                                }
                                reader2.Close();
                            }

                            operatorNameDropDownList.Items.Clear();
                            dateDropDownList.Items.Clear();
                            noOfPartsTextBox.Text = "";
                            LoadShiftOprDateDetails();

                            using (SqlCommand cmd3 = new SqlCommand("SELECT DISTINCT type FROM post_operation_details WHERE part_no='" + Application["tempPartNo"].ToString().Trim() + "'", con))
                            {
                                SqlDataReader Rreader = cmd3.ExecuteReader();
                                operationTypeList.DataSource = Rreader;
                                operationTypeList.DataBind();
                                Rreader.Close();
                                operationTypeList.Items.Insert(0, new ListItem("Select Operation", ""));
                                operationTypeList.Items.Add(new ListItem("N/A", "-1"));
                            }
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

        protected void LoadShiftOprDateDetails()
        {
            try
            {
                if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
                {
                    if (productionTypeDropDownList.SelectedItem.Text == "INHOUSE")
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

                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT operator_name FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            operatorNameDropDownList.DataSource = reader;
                            operatorNameDropDownList.DataBind();
                            reader.Close();
                            operatorNameDropDownList.Items.Insert(0, new ListItem("Select Operator Name", ""));
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

        protected void operatorChanged(object sender, EventArgs e)
        {
            try
            {
                if (operatorNameDropDownList.SelectedItem.Text != "Select Operator")
                {
                    if (productionTypeDropDownList.SelectedItem.Text == "INHOUSE")
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
                }
                else
                {
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
                    SqlConnection sqlCon = new SqlConnection(settings.ToString());
                    sqlCon.Open();
                    using (SqlCommand sqlnewCmd = new SqlCommand("SELECT target_quantity FROM post_operation_details WHERE type='" + operationTypeList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "'", sqlCon))
                    {
                        SqlDataReader reader = sqlnewCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            noOfPartsTextBox.Text = reader["target_quantity"].ToString();
                        }
                        reader.Close();
                    }
                    
                    sqlCon.Close();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.ReadOnly = false;
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
                        string query = "INSERT INTO production_rejection_history_master (rejection_code,rejection_quantity,fpa_no,part_name) OUTPUT INSERTED.Id VALUES(@rejectionCode,@rejectionQuantity,@fpaNo,'" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@rejectionCode", (prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@rejectionQuantity", (prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@fpaNo", fpaTextBox.Text);

                        var prodRejId = (int)cmd.ExecuteScalar();

                        string query2 = "INSERT INTO production_rejection_history (rejection_code,rejection_quantity,fpa_no,part_name,prod_rej_id)VALUES(@rejectionCode,@rejectionQuantity,@fpaNo,'" + partName + "',@prodRejId)";
                        SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
                        cmd2.Parameters.AddWithValue("@rejectionCode", (prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd2.Parameters.AddWithValue("@rejectionQuantity", (prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter") as TextBox).Text.Trim());
                        cmd2.Parameters.AddWithValue("@fpaNo", fpaTextBox.Text);
                        cmd2.Parameters.AddWithValue("@prodRejId", prodRejId);
                        cmd2.ExecuteNonQuery();

                        sqlCon.Close();
                        LoadProductRejHisValues();
                        lblSuccessMessage.Text = "Record Added";
                        lblErrorMessage.Text = "";
                        FpaRejectionQtyTextBox.Text = "";
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
                    FpaRejectionQtyTextBox.Text = "";
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

        protected void Cancel_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(settings.ToString());
            con.Open();
            // Delete rejection history from master
            string query = "DELETE FROM production_rejection_history_master;";
            SqlCommand sqlCmd = new SqlCommand(query, con);
            sqlCmd.ExecuteNonQuery();
            con.Close();

            if (Application["editFlag"] is true)
                Application["editFlag"] = null;

            Application["workerName"] = null;
            Application["workerShift"] = null;
            Application["newWip"] = null;
            Application["partName"] = null;
            Response.Redirect("~/FPAList.aspx");
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
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).BackColor = Color.WhiteSmoke;
                }
                else
                {
                    ((ImageButton)row.FindControl("AddImgBtn")).Visible = true;
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).ReadOnly = false;
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).Text = "";
                    ((TextBox)row.FindControl("txtRejQuantityFooter")).BackColor = Color.White;
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
                    if (productionTypeDropDownList.SelectedItem.Text == "INHOUSE")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["totalQtyFpa"] + "')", true);
                        checkWipActualQty();
                        Application["okQty"] = true;
                        errorLbl.Text = "";
                        FpaRejectionQtyTextBox.Text = "";
                        efficiencyTextBox.Text = "";

                        /*actualQtyTextBox.Text = "";
                        FpaRejectionQtyTextBox.Text = "";
                        efficiencyTextBox.Text = "";
                        Application["okQty"] = false;*/
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
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void checkWipActualQty()
        {
            try
            {
                Application["newWip"] = string.Empty;
                Application["prevQty"] = string.Empty;
                Application["totalQty"] = string.Empty;
                Application["totalQtyFpa"] = string.Empty;
                Application["newTotalQty"] = false;
                Application["fpaStatus"] = false;
                actualQtyTextBox.ReadOnly = false;

                SqlConnection sqlCon = new SqlConnection(settings.ToString());
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT act_qty FROM production WHERE date_dpr = CONVERT(DATETIME, @DateDpr , 105) AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "'", sqlCon))
                {
                    cmd.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["totalQty"] = reader["act_qty"].ToString();
                    }
                    reader.Close();
                }

                if (Application["editFlag"] is true)
                {
                    // AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND date_dpr = CONVERT(DATETIME, @DateDpr , 105) AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type = '" + operationTypeList.SelectedItem.Text + "'
                    using (SqlCommand sqlCmd = new SqlCommand("SELECT wip_qty,prev_qty FROM fpa_wip_check WHERE fpa_no = '" + fpaTextBox.Text + "'", sqlCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["newWip"] = reader["wip_qty"].ToString();
                            Application["prevQty"] = reader["prev_qty"];
                        }
                        reader.Close();
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["newWip"].ToString() + "')", true);
                }
                else
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SELECT wip_qty,prev_qty FROM fpa_wip_check WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND date_dpr = CONVERT(DATETIME, @DateDpr , 105) AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeList.SelectedItem.Text + "'", sqlCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@DateDpr", dateDropDownList.SelectedItem.Text);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["newWip"] = reader["wip_qty"].ToString();
                            Application["prevQty"] = reader["prev_qty"];
                        }
                        reader.Close();
                    }
                }
                sqlCon.Close();
                
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["prevQty"] + "')", true);

                if (Application["newWip"].ToString() != "")
                {
                    //SET WIP AS NEW TOTAL QTY
                    if (Application["editFlag"] is true)
                    {
                        double currWip = double.Parse(Application["prevQty"].ToString()) - double.Parse(actualQtyTextBox.Text.ToString());
                        if (currWip >= 0)
                        {
                            Application["diffWip"] = currWip - double.Parse(Application["newWip"].ToString());
                            Application["newWip"] = currWip;
                            Application["newTotalQty"] = true;
                            Application["prevQty"] = int.Parse(actualQtyTextBox.Text);
                            Application["totalQtyFpa"] = int.Parse(actualQtyTextBox.Text) + currWip;
                        }
                        else
                        {
                            lblErrorMessage.Text = "Quantity exceeds WIP quantity";
                            FpaRejectionQtyTextBox.Text = "";
                            actualQtyTextBox.Text = "";
                            efficiencyTextBox.Text = "";
                            Application["okQty"] = false;
                        }
                    }
                    else
                    {

                        if (int.Parse(actualQtyTextBox.Text) <= int.Parse(Application["newWip"].ToString()))
                        {
                            lblErrorMessage.Text = "";
                            Application["newTotalQty"] = true;
                            Application["prevQty"] = int.Parse(Application["newWip"].ToString());
                            Application["totalQtyFpa"] = int.Parse(Application["newWip"].ToString());
                        }
                        else
                        {
                            lblErrorMessage.Text = "Quantity exceeds WIP/Total quantity";
                            FpaRejectionQtyTextBox.Text = "";
                            actualQtyTextBox.Text = "";
                            efficiencyTextBox.Text = "";
                            Application["okQty"] = false;
                        }
                    }
                }
                else
                {
                    Application["newTotalQty"] = false;
                    Application["prevQty"] = int.Parse(Application["totalQty"].ToString());
                    Application["totalQtyFpa"] = int.Parse(Application["totalQty"].ToString());
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void FpaRejectionQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Application["otherWipQty"] = string.Empty;
                SqlConnection sqlCon = new SqlConnection(settings.ToString());
                sqlCon.Open();
                int targetQty = int.Parse(noOfPartsTextBox.Text);
                int totalTime = int.Parse(timeTextBox.Text);
                int okQty = int.Parse(actualQtyTextBox.Text);
                int fpaRejectedQty = int.Parse(FpaRejectionQtyTextBox.Text);
                int prodRejectionQty = 0;
                actualQtyTextBox.ReadOnly = false;

                foreach (GridViewRow row in prodRejHisGrid.Rows)
                {
                    Label Lblbasic = (Label)row.FindControl("rejectionQuantityLbl");
                    if (Lblbasic.Text != "")
                    {
                        prodRejectionQty += int.Parse(Lblbasic.Text);
                    }
                }

                if (productionTypeDropDownList.SelectedItem.Text == "INHOUSE")
                {
                    if (int.Parse(Application["totalQtyFpa"].ToString()) != 0 && okQty != 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["totalQtyFpa"].ToString() + "')", true);

                        if ((okQty + fpaRejectedQty + prodRejectionQty) <= int.Parse(Application["totalQtyFpa"].ToString()))
                        {
                            lblErrorMessage.Text = "";

                            Application["wipQty"] = (int.Parse(Application["totalQtyFpa"].ToString()) - (okQty + (prodRejectionQty + fpaRejectedQty))).ToString();

                            if (Application["wipQty"].ToString() != "")
                            {
                                if (int.Parse(Application["totalQtyFpa"].ToString()) != 0 && okQty != 0 && targetQty != 0)
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
                                                using (SqlCommand sqlCmd2 = new SqlCommand("SELECT wip_qty FROM fpa_wip_check WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr=CONVERT(DATETIME, @DateDpr , 105) AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type = '" + ((DataRow)data)["type"].ToString() + "'", sqlCon))
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
                                                    using (SqlCommand sqlCmd3 = new SqlCommand("SELECT wip_qty FROM fpa_wip_check WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr= CONVERT(DATETIME, @DateDpr , 105) AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type != '" + ((DataRow)data)["type"].ToString() + "'", sqlCon))
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
                            lblErrorMessage.Text = "Quantity exceeds WIP quantity";
                            FpaRejectionQtyTextBox.Text = "";
                            actualQtyTextBox.Text = "";

                            string query = "DELETE FROM production_rejection_history_master;";
                            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (productionTypeDropDownList.SelectedItem.Text == "OUTSOURCED")
                {
                    lblErrorMessage.Text = "";
                    efficiencyTextBox.Text = Math.Floor((((okQty + prodRejectionQty) - (fpaRejectedQty)) / (targetQty * totalTime * 0.9) * 100)).ToString();
                }
                else
                {
                    FpaRejectionQtyTextBox.Text = "";
                    lblErrorMessage.Text = "Please select production type";
                }
                sqlCon.Close();
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
                if (partNameDropDownList.SelectedItem.Text == "" || FpaRejectionQtyTextBox.Text == "" || efficiencyTextBox.Text == "")
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

                    if (Application["newTotalQty"] is true && Application["okQty"] is true)
                    {
                        if (Application["fpaStatus"] is true)
                        {
                            SqlCommand cmd1 = new SqlCommand("UPDATE production SET fpa_status='CLOSED',  post_opr_req='CLOSED' WHERE date_dpr = @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                            cmd1.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd1.ExecuteNonQuery();

                            /*SqlCommand cmd2 = new SqlCommand("UPDATE production set post_opr_req='CLOSED' WHERE date_dpr =  @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                            cmd2.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd2.ExecuteNonQuery();*/
                        }
                        else if(Application["fpaStatus"] is false)
                        {
                            SqlCommand cmdFpa = new SqlCommand("UPDATE production SET fpa_status='OPEN',  post_opr_req='OPEN' WHERE date_dpr = @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                            cmdFpa.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmdFpa.ExecuteNonQuery();
                        }

                        if (Application["editFlag"] is true)
                        {
                            int wipQty, prevQty;

                            SqlCommand selCmd = new SqlCommand("SELECT * FROM fpa_wip_check WHERE operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "' AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND date_dpr = @dateDpr AND prev_qty < '" + int.Parse(Application["totalQtyFpa"].ToString()) + "' AND operation_type = '"+operationTypeList.SelectedItem.Text+"'", con);
                            selCmd.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            SqlDataAdapter sda = new SqlDataAdapter(selCmd);
                            DataSet myDataSet = new DataSet();
                            sda.Fill(myDataSet);
                            ArrayList arr = new ArrayList();
                            
                            foreach (DataRow dtRow in myDataSet.Tables[0].Rows)
                            {
                                arr.Add(dtRow);
                            }

                            foreach (object data in arr)
                            {
                                wipQty = int.Parse(((DataRow)data)["wip_qty"].ToString()) + int.Parse(Application["diffWip"].ToString());
                                if(wipQty > 0)
                                {
                                    Application["status"] = "success";
                                }
                                else
                                {
                                    Application["status"] = "failure";
                                    break;
                                }
                            }

                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + arr.Count + "')", true);

                            if (Application["status"].ToString() == "success")
                            {
                                SqlCommand fastUpdate = new SqlCommand("UPDATE fpa_wip_check SET exp_qty = '" + actualQtyTextBox.Text + "', wip_qty = '" + Application["newWip"] + "' WHERE fpa_no = '" + fpaTextBox.Text + "'", con);
                                fastUpdate.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                                fastUpdate.ExecuteNonQuery();

                                foreach (object data in arr)
                                {
                                    wipQty = 0;
                                    prevQty = 0;
                                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ((DataRow)data)["fpa_no"].ToString() + "')", true);
                                    wipQty = int.Parse(((DataRow)data)["wip_qty"].ToString()) + int.Parse(Application["diffWip"].ToString());
                                    prevQty = int.Parse(((DataRow)data)["exp_qty"].ToString()) + wipQty;
                                    SqlCommand updateWipCmd = new SqlCommand("UPDATE fpa_wip_check SET wip_qty = '" + wipQty + "', prev_qty = '" + prevQty + "' WHERE fpa_no = '" + ((DataRow)data)["fpa_no"].ToString() + "'", con);
                                    updateWipCmd.ExecuteNonQuery();
                                }

                                SqlCommand cmd3 = new SqlCommand("UPDATE fpa SET operator_name = '" + operatorNameDropDownList.SelectedItem.Text + "', worker_shift = '" + workerShiftDetails.SelectedItem.Text + "', worker_date = @workerDate, worker_name = '" + workerNameDropDownList.SelectedItem.Text + "', production_type = '" + productionTypeDropDownList.SelectedItem.Text + "', part_name = '" + partNameDropDownList.SelectedItem.Text + "', date_dpr = @dateDpr, shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "', operation_type = '" + operationTypeList.SelectedItem.Text + "', total_qty = '" + Application["totalQty"].ToString() + "', no_of_parts = '" + noOfPartsTextBox.Text + "', total_time = '" + timeTextBox.Text + "', exp_qty = '" + actualQtyTextBox.Text + "', rej_qty = '" + FpaRejectionQtyTextBox.Text + "', efficiency = '" + efficiencyTextBox.Text + "' WHERE fpa_no = '" + fpaTextBox.Text + "'", con);
                                cmd3.Parameters.AddWithValue("@workerDate", DateTime.Parse(dateSelectionTextBox.Text));
                                cmd3.Parameters.AddWithValue("@dateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                                cmd3.ExecuteNonQuery();

                                /*SqlCommand cmd4 = new SqlCommand("UPDATE fpa_operation SET wip_qty='" + Application["wipQty"] + "' WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr =  @DateDpr AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeList.SelectedItem.Text + "'", con);
                                cmd4.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                                cmd4.ExecuteNonQuery();*/

                                lblErrorMessage.Text = "";
                                Application["okQty"] = false;
                                Application["newTotalQty"] = string.Empty;
                                Application["wipQty"] = string.Empty;
                                Application["prevQty"] = string.Empty;
                                Application["editFlag"] = null;
                                Application["diffWip"] = null;
                                Application["totalQty"] = string.Empty;
                                Application["status"] = null;
                                Application["newWip"] = null;
                                con.Close();
                                Response.Redirect("~/FPAList.aspx");
                            }
                            else
                            {
                                lblErrorMessage.Text = "Quantity exceeds WIP quantity";
                            }
                        }
                        else
                        {
                            SqlCommand cmd5 = new SqlCommand("INSERT INTO fpa(fpa_no,operator_name,worker_shift,worker_date,worker_name,production_type,part_name,date_dpr,shift_details,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,efficiency)VALUES('" + fpaTextBox.Text + "','" + operatorNameDropDownList.SelectedItem.Text + "','" + workerShiftDetails.SelectedItem.Text + "',  @WorkerDate,  '" + workerNameDropDownList.SelectedItem.Text + "','" + productionTypeDropDownList.SelectedItem.Text + "','" + partNameDropDownList.SelectedItem.Text + "',@DateDpr, '" + shiftDetailsDropDownList.SelectedItem.Text + "', '" + operationTypeList.SelectedItem.Text + "','" + Application["totalQty"].ToString() + "','" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "','" + actualQtyTextBox.Text + "','" + FpaRejectionQtyTextBox.Text + "','" + efficiencyTextBox.Text + "')", con);
                            cmd5.Parameters.AddWithValue("@WorkerDate", DateTime.Parse(dateSelectionTextBox.Text));
                            cmd5.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd5.ExecuteNonQuery();

                            SqlCommand cmd6 = new SqlCommand("INSERT INTO fpa_wip_check(fpa_no,exp_qty,wip_qty,prev_qty,part_name,operator_name,shift_details,date_dpr,operation_type)VALUES('" + fpaTextBox.Text + "','" + int.Parse(actualQtyTextBox.Text) + "','" + Application["wipQty"].ToString() + "','" + int.Parse(Application["prevQty"].ToString()) + "','"+partNameDropDownList.SelectedItem.Text+"','" + operatorNameDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "',@DateDpr,'" + operationTypeList.SelectedItem.Text + "')", con); ;
                            cmd6.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd6.ExecuteNonQuery();

                            /*SqlCommand cmd7 = new SqlCommand("UPDATE fpa_operation SET wip_qty='" + Application["wipQty"] + "' WHERE operator_name='" + operatorNameDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr =  @DateDpr AND shift_details = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operation_type='" + operationTypeList.SelectedItem.Text + "'", con);
                            cmd7.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                            cmd7.ExecuteNonQuery();*/

                            Application["okQty"] = false;
                            Application["newTotalQty"] = string.Empty;
                            Application["wipQty"] = string.Empty;
                            Application["prevQty"] = string.Empty;
                            Application["editFlag"] = null;
                            Application["totalQty"] = string.Empty;
                            con.Close();
                            Response.Redirect("~/FPAList.aspx");
                        }
                    }
                    else if (Application["newTotalQty"] is false && Application["okQty"] is true)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO fpa(fpa_no,operator_name,worker_shift,worker_date,worker_name,production_type, part_name,date_dpr,shift_details,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,efficiency)VALUES('" + fpaTextBox.Text + "','" + operatorNameDropDownList.SelectedItem.Text + "','" + workerShiftDetails.SelectedItem.Text + "',@WorkerDate,'" + workerNameDropDownList.SelectedItem.Text + "','" + productionTypeDropDownList.SelectedItem.Text + "','" + partNameDropDownList.SelectedItem.Text + "',@DateDpr,'" + shiftDetailsDropDownList.SelectedItem.Text + "', '" + operationTypeList.SelectedItem.Text + "', '" + Application["totalQty"].ToString() + "', '" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "', '" + actualQtyTextBox.Text + "', '" + FpaRejectionQtyTextBox.Text + "', '" + efficiencyTextBox.Text + "')", con);
                        cmd.Parameters.AddWithValue("@WorkerDate", DateTime.Parse(dateSelectionTextBox.Text));
                        cmd.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd.ExecuteNonQuery();

                        /*SqlCommand cmd2 = new SqlCommand("INSERT INTO fpa_operation(fpa_no,operator_name,worker_name,part_name,date_dpr,shift_details,operation_type,exp_qty,wip_qty,efficiency)VALUES('" + fpaTextBox.Text + "','" + operatorNameDropDownList.SelectedItem.Text + "', '" + workerNameDropDownList.SelectedItem.Text + "', '" + partNameDropDownList.SelectedItem.Text + "', @DateDpr, '" + shiftDetailsDropDownList.SelectedItem.Text + "', '" + operationTypeList.SelectedItem.Text + "','" + actualQtyTextBox.Text + "', '" + Application["wipQty"].ToString() + "', '" + efficiencyTextBox.Text + "')", con);
                        cmd2.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd2.ExecuteNonQuery();*/

                        SqlCommand cmd3 = new SqlCommand("UPDATE production set fpa_status='OPEN' WHERE date_dpr = @DateDpr AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND operator_name='" + operatorNameDropDownList.SelectedItem.Text + "'", con);
                        cmd3.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd3.ExecuteNonQuery();

                        SqlCommand cmd4 = new SqlCommand("INSERT INTO fpa_wip_check(fpa_no,exp_qty,wip_qty,prev_qty,part_name,operator_name,shift_details,date_dpr,operation_type)VALUES('" + fpaTextBox.Text + "','" + int.Parse(actualQtyTextBox.Text) + "','" + Application["wipQty"].ToString() + "','" + int.Parse(Application["prevQty"].ToString()) + "','"+partNameDropDownList.SelectedItem.Text+"','" + operatorNameDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "',@DateDpr,'" + operationTypeList.SelectedItem.Text + "')", con); ;
                        cmd4.Parameters.AddWithValue("@DateDpr", DateTime.Parse(dateDropDownList.SelectedItem.Text));
                        cmd4.ExecuteNonQuery();

                        Application["okQty"] = false;
                        Application["newTotalQty"] = string.Empty;
                        Application["wipQty"] = string.Empty;
                        Application["prevQty"] = string.Empty;
                        Application["editFlag"] = null;
                        Application["totalQty"] = string.Empty;

                        con.Close();
                        Response.Redirect("~/FPAList.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void productionTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection1 = new SqlConnection(settings.ToString());
            connection1.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM parts_master", connection1))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                partNameDropDownList.DataSource = reader;
                partNameDropDownList.DataBind();
                reader.Close();
                partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
            }
            connection1.Close();
            shiftDetailsDropDownList.Items.Clear();
            dateDropDownList.Items.Clear();
            operatorNameDropDownList.Items.Clear();
        }

        protected void timeCustom_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (int.Parse(args.Value) > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }
}