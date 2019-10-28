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
    public partial class Fpa : System.Web.UI.Page
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
                productionTagLabel.Visible = false;
                productionTagDropDownList.Visible = false;
                operatorNameTextBox.Text = Session["username"].ToString();
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM production", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        partNameDropDownList.DataSource = reader;
                        partNameDropDownList.DataBind();
                        reader.Close();
                        partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT type FROM post_operation_details", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        operationTypeList.DataSource = reader;
                        operationTypeList.DataBind();
                        reader.Close();
                        operationTypeList.Items.Insert(0, new ListItem("Select Operation", ""));
                    }
                    con.Close();
                }
            }
        }

        protected void partNameChanged(object sender, EventArgs e)
        {
            if (partNameDropDownList.SelectedItem.Text == "Select Part Name")
            {
                productionTagLabel.Visible = false;
                productionTagDropDownList.Visible = false;
            }
            else
            {
                productionTagLabel.Visible = true;
                productionTagDropDownList.Visible = true;
                LoadProductTagDetailsValues();
                LoadShiftDetails();
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
        protected void workerChanged(object sender, EventArgs e)
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
        }

        protected void dateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateDropDownList.SelectedItem.Text != "Select Date")
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT act_qty FROM production WHERE shift_details='" + shiftDetailsDropDownList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "' AND date_dpr='" + dateDropDownList.SelectedItem.Text.ToString() + "'", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            totalQtyTextBox.Text = reader["act_qty"].ToString();
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

        protected void operationTypeListChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True");
                sqlCon.Open();
                using (SqlCommand sqlnewCmd = new SqlCommand("SELECT target_quantity FROM post_operation_details WHERE type='" + operationTypeList.SelectedItem.Text + "' AND part_name='" + partNameDropDownList.SelectedItem.Text + "'", sqlCon))
                {
                    SqlDataReader reader = sqlnewCmd.ExecuteReader();
                    while(reader.Read())
                    {
                        noOfPartsTextBox.Text = reader["target_quantity"].ToString();
                    }
                    //System.Diagnostics.Debug.WriteLine(noOfPartsTextBox.Text.ToString());
                    reader.Close();
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void WIPchanged(object sender,EventArgs e)
        {
            try
            {
                if (wipQtyTextBox.Text != "")
                {
                    int totalQty = int.Parse(totalQtyTextBox.Text);
                    int okQty = int.Parse(actualQtyTextBox.Text);
                    int fpaRejectedQty = int.Parse(FpaRejectionQtyTextBox.Text);
                    int prodRejectionQty = int.Parse(prodRejectionQtyTextBox.Text);
                    int targetQty = int.Parse(noOfPartsTextBox.Text);
                    int totalTime = int.Parse(timeTextBox.Text);

                    if (totalQty != 0 && okQty != 0 && targetQty != 0)
                    {
                        //System.Diagnostics.Debug.WriteLine(targetQty * totalTime * 0.9);
                        efficiencyTextBox.Text = Math.Floor((((okQty + prodRejectionQty) - (fpaRejectedQty)) / (targetQty * totalTime * 0.9)*100)).ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void prodTagDetailsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        string query = "INSERT INTO production_tag_details_master (emp_no,date,shift,qty,part_name) VALUES(@empNo,@date,@shift,@qty,'" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@empNo", (prodTagDetailsGrid.FooterRow.FindControl("txtEmpNoFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@date", (prodTagDetailsGrid.FooterRow.FindControl("txtDateFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@shift", (prodTagDetailsGrid.FooterRow.FindControl("txtShiftFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@qty", (prodTagDetailsGrid.FooterRow.FindControl("txtQtyFooter") as TextBox).Text.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        if (Application["editFlag"] is true)
                            BindControlsOnPageLoad();
                        else
                            LoadProductTagDetailsValues();
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

        protected void prodTagDetailsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM production_tag_details_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(prodTagDetailsGrid.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    if (Application["editFlag"] is true)
                        BindControlsOnPageLoad();
                    else
                        LoadProductTagDetailsValues();
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
            prodTagDetailsGrid.DataSource = (DataTable)Application["ProductionTagDetails"];
            prodTagDetailsGrid.DataBind();
        }

        protected void FpaRejectionQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int totalQty = int.Parse(totalQtyTextBox.Text);
                int okQty = int.Parse(actualQtyTextBox.Text);
                int fpaRejectedQty = int.Parse(FpaRejectionQtyTextBox.Text);
                int prodRejectionQty = int.Parse(prodRejectionQtyTextBox.Text);

                if (totalQty != 0 && okQty != 0)
                {
                    wipQtyTextBox.Text = (totalQty - (okQty + (prodRejectionQty + fpaRejectedQty))).ToString();
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadProductTagDetailsValues()
        {
            /************* PRODUCTION TAG DETAILS *****************/
            if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
            {
                DataTable dtProductionTag = new DataTable();
                dtProductionTag.Columns.AddRange(new DataColumn[4] { new DataColumn("EMP NO"), new DataColumn("DATE"), new DataColumn("SHIFT"), new DataColumn("QTY") });
                Application["ProductionTagDetails"] = dtProductionTag;
                BindProductionTagGrid();

                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_tag_details_master where part_name = '" + partNameDropDownList.SelectedItem.Text.ToString().Trim() + "'", sqlCon);
                    sqlDa.Fill(dtProductionTag);
                }
                if (dtProductionTag.Rows.Count > 0)
                {
                    BindProductionTagGrid();
                }
                else
                {
                    dtProductionTag.Rows.Add(dtProductionTag.NewRow());
                    BindProductionTagGrid();
                    prodTagDetailsGrid.Rows[0].Cells.Clear();
                    prodTagDetailsGrid.Rows[0].Cells.Add(new TableCell());
                    prodTagDetailsGrid.Rows[0].Cells[0].ColumnSpan = dtProductionTag.Columns.Count;
                    prodTagDetailsGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    prodTagDetailsGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (wipQtyTextBox.Text == "" || partNameDropDownList.SelectedItem.Text == "" || operatorNameTextBox.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO fpa(worker_name,part_name,date,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,wip_qty,rej_code,prod_tag_details)VALUES('" + operatorNameTextBox.Text + "','" + partNameDropDownList.SelectedItem.Text + "','" + dateDropDownList.SelectedItem.Text + "','" + operationTypeList.SelectedItem.Text + "','" + totalQtyTextBox.Text + "','" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "','" + actualQtyTextBox.Text + "','" + FpaRejectionQtyTextBox.Text + "','" + wipQtyTextBox.Text + "','" + rejectionCodeList.SelectedItem.Text + "','" + productionTagDropDownList.SelectedItem.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
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
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }
}