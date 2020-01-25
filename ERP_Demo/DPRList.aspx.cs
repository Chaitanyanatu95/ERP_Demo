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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT id,operator_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots,rejection_pcs,rejection_kgs,act_qty,downtime_hrs,down_time_code,efficiency,date_dpr,post_opr_req,fpa_status FROM production", sqlCon);
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
    }
}