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
    public partial class displayFamily : System.Web.UI.Page
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
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM family_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                familyGridView.DataSource = dtbl;
                familyGridView.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                familyGridView.DataSource = dtbl;
                familyGridView.DataBind();
                familyGridView.Rows[0].Cells.Clear();
                familyGridView.Rows[0].Cells.Add(new TableCell());
                familyGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                familyGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                familyGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void familyGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            familyGridView.EditIndex = e.NewEditIndex;
            //temp = customer.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void familyGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            familyGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void familyGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM family_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(familyGridView.DataKeys[e.RowIndex].Value.ToString()));
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

        protected void familyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newFamilyMaster.aspx");
        }

        protected void familyGridView_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }
}