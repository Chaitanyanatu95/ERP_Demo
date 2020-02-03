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
    public partial class displayPartsWorker : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
                Application["editFlag"] = false;
            }
        }

        protected void partsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            partsGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM parts_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                partsGridView.DataSource = dtbl;
                partsGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                partsGridView.DataSource = dtbl;
                partsGridView.DataBind();
                partsGridView.Rows[0].Cells.Clear();
                partsGridView.Rows[0].Cells.Add(new TableCell());
                partsGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                partsGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                partsGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void parts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "viewDetails")
            {
                Application["viewDetailsId"] = e.CommandArgument.ToString();
                Response.Redirect("viewDetails.aspx");
            }
        }

        protected void parts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            partsGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void searchPart()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM parts_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE part_name LIKE '%' + @PartName + '%'";
                        cmd.Parameters.AddWithValue("@PartName", searchTextBox.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        partsGridView.DataSource = dt;
                        partsGridView.DataBind();
                    }
                }
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchPart();
        }
    }
}