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
                        operationTypeList.Items.Insert(0, new ListItem("Select Operation", ""));
                    }
                }
            }
        }

        protected void partNameChanged(object sender, EventArgs e)
        {
            if (partNameDropDownList.SelectedItem.Text == "Select Part Name")
            {
                Application["fpaPartName"] = null;
                productionTagLabel.Visible = false;
                productionTagDropDownList.Visible = false;
            }
            else
            {
                Application["fpaPartName"] = partNameDropDownList.SelectedItem.Text;
                productionTagLabel.Visible = true;
                productionTagDropDownList.Visible = true;
                LoadProductTagDetailsValues();
            }
        }
        
        protected void prodTagDetailsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    //string partNo = Application["partNo"].ToString();
                    string partName = Application["fpaPartName"].ToString();
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "'"+Application["PostOperation"].ToString()+"'", true);
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO production_tag_details_master (emp_no,date,shift,qty,part_no,part_name) VALUES(@empNo,@date,@shift,@qty,'" + partName + "')";
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

        protected void rejectionQtyTextBox_TextChanged(object sender, EventArgs e)
        {
            int totalQty = int.Parse(totalQtyTextBox.Text);
            int acceptedQty = int.Parse(acceptedQtyTextBox.Text);
            int rejectedQty = int.Parse(rejectionQtyTextBox.Text);
            if (totalQty != 0 && acceptedQty != 0 && rejectedQty != 0)
            {
                wipQtyTextBox.Text = (totalQty - acceptedQty - rejectedQty).ToString();
            }
        }

        protected void LoadProductTagDetailsValues()
        {
            /************* PRODUCTION TAG DETAILS *****************/

            DataTable dtProductionTag = new DataTable();
            dtProductionTag.Columns.AddRange(new DataColumn[4] { new DataColumn("EMP NO"), new DataColumn("DATE"), new DataColumn("SHIFT"), new DataColumn("QTY") });
            Application["ProductionTagDetails"] = dtProductionTag;
            BindProductionTagGrid();

            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production_tag_details_master where part_name = '" + Application["fpaPartName"].ToString() + "'", sqlCon);
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
                //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                prodTagDetailsGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            Application["fpaPartName"] = null;
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO fpa(worker_name,part_name,date,operation_type,total_qty,no_of_parts,total_time,exp_qty,rej_qty,wip_qty,rej_code,prod_tag_details)VALUES('" + operatorNameTextBox.Text + "','" + partNameDropDownList.SelectedItem.Text + "','" + datePickerTextBox.Value + "','" + operationTypeList.SelectedItem.Text + "','" + totalQtyTextBox.Text + "','" + noOfPartsTextBox.Text + "','" + timeTextBox.Text + "','" + actualQtyTextBox.Text + "','" + rejectionQtyTextBox.Text + "','" + wipQtyTextBox.Text + "','" + rejectionCodeList.SelectedItem.Text + "','" + productionTagDropDownList.SelectedItem.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                    con.Close();
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