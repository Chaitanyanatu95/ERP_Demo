using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo.masters
{
    public partial class Worker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM workers_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                worker.DataSource = dtbl;
                worker.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                worker.DataSource = dtbl;
                worker.DataBind();
                worker.Rows[0].Cells.Clear();
                worker.Rows[0].Cells.Add(new TableCell());
                worker.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                worker.Rows[0].Cells[0].Text = "No Data Found ..!";
                worker.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }
    }
}