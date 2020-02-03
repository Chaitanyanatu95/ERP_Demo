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
    public partial class displayUnitOfMeasurement : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM unit_of_measurement_master", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    unitOfMeasurementGridView.DataSource = dtbl;
                    unitOfMeasurementGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    unitOfMeasurementGridView.DataSource = dtbl;
                    unitOfMeasurementGridView.DataBind();
                    unitOfMeasurementGridView.Rows[0].Cells.Clear();
                    unitOfMeasurementGridView.Rows[0].Cells.Add(new TableCell());
                    unitOfMeasurementGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    unitOfMeasurementGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    unitOfMeasurementGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void unitOfMeasurementGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            unitOfMeasurementGridView.EditIndex = e.NewEditIndex;
            //temp = customer.Rows[0].Cells[0].Text;
            PopulateGridview();
        }
        protected void unitOfMeasurementGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            unitOfMeasurementGridView.EditIndex = -1;
            PopulateGridview();
        }
        protected void unitOfMeasurementGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM unit_of_measurement_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(unitOfMeasurementGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
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
        protected void unitOfMeasurementButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newUnitMeasurementMaster.aspx");
        }
        protected void unitOfMeasurementGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["unitOfMeasurementId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newUnitMeasurementMaster.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchUOM();
        }

        protected void searchUOM()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM unit_of_measurement_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE unit_of_measurement LIKE '%' + @UOM + '%'";
                        cmd.Parameters.AddWithValue("@UOM", searchTextBox.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        unitOfMeasurementGridView.DataSource = dt;
                        unitOfMeasurementGridView.DataBind();
                    }
                }
            }
        }

        protected void unitOfMeasurementGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            unitOfMeasurementGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}