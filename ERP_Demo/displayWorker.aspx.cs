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
    public partial class displayWorker : System.Web.UI.Page
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
                /*string stuffQuery = "SELECT WM.id, WM.worker_id, WM.worker_name, WM.user_id, WM.user_password," +
                    " STUFF((SELECT ', ' + WR.rights FROM worker_rights WR WHERE WR.worker_id = WM.worker_id " +
                    "FOR XML PATH('')), 1, 1, '') [rights] FROM worker_master WM GROUP BY WM.id, WM.worker_id," +
                    " WM.worker_name, WM.user_id, WM.user_password ORDER BY 1;";*/
                string stuffQuery = "SELECT * FROM worker_master;";

                SqlDataAdapter sqlDa = new SqlDataAdapter(stuffQuery, sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                workerGridView.DataSource = dtbl;
                workerGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                workerGridView.DataSource = dtbl;
                workerGridView.DataBind();
                workerGridView.Rows[0].Cells.Clear();
                workerGridView.Rows[0].Cells.Add(new TableCell());
                workerGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                workerGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                workerGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void workerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            workerGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void workerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            workerGridView.EditIndex = -1;
            PopulateGridview();
        }

        /*protected void workerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "UPDATE worker_master SET worker_name=@worker_name,worker_id=@worker_id,user_id=@user_id,user_password=@user_password,rights=@rights WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@worker_name", (workerGridView.Rows[e.RowIndex].FindControl("txtWName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@worker_id", (workerGridView.Rows[e.RowIndex].FindControl("txtWId") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@user_id", (workerGridView.Rows[e.RowIndex].FindControl("txtUId") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@user_password", (workerGridView.Rows[e.RowIndex].FindControl("txtUPassword") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@rights", (workerGridView.Rows[e.RowIndex].FindControl("txtRights") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(workerGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    workerGridView.EditIndex = -1;
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }*/

        protected void workerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();

                    string query = "DELETE FROM worker_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(workerGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void workerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newWorkerMaster.aspx");
        }

        protected void workerGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Application["workerId"] = commandArgs[0];
                bool editFlag = true;
                Application["editFlag"] = editFlag;
                Response.Redirect("~/newWorkerMaster.aspx/");
            }
        }
    }
}