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
            if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
            { 
                LoadProductRejHisValues();
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

        protected void FpaRejectionQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int totalQty = int.Parse(totalQtyTextBox.Text);
                int okQty = int.Parse(actualQtyTextBox.Text);
                int fpaRejectedQty = int.Parse(FpaRejectionQtyTextBox.Text);
                int prodRejectionQty = 0;

                foreach (GridViewRow row in prodRejHisGrid.Rows)
                {
                    Label Lblbasic = (Label)row.FindControl("rejectionQuantityLbl");
                    prodRejectionQty += int.Parse(Lblbasic.Text);
                    //System.Diagnostics.Debug.WriteLine(prodRejectionQty);
                }
                System.Diagnostics.Debug.WriteLine(prodRejectionQty);
                if (totalQty != 0 && okQty != 0)
                {
                    wipQtyTextBox.Text = (totalQty - (okQty + (prodRejectionQty + fpaRejectedQty))).ToString();
                }

                if (wipQtyTextBox.Text != "")
                {
                    int targetQty = int.Parse(noOfPartsTextBox.Text);
                    int totalTime = int.Parse(timeTextBox.Text);
                    if (totalQty != 0 && okQty != 0 && targetQty != 0)
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
                        string query = "INSERT INTO production_rejection_history_master (rejection_code,rejection_quantity,part_name) VALUES(@rejectionCode,@rejectionQuantity,'" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@rejectionCode", (prodRejHisGrid.FooterRow.FindControl("rejectionCodeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@rejectionQuantity", (prodRejHisGrid.FooterRow.FindControl("txtRejQuantityFooter") as TextBox).Text.Trim());
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        if (Application["editFlag"] is true)
                            BindControlsOnPageLoad();
                        else
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
                    if (Application["editFlag"] is true)
                        BindControlsOnPageLoad();
                    else
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
            if (partNameDropDownList.SelectedItem.Text != "Select Part Name")
            {
                DataTable dtProductionRejHis = new DataTable();
                dtProductionRejHis.Columns.AddRange(new DataColumn[2] { new DataColumn("REJECTION CODE"), new DataColumn("REJECTION QUANTITY") });
                Application["ProductionRejectionDetails"] = dtProductionRejHis;
                BindProductionTagGrid();

                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_rejection_history_master where part_name = '" + partNameDropDownList.SelectedItem.Text.ToString().Trim() + "'", sqlCon);
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
                    prodRejHisGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    prodRejHisGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO fpa(worker_name,part_name,date,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,wip_qty,efficiency)VALUES('" + operatorNameTextBox.Text + "','" + partNameDropDownList.SelectedItem.Text + "','" + dateDropDownList.SelectedItem.Text + "','" + operationTypeList.SelectedItem.Text + "','" + totalQtyTextBox.Text + "','" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "','" + actualQtyTextBox.Text + "','" + FpaRejectionQtyTextBox.Text + "','" + wipQtyTextBox.Text + "','"+efficiencyTextBox.Text +"')", con);
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