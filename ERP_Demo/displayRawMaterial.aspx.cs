using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class displayRawMaterial : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
                Application["editFlag"] = false;
            }
        }

        void PopulateGridview()
        {
            try
            {
                DataTable dtbl = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM raw_material_master", sqlCon);
                    sqlDa.Fill(dtbl);
                    sqlCon.Close();
                }
                if (dtbl.Rows.Count > 0)
                {
                    rawMaterialGridView.DataSource = dtbl;
                    rawMaterialGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    rawMaterialGridView.DataSource = dtbl;
                    rawMaterialGridView.DataBind();
                    rawMaterialGridView.Rows[0].Cells.Clear();
                    rawMaterialGridView.Rows[0].Cells.Add(new TableCell());
                    rawMaterialGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    rawMaterialGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    rawMaterialGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void rawMaterialGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            rawMaterialGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void rawMaterialGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            rawMaterialGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void rawMaterialGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "UPDATE raw_material_master SET material_name=@material_name,material_grade=@material_grade,material_color=@material_color,material_make=@material_make WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@material_name", (rawMaterialGridView.Rows[e.RowIndex].FindControl("txtMName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@material_grade", (rawMaterialGridView.Rows[e.RowIndex].FindControl("txtMGrade") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@material_color", (rawMaterialGridView.Rows[e.RowIndex].FindControl("txtMColor") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@material_make", (rawMaterialGridView.Rows[e.RowIndex].FindControl("txtMMake") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(rawMaterialGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    rawMaterialGridView.EditIndex = -1;
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void rawMaterialGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM raw_material_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(rawMaterialGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void rawMaterialButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newRawMaterialMaster.aspx");
        }

        protected void rawMaterialGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["rawMaterialId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newRawMaterialMaster.aspx/");
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