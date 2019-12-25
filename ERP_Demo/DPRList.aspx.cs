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
    public partial class DPRList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    PopulateGridview();
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Hello", true);
                    Application["editFlag"] = false;
                }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM production", sqlCon);
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
    }
}