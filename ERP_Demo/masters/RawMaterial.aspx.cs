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
    public partial class RawMaterial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {

        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM raw_material_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                rawmaterial.DataSource = dtbl;
                rawmaterial.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                rawmaterial.DataSource = dtbl;
                rawmaterial.DataBind();
                rawmaterial.Rows[0].Cells.Clear();
                rawmaterial.Rows[0].Cells.Add(new TableCell());
                rawmaterial.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                rawmaterial.Rows[0].Cells[0].Text = "No Data Found ..!";
                rawmaterial.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void rawmaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO raw_material_master (material_name,material_size,material_quantity,material_color) VALUES (@material_name,@material_size,@material_quantity,@material_color)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@material_name", (rawmaterial.FooterRow.FindControl("txtMnameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@material_size", (rawmaterial.FooterRow.FindControl("txtSizeFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@material_quantity", (rawmaterial.FooterRow.FindControl("txtQuantityFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@material_color", (rawmaterial.FooterRow.FindControl("txtColorFooter") as TextBox).Text.Trim());
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

        protected void rawmaterial_RowEditing(object sender, GridViewEditEventArgs e)
        {
            rawmaterial.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void rawmaterial_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            rawmaterial.EditIndex = -1;
            PopulateGridview();
        }

        protected void rawmaterial_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "UPDATE raw_material_master SET material_name=@material_name,material_size=@material_size,material_quantity=@material_quantity,material_color=@material_color WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@material_name", (rawmaterial.Rows[e.RowIndex].FindControl("txtMName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@material_size", (rawmaterial.Rows[e.RowIndex].FindControl("txtMSize") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@material_quantity", (rawmaterial.Rows[e.RowIndex].FindControl("txtMQuantity") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@material_color", (rawmaterial.Rows[e.RowIndex].FindControl("txtMColor") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(rawmaterial.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    rawmaterial.EditIndex = -1;
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

        protected void rawmaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM raw_material_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(rawmaterial.DataKeys[e.RowIndex].Value.ToString()));
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