using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.AccessControl;

namespace ERP_Demo
{
    public partial class postOperationsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["editFlag"] is true)
                {
                    LoadEditValues();
                }
                else
                {
                    LoadPostOperationValues();
                    LoadPackagingDetailsValues();
                }
            }
        }
        
        protected void LoadPostOperationValues()
        {
            /************** POSTOPERATION ****************/
            DataTable dtPostOperation = new DataTable();
            dtPostOperation.Columns.AddRange(new DataColumn[4] { new DataColumn("TYPE"), new DataColumn("TARGET QUANTITY"), new DataColumn("REMARKS"), new DataColumn("PHOTO") });
            Application["PostOperation"] = dtPostOperation;
            BindPostGrid();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM post_operation_details where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                sqlDa.Fill(dtPostOperation);
            }
            if (dtPostOperation.Rows.Count > 0)
            {
                BindPostGrid();
            }
            else
            {
                dtPostOperation.Rows.Add(dtPostOperation.NewRow());
                BindPostGrid();
                //postOperationDropDownList.Items.Insert(0, new ListItem("Select Post Opr", ""));
                postOperationGrid.Rows[0].Cells.Clear();
                postOperationGrid.Rows[0].Cells.Add(new TableCell());
                postOperationGrid.Rows[0].Cells[0].ColumnSpan = dtPostOperation.Columns.Count;
                //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                postOperationGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void LoadPackagingDetailsValues()
        {
            /************* PACKAGING DETAILS *****************/
            DataTable dtPackaging = new DataTable();
            dtPackaging.Columns.AddRange(new DataColumn[4] { new DataColumn("PACKAGING TYPE"), new DataColumn("SIZE"), new DataColumn("QTY PER PACKAGE"), new DataColumn("PHOTO") });
            Application["PackagingDetails"] = dtPackaging;
            BindPackagingDetailsGrid();

            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM packaging_details_master where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                sqlDa.Fill(dtPackaging);
            }
            if (dtPackaging.Rows.Count > 0)
            {
                BindPackagingDetailsGrid();
            }
            else
            {
                dtPackaging.Rows.Add(dtPackaging.NewRow());
                BindPackagingDetailsGrid();
                //packagingDetailsDropDownList.Items.Insert(0, new ListItem("Select Packaging Type", ""));
                packagingDetailsGrid.Rows[0].Cells.Clear();
                packagingDetailsGrid.Rows[0].Cells.Add(new TableCell());
                packagingDetailsGrid.Rows[0].Cells[0].ColumnSpan = dtPackaging.Columns.Count;
                //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                packagingDetailsGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        
    
        protected void LoadEditValues()
        {
            if (Application["postOperationDetailsReq"].ToString() == "NO")
            {
                postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("YES"));

                postOperationDropDownList.SelectedItem.Text = Application["postOperationDetailsReq"].ToString();

                postOperationDropDownList.Items.Insert(1, new ListItem("YES"));

                //Application["postOperationDetailsReq"] = null;

                LoadPostOperationValues();
            }
            else
            {
                //postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("YES"));
                LoadPostOperationValues();
            }

            if (Application["packagingDetailsReq"].ToString() == "NO")
            {
                packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("YES"));

                packagingDetailsDropDownList.SelectedItem.Text = Application["packagingDetailsReq"].ToString();

                packagingDetailsDropDownList.Items.Insert(1, new ListItem("YES"));

                //Application["packagingDetailsReq"] = null;

                LoadPackagingDetailsValues();
            }
            else
            {
                //packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("YES"));
                LoadPackagingDetailsValues();
            }

            var temp = Server.MapPath("~/UploadedFiles/Parts/");
            if (Directory.Exists(temp))
            {
                DirectoryInfo di = new DirectoryInfo(temp);
                
                FileInfo[] files = di.GetFiles(Application["partNo"]+"_*.*");

                foreach ( FileInfo m in files)
                {
                    //System.Diagnostics.Debug.WriteLine(m.Name.ToString());
                    listUploadedFiles.Text += String.Format("<style> display:block; </style><br/> {0}", m.Name.ToString());
                    imgBtnExtra.ImageUrl = "~/Images/cancel.png";
                    //"<asp:ImageButton ID = 'btnExtraFile' runat = 'server' ImageUrl = '~/Images/cancel.png' Height = '20' Width = '20' OnClick = 'btnExtraFile_Click' />"
                }
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
           if (Application["partNo"].ToString() == "" || Application["partName"].ToString() == "" || Application["custName"].ToString() == "" || Application["custPartNo"].ToString() == "" || Application["prodCategory"].ToString() == "" || Application["moldName"].ToString() == "" || Application["moldMfgYear"].ToString() == "" || Application["moldLife"].ToString() == "" || Application["noOfCavities"].ToString() == "" || Application["unit"].ToString() == "" || Application["partWeight"].ToString() == "" || Application["shotWeight"].ToString() == "" || Application["cycleTime"].ToString() == "" || Application["jigReq"].ToString() == "" || Application["moldProductionCycle"].ToString() == "" || Application["rawMaterial"].ToString() == "")
           {
               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
           }
           else
           {
                Application["postOperationDetailsReq"] = postOperationDropDownList.SelectedItem.Text;
                Application["packagingDetailsReq"] = packagingDetailsDropDownList.SelectedItem.Text;
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
               con.Open();
                if (Application["editFlag"] is true)
                {
                    string query = "UPDATE parts_master SET part_name='"+Application["partName"]+"', customer_name='"+Application["custName"]+"', customer_part_no = '"+Application["custPartNo"]+"',product_category='"+Application["prodCategory"]+"',mold_name='"+Application["moldName"]+"',mold_mfg_year='"+Application["moldMfgYear"]+"',mold_life='"+Application["moldLife"]+"',no_of_cavities='"+Application["noOfCavities"]+"',unit_of_measurement = '"+Application["unit"]+"',part_weight = '"+Application["partWeight"]+"',shot_weight='"+Application["shotWeight"]+"',cycle_time='"+Application["cycleTime"]+"'," +
                        "jig_fixture_req='"+Application["jigReq"]+ "',production_in_pcs = '"+Application["moldProductionCycle"] + "',part_photo='"+Application["partPhoto"]+"',mold_spec_sheet='"+Application["moldSpec"]+"',raw_material='"+Application["rawMaterial"] +"',rm_grade='"+Application["rmGrade"] +"',rm_make='"+Application["rmMake"]+"',rm_color='"+Application["rmColor"]+"',masterbatch='"+Application["masterbatch"] +"',alt_raw_material='"+Application["altRawMaterial"] +"',mb_name='"+Application["mbName"]+"',mb_grade='"+Application["mbGrade"]+"',mb_mfg='"+Application["mbMfg"]+"',mb_color='"+Application["mbColor"]+"',mb_color_code='"+Application["mbColorCode"]+"',alt_rm_name='"+Application["altRMName"] +"',alt_rm_grade='"+Application["altRmGrade"]+ "'" +
                        ",alt_rm_make='"+Application["altRmMake"]+"',alt_rm_color='"+Application["altRmColor"]+"',alt_masterbatch='"+Application["altMasterbatch"] +"',alt_mb_name='"+Application["altMasterbatchName"] +"',alt_mb_grade='"+Application["altMasterbatchGrade"] +"',alt_mb_mfg='"+Application["altMasterbatchMfg"] +"',alt_mb_color='"+Application["altMasterbatchColor"] +"',alt_mb_color_code='"+Application["altMasterbatchColorCode"]+ "',post_operation_required ='" + postOperationDropDownList.SelectedItem.Text + "' ,packaging_details_required = '" + packagingDetailsDropDownList.SelectedItem.Text + "' WHERE part_no = '"+Application["partNo"]+"'";
                    Application["query"] = query;
                }
                else
                {
                    string query = "INSERT INTO parts_master(part_no,part_name,customer_name,customer_part_no,product_category,mold_name,mold_mfg_year,mold_life,no_of_cavities,unit_of_measurement,part_weight,shot_weight,cycle_time,jig_fixture_req,production_in_pcs,part_photo,mold_spec_sheet,raw_material,rm_grade,rm_make,rm_color,masterbatch,alt_raw_material,mb_name,mb_grade,mb_mfg,mb_color,mb_color_code,alt_rm_name,alt_rm_grade," +
                                                "alt_rm_make,alt_rm_color,alt_masterbatch,alt_mb_name,alt_mb_grade,alt_mb_mfg,alt_mb_color,alt_mb_color_code,post_operation_required,packaging_details_required)VALUES('" + Application["partNo"] + "','" + Application["partName"] + "','" + Application["custName"] + "','" + Application["custPartNo"] + "','" + Application["prodCategory"] + "','" + Application["moldName"] + "','" + Application["moldMfgYear"] + "','" + Application["moldLife"] + "','" + Application["noOfCavities"] + "','" + Application["unit"] + "','" + Application["partWeight"] + "','" + Application["shotWeight"] + "','" + Application["cycleTime"] + "','" + Application["jigReq"] + "','" + Application["moldProductionCycle"] + "','" + Application["partPhoto"] + "','" + Application["moldSpec"] + "','" + Application["rawMaterial"] + "','" + Application["rmGrade"] + "','" + Application["rmMake"] + "'" +
                                                ",'" + Application["rmColor"] + "','" + Application["masterbatch"] + "','" + Application["altRawMaterial"] + "','" + Application["mbName"] + "','" + Application["mbGrade"] + "','" + Application["mbMfg"] + "','" + Application["mbColor"] + "','" + Application["mbColorCode"] + "','" + Application["altRMName"] + "','" + Application["altRmGrade"] + "','" + Application["altRmMake"] + "','" + Application["altRmColor"] + "','" + Application["altMasterbatch"] + "','" + Application["altMasterbatchName"] + "','" + Application["altMasterbatchGrade"] + "','" + Application["altMasterbatchMfg"] + "','" + Application["altMasterbatchColor"] + "','" + Application["altMasterbatchColorCode"] + "','" + postOperationDropDownList.SelectedItem.Text + "','" + packagingDetailsDropDownList.SelectedItem.Text + "')";
                    Application["query"] = query;
                }
                SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                cmd.ExecuteNonQuery();
                Application["editFlag"] = null;
                Application["query"] = null;
                Application["partNo"] = null;
                con.Close();
                Response.Redirect("~/displayParts.aspx");
            }
        }

        protected void BindPostGrid()
        {
           postOperationGrid.DataSource = (DataTable)Application["PostOperation"];
           postOperationGrid.DataBind();
        }

        protected void BindPackagingDetailsGrid()
        {
            packagingDetailsGrid.DataSource = (DataTable)Application["PackagingDetails"];
            packagingDetailsGrid.DataBind();
        }

        

        protected void postOperationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string partNo = Application["partNo"].ToString();
                    string partName = Application["partName"].ToString();
                    GridViewRow row = postOperationGrid.FooterRow;
                    if (((FileUpload)row.FindControl("photoUploadFooter")).HasFile)
                    {
                        string fileName = Path.GetFileName(((FileUpload)row.FindControl("photoUploadFooter")).PostedFile.FileName);
                        string photoPath = "~/UploadedFiles/PostOpr/" + fileName;
                        ((FileUpload)row.FindControl("photoUploadFooter")).PostedFile.SaveAs(Server.MapPath(photoPath));
                        Application["photoPath"] = photoPath;
                    }

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "'"+Application["PostOperation"].ToString()+"'", true);
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {

                        sqlCon.Open();
                        string query = "INSERT INTO post_operation_details (type,target_quantity,process_description,photo,part_no,part_name) VALUES (@type,@targetQuantity,@processDesc,'"+ Application["photoPath"] + "','" + partNo + "','" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@type", (postOperationGrid.FooterRow.FindControl("postOperationTypeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@targetQuantity", (postOperationGrid.FooterRow.FindControl("txtTargetQuantityFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@processDesc", (postOperationGrid.FooterRow.FindControl("txtProcessDescriptionFooter") as TextBox).Text.Trim());
                        //cmd.Parameters.AddWithValue("@photo", (postOperationGrid.FooterRow.FindControl("photoUploadFooter") as FileUpload).FileName.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        if (Application["editFlag"] is true)
                            LoadEditValues();
                        else
                            LoadPostOperationValues();
                        lblSuccessMessage.Text = "Record Added";
                        lblErrorMessage.Text = "";
                        Application["photoPath"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void postOperationGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM post_operation_details WHERE id = @id";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(postOperationGrid.DataKeys[e.RowIndex].Value.ToString()));
                    
                    sqlCmd.ExecuteNonQuery();
                    if (Application["editFlag"] is true)
                        LoadEditValues();
                    else
                        LoadPostOperationValues();
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

        protected void packagingDetailsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string partNo = Application["partNo"].ToString();
                    string partName = Application["partName"].ToString();
                    GridViewRow row = packagingDetailsGrid.FooterRow;
                    if (((FileUpload)row.FindControl("postPhotoUploadFooter")).HasFile)
                    {
                        string fileName = Path.GetFileName(((FileUpload)row.FindControl("postPhotoUploadFooter")).PostedFile.FileName);
                        string photoPath = "~/UploadedFiles/Packaging/" + fileName;
                        ((FileUpload)row.FindControl("postPhotoUploadFooter")).PostedFile.SaveAs(Server.MapPath(photoPath));
                        Application["photoPath"] = photoPath;
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "'"+Application["PostOperation"].ToString()+"'", true);
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO packaging_details_master (type,size,qty_per_package,photo,part_no,part_name) VALUES(@type,@size,@qtyPerPackage,'"+Application["photoPath"]+"','" + partNo + "','" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@type", (packagingDetailsGrid.FooterRow.FindControl("packagingDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@size", (packagingDetailsGrid.FooterRow.FindControl("txtPostSizeFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@qtyPerPackage", (packagingDetailsGrid.FooterRow.FindControl("txtPostQuantityFooter") as TextBox).Text.Trim());
                        //cmd.Parameters.AddWithValue("@photo", (packagingDetailsGrid.FooterRow.FindControl("postPhotoUploadFooter") as FileUpload).FileName.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        if (Application["editFlag"] is true)
                            LoadEditValues();
                        else
                            LoadPackagingDetailsValues();
                        lblSuccessMessage.Text = "Record Added";
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

        protected void packagingTypeChanged(object sender, EventArgs e)
        {
            GridViewRow row = packagingDetailsGrid.FooterRow;
            if (((DropDownList)row.FindControl("packagingDropDownList")).SelectedItem.Text == "Select Packaging Type")
            {
                ((TextBox)row.FindControl("txtPostSizeFooter")).Text = "";
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT size FROM packaging_details_master WHERE type= '" + ((DropDownList)row.FindControl("packagingDropDownList")).SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            ((TextBox)row.FindControl("txtPostSizeFooter")).Text = reader["size"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
        }

        protected void packagingDetailsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM packaging_details_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(packagingDetailsGrid.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    if (Application["editFlag"] is true)
                        LoadEditValues();
                    else
                        LoadPackagingDetailsValues();
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

        protected void uploadFile_Click(object sender, EventArgs e)
        {
            if (UploadMultipleFiles.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in UploadMultipleFiles.PostedFiles)
                {
                    uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/UploadedFiles/Parts/"), Application["partNo"] + "_" + uploadedFile.FileName));
                    listUploadedFiles.Text += String.Format("<style>display:block;</style><br/>{0}", Application["partNo"] + "_" + uploadedFile.FileName);
                }
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayParts.aspx");
        }

        protected void imgBtnExtra_Click(object sender, ImageClickEventArgs e)
        {
            var temp = Server.MapPath("~/UploadedFiles/Parts/");
            if (Directory.Exists(temp))
            {
                DirectoryInfo di = new DirectoryInfo(temp);

                FileInfo[] files = di.GetFiles(Application["partNo"] + "_*.*");

                foreach (FileInfo m in files)
                {
                    File.Delete(Server.MapPath("~/UploadedFiles/Parts/"+m.ToString()+""));
                    listUploadedFiles.Text = "";
                }
            }
        }
    }
}