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
    public partial class Masterbatch : System.Web.UI.Page
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
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM masterbatch_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                masterbatch.DataSource = dtbl;
                masterbatch.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                masterbatch.DataSource = dtbl;
                masterbatch.DataBind();
                masterbatch.Rows[0].Cells.Clear();
                masterbatch.Rows[0].Cells.Add(new TableCell());
                masterbatch.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                masterbatch.Rows[0].Cells[0].Text = "No Data Found ..!";
                masterbatch.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void masterbatch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO masterbatch_master (mb_name,mb_grade,mb_mfg,mb_color,mb_color_code) VALUES (@mbname,@mbgrade,@mbmfg,@mbcolor,@mbcolorcode)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@mbname", (masterbatch.FooterRow.FindControl("txtMbnameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@mbgrade", (masterbatch.FooterRow.FindControl("txtMbgradeFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@mbmfg", (masterbatch.FooterRow.FindControl("txtMbmfgFooter") as
                        TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@mbcolor", (masterbatch.FooterRow.FindControl("txtMbcolorFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@mbcolorcode", (masterbatch.FooterRow.FindControl("txtMbcolorcodeFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview();
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void masterbatch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            masterbatch.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void masterbatch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            masterbatch.EditIndex = -1;
            PopulateGridview();
        }

        protected void masterbatch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "UPDATE masterbatch_master SET mb_name=@mb_name,mb_grade=@mb_grade,mb_mfg=@mb_mfg,mb_color=@mb_color,mb_color_code=@mb_color_code WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@mb_name", (masterbatch.Rows[e.RowIndex].FindControl("txtMbname") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@mb_grade", (masterbatch.Rows[e.RowIndex].FindControl("txtMbgrade") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@mb_mfg", (masterbatch.Rows[e.RowIndex].FindControl("txtMbmfg") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@mb_color", (masterbatch.Rows[e.RowIndex].FindControl("txtMbcolor") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@mb_color_code", (masterbatch.Rows[e.RowIndex].FindControl("txtMbcolorcode") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(masterbatch.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    masterbatch.EditIndex = -1;
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
        }

        protected void masterbatch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEWDESKTOP-TBP992L\SQLEXPRESS;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM masterbatch_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(masterbatch.DataKeys[e.RowIndex].Value.ToString()));
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

        }
    }
}