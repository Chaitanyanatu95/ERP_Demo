using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class FPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControlsOnPageLoad();
            }
        }

        protected void BindControlsOnPageLoad()
        {
            if (Session["username"] is null)
            {
                Response.Redirect("~/Login.aspx/");
            }
            else
            {
                //operatorNameTextBox.Text = Session["username"].ToString();
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    LoadProductRejHisValues();
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
                    con.Close();
                }
            }
        }

        protected void partNameChanged(object sender, EventArgs e)
        {
            if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
            {
                Application["tempPartName"] = string.Empty;
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM production WHERE part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["tempPartName"] = reader["part_name"].ToString();
                    }
                    reader.Close();

                    if(Application["tempPartName"].ToString() != "")
                    {
                        workerNameDropDownList.Items.Clear();
                        dateDropDownList.Items.Clear();
                        noOfPartsTextBox.Text = "";
                        LoadShiftDetails();
                    }
                    else
                    {
                        shiftDetailsDropDownList.Items.Clear();
                        workerNameDropDownList.Items.Clear();
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
            }
            else
            {
                shiftDetailsDropDownList.Items.Clear();
                workerNameDropDownList.Items.Clear();
                operationTypeList.Items.Clear();
                dateDropDownList.Items.Clear();
                noOfPartsTextBox.Text = "";
            }
        }

        protected void LoadShiftDetails()
        {
            if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
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

        protected void shiftChanged(object sender, EventArgs e)
        {
            if (shiftDetailsDropDownList.SelectedItem.Text != "Select Shift")
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT worker_name FROM production WHERE shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "'", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    workerNameDropDownList.DataSource = reader;
                    workerNameDropDownList.DataBind();
                    reader.Close();
                    workerNameDropDownList.Items.Insert(0, new ListItem("Select Worker", ""));
                }
                con.Close();
            }
            else
            {
                workerNameDropDownList.Items.Clear();
                FpaRejectionQtyTextBox.Text = "";
                actualQtyTextBox.Text = "";
                efficiencyTextBox.Text = "";
                timeTextBox.Text = "";
                dateDropDownList.Items.Clear();
            }
        }
        protected void workerChanged(object sender, EventArgs e)
        {
            if (workerNameDropDownList.SelectedItem.Text != "Select Worker")
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT date_dpr FROM production WHERE shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND worker_name ='" + workerNameDropDownList.SelectedItem.Text + "'", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    dateDropDownList.DataSource = reader;
                    dateDropDownList.DataBind();
                    reader.Close();
                    dateDropDownList.Items.Insert(0, new ListItem("Select Date", ""));
                }
                con.Close();
                FpaRejectionQtyTextBox.Text = "";
                actualQtyTextBox.Text = "";
                efficiencyTextBox.Text = "";
                timeTextBox.Text = "";
            }
            else
            {
                workerNameDropDownList.Items.Clear();
                operationTypeList.Items.Clear();
                FpaRejectionQtyTextBox.Text = "";
                actualQtyTextBox.Text = "";
                efficiencyTextBox.Text = "";
                timeTextBox.Text = "";
                dateDropDownList.Items.Clear();
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
                }
                else
                {
                    dateDropDownList.Items.Clear();
                    operationTypeList.Items.Clear();
                    FpaRejectionQtyTextBox.Text = "";
                    actualQtyTextBox.Text = "";
                    efficiencyTextBox.Text = "";
                    timeTextBox.Text = "";
                }
            }
            catch(Exception ex)
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
                    
                    fpaOperation();

                    /*SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True");
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
                    }*/
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
                    actualQtyTextBox.Text = "";
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

        protected void fpaOperation()
        {
            Application["newWip"] = string.Empty;
            string newWip = string.Empty;
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True");
            sqlCon.Open();
            
            using (SqlCommand sqlCmd = new SqlCommand("SELECT wip_qty FROM fpa_operation WHERE operation_type='" + operationTypeList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND worker_name = '" + workerNameDropDownList.SelectedItem.Text + "' AND shift = '" + shiftDetailsDropDownList.SelectedItem.Text + "' AND date = '" + dateDropDownList.SelectedItem.Text + "'", sqlCon))
            {
                SqlDataReader reader2 = sqlCmd.ExecuteReader();
                while (reader2.Read())
                {
                    Application["newWip"] = reader2["wip_qty"].ToString();
                }
                reader2.Close();
            }
            sqlCon.Close();

            newWip = Application["newWip"].ToString();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + newWip + "')", true);

            if (int.Parse(newWip) != 0)
            {
                Application["newTotalQty"] = true;
            }
            else
            {
                Application["newTotalQty"] = false;
            }
        }

        protected void FpaRejectionQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Application["prodRejQty"] = string.Empty;
                Application["prodRejQty"] = 0;

                if (Application["newTotalQty"] is true)
                {
                    Application["totalQtyFpa"] = int.Parse(Application["newWip"].ToString());
                }
                else
                {
                    Application["totalQtyFpa"] = int.Parse(Application["totalQty"].ToString());
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Application["totalQtyFpa"].ToString() + "')", true);

                //int totalQty = int.Parse(Application["totalQty"].ToString());
                int okQty = int.Parse(actualQtyTextBox.Text);
                int fpaRejectedQty = int.Parse(FpaRejectionQtyTextBox.Text);
                int prodRejectionQty = int.Parse(Application["prodRejQty"].ToString());

                foreach (GridViewRow row in prodRejHisGrid.Rows)
                {
                    Label Lblbasic = (Label)row.FindControl("rejectionQuantityLbl");
                    if (Lblbasic.Text != "")
                    {
                        prodRejectionQty += int.Parse(Lblbasic.Text);
                    }
                }

               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + prodRejectionQty.ToString() + "')", true);
                if (Application["totalQtyFpa"].ToString() != "0" && okQty != 0)
                {
                    Application["wipQty"] = (int.Parse(Application["totalQtyFpa"].ToString()) - (okQty + (prodRejectionQty + fpaRejectedQty))).ToString();
                }

                if (Application["wipQty"].ToString() != "")
                {
                    int targetQty = int.Parse(noOfPartsTextBox.Text);
                    int totalTime = int.Parse(timeTextBox.Text);
                    if (Application["totalQtyFpa"].ToString() != "0" && okQty != 0 && targetQty != 0)
                    {
                        //System.Diagnostics.Debug.WriteLine(targetQty * totalTime * 0.9);
                        efficiencyTextBox.Text = Math.Floor((((okQty + prodRejectionQty) - (fpaRejectedQty)) / (targetQty * totalTime * 0.9) * 100)).ToString();
                    }
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
                    //string partNo = Application["partNo"].ToString();
                    string partName = partNameDropDownList.SelectedItem.Text.ToString().Trim();
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "'"+Application["PostOperation"].ToString()+"'", true);
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO production_rejection_history_master (rejection_code,rejection_quantity,part_name) VALUES(@rejectionCode,@rejectionQuantity,'" + partName +"')";
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
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
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

        protected void BindProductionTagGrid()
        {
            prodRejHisGrid.DataSource = (DataTable)Application["ProductionRejectionDetails"];
            prodRejHisGrid.DataBind();
        }

        protected void LoadProductRejHisValues()
        {
            /************* PRODUCTION TAG DETAILS *****************/
            DataTable dtProductionRejHis = new DataTable();
            dtProductionRejHis.Columns.AddRange(new DataColumn[2] { new DataColumn("REJECTION CODE"), new DataColumn("REJECTION QUANTITY") });
            Application["ProductionRejectionDetails"] = dtProductionRejHis;
            BindProductionTagGrid();
            
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
            sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history_master", sqlCon);
            sqlDa.Fill(dtProductionRejHis);
            }
            if (dtProductionRejHis.Rows.Count > 0)
            {
                BindProductionTagGrid();
            }
            else
            {
                dtProductionRejHis.Rows.Add(dtProductionRejHis.NewRow());
                BindProductionTagGrid();
                prodRejHisGrid.Rows[0].Cells.Clear();
                prodRejHisGrid.Rows[0].Cells.Add(new TableCell());
                prodRejHisGrid.Rows[0].Cells[0].ColumnSpan = dtProductionRejHis.Columns.Count;
                //prodRejHisGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                prodRejHisGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO fpa(worker_name,part_name,date,shift,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,efficiency)VALUES('" + operatorNameDropDownList.SelectedItem.Text + "','" + partNameDropDownList.SelectedItem.Text + "','" + dateDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "','" + operationTypeList.SelectedItem.Text + "','" + Application["totalQty"].ToString()+ "','" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "','" + actualQtyTextBox.Text + "','" + FpaRejectionQtyTextBox.Text + "','"+efficiencyTextBox.Text +"')", con);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmmd = new SqlCommand("INSERT INTO fpa_operation(worker_name,part_name,date,shift,operation_type,wip_qty,efficiency)VALUES('" + operatorNameDropDownList.SelectedItem.Text + "','" + partNameDropDownList.SelectedItem.Text + "','" + dateDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "','" + operationTypeList.SelectedItem.Text + "','" + Application["wipQty"].ToString() + "','" + efficiencyTextBox.Text + "')", con);
                    cmmd.ExecuteNonQuery();

                    //Delete rejection history from master
                    string query = "DELETE FROM production_rejection_history_master;";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("~/Default.aspx");
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
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            // Delete rejection history from master
            string query = "DELETE FROM production_rejection_history_master;";
            SqlCommand sqlCmd = new SqlCommand(query, con);
            //sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(prodRejHisGrid.DataKeys[e.RowIndex].Value.ToString()));
            sqlCmd.ExecuteNonQuery();
            Response.Redirect("~/Default.aspx");
        }

        protected void rejectionCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void customValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Application["totalQty"] = string.Empty;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT act_qty FROM production WHERE shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr='" + dateDropDownList.SelectedItem.Text.ToString() + "'", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Application["totalQty"] = reader["act_qty"].ToString();
                }
                reader.Close();
            }
            con.Close();
            if (Application["newTotalQty"] is false)
            {
                if (int.Parse(args.Value) <= int.Parse(Application["totalQty"].ToString()))
                {
                    args.IsValid = true;
                }
            }
            else if (Application["newTotalQty"] is true)
            {
                if (int.Parse(args.Value) <= int.Parse(Application["newWip"].ToString()))
                {
                    args.IsValid = true;
                }
            }
            else
            {
                FpaRejectionQtyTextBox.Text = "";
                actualQtyTextBox.Text = "";
                efficiencyTextBox.Text = "";
                timeTextBox.Text = "";
                args.IsValid = false;
            }
        }
    }
}