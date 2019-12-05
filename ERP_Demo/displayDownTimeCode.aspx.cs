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
    public partial class displayDownTimeCode : System.Web.UI.Page
    {
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
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM down_time_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                downtimeGridView.DataSource = dtbl;
                downtimeGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                downtimeGridView.DataSource = dtbl;
                downtimeGridView.DataBind();
                downtimeGridView.Rows[0].Cells.Clear();
                downtimeGridView.Rows[0].Cells.Add(new TableCell());
                downtimeGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                downtimeGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                downtimeGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void downtimeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Application["downTimeCodeId"] = commandArgs[0];
                bool editFlag = true;
                Application["editFlag"] = editFlag;
                Response.Redirect("~/newDownTimeCode.aspx/");
            }
        }

        protected void downtimeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            downtimeGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void downtimeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM down_time_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(downtimeGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblDownTimeCodeSuccessMessage.Text = "Selected Record Deleted";
                    lblDownTimeCodeErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblDownTimeCodeSuccessMessage.Text = "";
                lblDownTimeCodeErrorMessage.Text = ex.Message;
            }
        }

        protected void customerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newDownTimeCode.aspx");
        }
    }
}