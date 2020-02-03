using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM assemble_master", sqlCon);
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
                lblFamilySuccessMessage.Text = "";
                lblFamilyErrorMessage.Text = ex.Message;
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
                    string query = "DELETE FROM assemble_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(assembleGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblFamilySuccessMessage.Text = "Selected Record Deleted";
                    lblFamilyErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblFamilySuccessMessage.Text = "";
                lblFamilyErrorMessage.Text = ex.Message;
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
                    Application["familyId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newFamilyMaster.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblFamilySuccessMessage.Text = "";
                lblFamilyErrorMessage.Text = ex.Message;
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
                    string sql = "SELECT * FROM assemble_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE child_part LIKE '%' + @ChildPart+ '%'";
                        cmd.Parameters.AddWithValue("@ChildPart", searchTextBox.Text.Trim());
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