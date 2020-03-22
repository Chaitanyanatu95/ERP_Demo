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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT id,dpr_no,operator_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots,rejection_pcs,rejection_kgs,act_qty,efficiency,date_dpr,post_opr_req,fpa_status FROM production ORDER BY date_dpr", sqlCon);
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
                Label partName = (Label)row.FindControl("partName");
                Label shiftDetails = (Label)row.FindControl("shiftDetails");
                Label dateDpr = (Label)row.FindControl("dateDpr");
                Label postFlag = (Label)row.FindControl("postStatusFlag");
                Label fpaFlag = (Label)row.FindControl("fpaStatusFlag");
                HiddenField dprNo = (HiddenField)row.FindControl("dprNo");
                var postFlagText = postFlag.Text;
                var fpaFlagText = fpaFlag.Text;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + postFlag.Text + "');", true);

                SqlConnection conn = new SqlConnection(settings.ToString());
                conn.Open();
                if ((postFlagText.ToString() != "CLOSED" || fpaFlagText.ToString() != "CLOSED") && fpaFlagText.ToString() != "")
                {
                    lblErrorMessage.Text = "";
                    string query = "DELETE FROM production WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, conn);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(productionGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();

                    string query2 = "DELETE FROM down_time_production WHERE dpr_no = @dprNo";
                    SqlCommand sqlCmd2 = new SqlCommand(query2, conn);
                    sqlCmd2.Parameters.AddWithValue("@dprNo", dprNo.Value);
                    sqlCmd2.ExecuteNonQuery();

                    PopulateGridview();
                }
                else if(postFlagText.ToString() == "CLOSED" || fpaFlagText.ToString() == "CLOSED")
                {
                    lblErrorMessage.Text = "Cannot delete as operation is finished";
                }
                else
                {
                    lblErrorMessage.Text = "Cannot delete the entry as it is used for FPA";
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        protected void productionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                /*GridViewRow row = productionGridView.Rows[RowIndex];*/
                Label postFlag = (Label)gvr.FindControl("postStatusFlag");
                Label fpaFlag = (Label)gvr.FindControl("fpaStatusFlag");
                if (postFlag.Text != "CLOSED")
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["partName"] = commandArgs[0];
                    Application["operatorName"] = commandArgs[1];
                    Application["dateDpr"] = commandArgs[2];
                    Application["shiftDetails"] = commandArgs[3];
                    Application["id"] = commandArgs[4];
                    Application["fpaStatus"] = commandArgs[5];
                    Application["dprNo"] = commandArgs[6];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/DPR.aspx/");
                }
                else
                {
                    lblErrorMessage.Text = "Cannot edit as operation is finished";
                }
            }
        }
    }
}