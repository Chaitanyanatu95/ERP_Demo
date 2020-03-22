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
    public partial class FPAList : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM fpa ORDER BY worker_date", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    fpaGridView.DataSource = dtbl;
                    fpaGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    fpaGridView.DataSource = dtbl;
                    fpaGridView.DataBind();
                    fpaGridView.Rows[0].Cells.Clear();
                    fpaGridView.Rows[0].Cells.Add(new TableCell());
                    fpaGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    fpaGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    fpaGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void fpaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fpaGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }

        protected void fpaGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ dprFlag.Text.ToString()+"');", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('In');", true);

                lblErrorMessage.Text = "";
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();

                    GridViewRow row = fpaGridView.Rows[e.RowIndex];
                    HiddenField fpaNo = (HiddenField)row.FindControl("fpaNo");

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ assNo.Text.ToString()+"');", true);
                    //int rowIndex = ((sender as Button).NamingContainer as GridViewRow).RowIndex;

                    string query = "DELETE FROM production_rejection_history WHERE fpa_no = @fpaNo";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@fpaNo", fpaNo.Value.ToString());
                    sqlCmd.ExecuteNonQuery();

                    string query2 = "DELETE FROM fpa_operation WHERE fpa_no = @fpaNo";
                    SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
                    sqlCmd2.Parameters.AddWithValue("@fpaNo", fpaNo.Value.ToString());
                    sqlCmd2.ExecuteNonQuery();

                    string query3 = "DELETE FROM fpa WHERE id = @id";
                    SqlCommand sqlCmd3 = new SqlCommand(query3, sqlCon);
                    sqlCmd3.Parameters.AddWithValue("@id", Convert.ToInt32(fpaGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd3.ExecuteNonQuery();

                    sqlCon.Close();
                }
                    PopulateGridview();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblSuccessMessage.Text = "";
            }
        }

        protected void fpaGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //int RowIndex = gvr.RowIndex;

                /*GridViewRow row = productionGridView.Rows[RowIndex];*/
               // Label postFlag = (Label)gvr.FindControl("postStatusFlag");
                //Label fpaFlag = (Label)gvr.FindControl("fpaStatusFlag");
                //if (postFlag.Text != "CLOSED")
                //{
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["id"] = commandArgs[0];
                    Application["fpaNo"] = commandArgs[1];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/FPA.aspx/");
                //}
                //else
               // {
               //    lblErrorMessage.Text = "Cannot edit as operation is finished";
               // }
            }
        }
    }
}