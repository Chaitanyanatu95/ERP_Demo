using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class displayAssemble : System.Web.UI.Page
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT assembly_no,id,assembly_name FROM (SELECT assembly_name, MAX(id) id, MAX(assembly_no) assembly_no FROM assembly_master GROUP BY assembly_name) A ORDER BY assembly_no;", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    assembleGridView.DataSource = dtbl;
                    assembleGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    assembleGridView.DataSource = dtbl;
                    assembleGridView.DataBind();
                    assembleGridView.Rows[0].Cells.Clear();
                    assembleGridView.Rows[0].Cells.Add(new TableCell());
                    assembleGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    assembleGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    assembleGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblAssemblySuccessMessage.Text = "";
                lblAssemblyErrorMessage.Text = ex.Message;
            }
        }

        protected void assembleGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            assembleGridView.EditIndex = e.NewEditIndex;
            //temp = customer.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void assembleGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            assembleGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void assembleGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();

                    GridViewRow row = assembleGridView.Rows[e.RowIndex];
                    Label assNo = (Label)row.FindControl("assembleNo");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ assNo.Text.ToString()+"');", true);
                    //int rowIndex = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
                    
                    string query2 = "DELETE FROM assembly_operation_saved_details WHERE assembly_id = @assNo";
                    SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
                    sqlCmd2.Parameters.AddWithValue("@assNo", assNo.Text.ToString());
                    sqlCmd2.ExecuteNonQuery();

                    string query = "DELETE FROM assembly_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(assembleGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    
                    sqlCon.Close();

                    PopulateGridview();
                    lblAssemblySuccessMessage.Text = "Selected Record Deleted";
                    lblAssemblyErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblAssemblySuccessMessage.Text = "";
                lblAssemblyErrorMessage.Text = ex.Message;
            }
        }

        protected void assembleButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newAssembleMaster.aspx");
        }

        protected void assembleGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["assemblyId"] = commandArgs[0];
                    Application["assemblyNo"] = commandArgs[1];
                    Application["assemblyName"] = commandArgs[2];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newAssembleMaster.aspx/");
                }
                if (e.CommandName == "viewDetails")
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["viewDetailsId"] = commandArgs[0];
                    Application["assemblyNo"] = commandArgs[1];
                    Response.Redirect("viewDetailsAssembly.aspx");
                }
            }
            catch(Exception ex)
            {
                lblAssemblySuccessMessage.Text = "";
                lblAssemblyErrorMessage.Text = ex.Message;
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchAssemble();
        }

        protected void searchAssemble()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM assembly_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE assembly_name LIKE '%' + @assName+ '%'";
                        cmd.Parameters.AddWithValue("@assName", searchTextBox.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        assembleGridView.DataSource = dt;
                        assembleGridView.DataBind();
                    }
                }
            }
        }
        protected void assembleGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            assembleGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}