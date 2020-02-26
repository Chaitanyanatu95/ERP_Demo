﻿using System;
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
    public partial class DPRList : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    PopulateGridview();
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT id,operator_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots,rejection_pcs,rejection_kgs,act_qty,downtime_hrs,down_time_code,efficiency,date_dpr,post_opr_req,fpa_status FROM production ORDER BY date_dpr", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    productionGridView.DataSource = dtbl;
                    productionGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    productionGridView.DataSource = dtbl;
                    productionGridView.DataBind();
                    productionGridView.Rows[0].Cells.Clear();
                    productionGridView.Rows[0].Cells.Add(new TableCell());
                    productionGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    productionGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    productionGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void productionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productionGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }

        protected void productionGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = productionGridView.Rows[e.RowIndex];
                Label dprFlag = (Label)row.FindControl("postStatusFlag");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ dprFlag.Text.ToString()+"');", true);

                if (dprFlag.Text.ToString().Trim() != "OPEN")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('In');", true);

                    lblErrorMessage.Text = "";
                    using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                    {
                        sqlCon.Open();

                        string query = "DELETE FROM production WHERE id = @id";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(productionGridView.DataKeys[e.RowIndex].Value.ToString()));
                        sqlCmd.ExecuteNonQuery();

                        sqlCon.Close();
                    }
                    PopulateGridview();
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Out');", true);
                    lblErrorMessage.Text = "Cannot delete the entry as it is used for FPA";
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblSuccessMessage.Text = "";
            }
        }
    }
}