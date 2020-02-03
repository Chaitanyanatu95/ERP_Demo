using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace ERP_Demo
{
    public partial class displayWorker : Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
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
            try
            {
                DataTable dtbl = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    /*string stuffQuery = "SELECT WM.id, WM.worker_id, WM.worker_name, WM.user_id, WM.user_password," +
                        " STUFF((SELECT ', ' + WR.rights FROM worker_rights WR WHERE WR.worker_id = WM.worker_id " +
                        "FOR XML PATH('')), 1, 1, '') [rights] FROM worker_master WM GROUP BY WM.id, WM.worker_id," +
                        " WM.worker_name, WM.user_id, WM.user_password ORDER BY 1;";*/
                    string stuffQuery = "SELECT * FROM worker_master order by id OFFSET 1 ROWS;";

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
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
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
        protected void workerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
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
            try
            {
                if (e.CommandName == "Edit")
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["workerId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newWorkerMaster.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchWorker();
        }

        protected void searchWorker()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM worker_master";
                    if (!string.IsNullOrEmpty(searchWorkerTextBox.Text.Trim()))
                    {
                        sql += " WHERE worker_name LIKE '%' + @WorkerName + '%'";
                        cmd.Parameters.AddWithValue("@WorkerName", searchWorkerTextBox.Text.Trim());
                    }
                    sql += " except select * from worker_master where id=1";
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        workerGridView.DataSource = dt;
                        workerGridView.DataBind();
                    }
                }
            }
        }
        protected void workerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            workerGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}