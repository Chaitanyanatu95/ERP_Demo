using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

namespace ERP_Demo.masters
{
    public partial class Parts : System.Web.UI.Page
    {

        string connectionString = @"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True";

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
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM parts_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                PartsGrid.DataSource = dtbl;
                PartsGrid.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                PartsGrid.DataSource = dtbl;
                PartsGrid.DataBind();
                PartsGrid.Rows[0].Cells.Clear();
                PartsGrid.Rows[0].Cells.Add(new TableCell());
                PartsGrid.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                PartsGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                PartsGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void PartsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        FileUpload newFileUpload = (FileUpload)PartsGrid.FooterRow.FindControl("newFileUpload");
                        sqlCon.Open();
                        string path = "~/masters/PartsImages/";
                        if (newFileUpload.HasFile)
                        {
                            path += newFileUpload.FileName;
                            //save image in folder    
                            newFileUpload.SaveAs(Server.MapPath(path));
                        }
                        string query = "INSERT INTO parts_master (part_name,part_weight,part_photo,mold_name,drawing_no,no_of_cavities) VALUES (@part_name,@part_weight,@part_photo,@mold_name,@drawing_no,@no_of_cavities)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@part_name", (PartsGrid.FooterRow.FindControl("txtPartNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@part_weight", (PartsGrid.FooterRow.FindControl("txtPartWeightFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@part_photo",path);
                        sqlCmd.Parameters.AddWithValue("@mold_name", (PartsGrid.FooterRow.FindControl("txtMoldNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@drawing_no", (PartsGrid.FooterRow.FindControl("txtDrawingNoFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@no_of_cavities", (PartsGrid.FooterRow.FindControl("txtNo_CavitiesFooter") as TextBox).Text.Trim());
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

        protected void PartsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            PartsGrid.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void PartsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PartsGrid.EditIndex = -1;
            PopulateGridview();
        }

        protected void PartsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    FileUpload editFileUpload = (FileUpload)PartsGrid.Rows[e.RowIndex].FindControl("editFileUpload");
                    sqlCon.Open();
                    string path = "~/masters/PartsImages/";
                    if (editFileUpload.HasFile)
                    {
                        path += editFileUpload.FileName;
                        //save image in folder    
                        editFileUpload.SaveAs(Server.MapPath(path));
                    }
                    else
                    {
                        // use previous user image if new image is not changed    
                        Image img = (Image)PartsGrid.Rows[e.RowIndex].FindControl("imgPartPhoto");
                        path = img.ImageUrl;
                    }
                    string query = "UPDATE parts_master SET part_name=@part_name , part_weight=@part_weight , mold_name=@mold_name , drawing_no=@drawing_no , no_of_cavities=@no_of_cavities , part_photo='" + path + "' WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@part_name", (PartsGrid.Rows[e.RowIndex].FindControl("txtPartName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@part_weight", (PartsGrid.Rows[e.RowIndex].FindControl("txtPartWeight") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@mold_name", (PartsGrid.Rows[e.RowIndex].FindControl("txtMoldName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@drawing_no", (PartsGrid.Rows[e.RowIndex].FindControl("txtDrawingNo") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@no_of_cavities", (PartsGrid.Rows[e.RowIndex].FindControl("txtNo_Cavities") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(PartsGrid.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PartsGrid.EditIndex = -1;
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

        protected void PartsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM parts_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(PartsGrid.DataKeys[e.RowIndex].Value.ToString()));
                    Image lblDeleteImageName = (Image)PartsGrid.Rows[e.RowIndex].FindControl("PartPhoto");
                    string lblDeleteImagePath = lblDeleteImageName.ImageUrl;
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                    ImageDeleteFromFolder(lblDeleteImagePath);
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

        //Delete Image from Directory
        protected void ImageDeleteFromFolder(string delImagePath)
        {
            string path = Server.MapPath(delImagePath);
            try
            {
                if (File.Exists(path)) //check file exsit or not  
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "error in ImageDeleteFromFolder : " + ex.Message;
            }
        }

        protected void PartsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}