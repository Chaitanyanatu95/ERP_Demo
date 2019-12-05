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
    public partial class displayPartsWorker : System.Web.UI.Page
    {
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
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
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
            //string temp;
            //partsGridView.EditIndex = e.NewEditIndex;
            //temp = partsGridView.Rows[0].Cells[0].Text;
            //PopulateGridview();
        }

        protected void parts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            partsGridView.EditIndex = -1;
            PopulateGridview();
        }

        /*protected void parts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM parts_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(partsGridView.DataKeys[e.RowIndex].Value.ToString()));
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
        }*/

        /*protected void partsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newPartsMaster.aspx");
        }*/
    }
}