using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ERP_Demo
{
    public partial class postOperationsPage : Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
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
            try
            {
                /************** POSTOPERATION ****************/
                DataTable dtPostOperation = new DataTable();
                dtPostOperation.Columns.AddRange(new DataColumn[4] { new DataColumn("TYPE"), new DataColumn("TARGET QUANTITY"), new DataColumn("REMARKS"), new DataColumn("PHOTO") });
                Application["PostOperation"] = dtPostOperation;
                Application["tempPartNo"] = string.Empty;
                BindPostGrid();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "SELECT part_no FROM parts_master where part_no= '" + Application["partNo"].ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["tempPartNo"] = reader["part_no"];
                    }
                    reader.Close();
                    if (Application["tempPartNo"].ToString() != "")
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM post_operation_details where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                        sqlDa.Fill(dtPostOperation);
                        Application["tempPartNo"] = null;
                    }
                    else
                    {
                        //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM post_operation_details where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                        string postSelectQuery = "SELECT part_no FROM post_operation_details where part_no= '" + Application["partNo"].ToString() + "'";
                        SqlCommand selCmd = new SqlCommand(postSelectQuery, sqlCon);
                        Application["postSelectData"] = string.Empty;
                        SqlDataReader selReader = selCmd.ExecuteReader();
                        while (selReader.Read())
                        {
                            if (selReader["part_no"].ToString() != "")
                            {
                                Application["postSelectData"] = selReader["part_no"].ToString();
                            }
                        }
                        selReader.Close();
                        if (Application["postSelectData"].ToString() != "" && Application["rowCommand"] is false)
                        {
                            string postQuery = "DELETE FROM post_operation_details where part_no= '" + Application["partNo"].ToString() + "'";
                            SqlCommand cmmd = new SqlCommand(postQuery, sqlCon);
                            cmmd.ExecuteNonQuery();
                            Application["postSelectData"] = null;
                        }
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM post_operation_details where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                        sqlDa.Fill(dtPostOperation);
                    }
                }
                if (dtPostOperation.Rows.Count > 0)
                {
                    BindPostGrid();
                    var drop = (DropDownList)postOperationGrid.FooterRow.FindControl("postOperationTypeDropDownList");
                    drop.Items.Insert(0, new ListItem("Select Type"));
                    drop.Items.Add(new ListItem("N/A", "-1"));
                }
                else
                {
                    dtPostOperation.Rows.Add(dtPostOperation.NewRow());
                    BindPostGrid();
                    var drop = (DropDownList)postOperationGrid.FooterRow.FindControl("postOperationTypeDropDownList");
                    drop.Items.Insert(0, new ListItem("Select Type"));
                    drop.Items.Add(new ListItem("N/A", "-1"));
                    postOperationGrid.Rows[0].Cells.Clear();
                    postOperationGrid.Rows[0].Cells.Add(new TableCell());
                    postOperationGrid.Rows[0].Cells[0].ColumnSpan = dtPostOperation.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    postOperationGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadPackagingDetailsValues()
        {
            try
            {
                /************* PACKAGING DETAILS *****************/
                DataTable dtPackaging = new DataTable();
                dtPackaging.Columns.AddRange(new DataColumn[4] { new DataColumn("PACKAGING TYPE"), new DataColumn("SIZE"), new DataColumn("QTY PER PACKAGE"), new DataColumn("PHOTO") });
                Application["PackagingDetails"] = dtPackaging;
                Application["tempPartNo"] = string.Empty;
                BindPackagingDetailsGrid();

                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "SELECT part_no FROM parts_master where part_no= '" + Application["partNo"].ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["tempPartNo"] = reader["part_no"];
                    }
                    reader.Close();
                    if (Application["tempPartNo"].ToString() != "")
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM packaging_details_master where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                        sqlDa.Fill(dtPackaging);
                    }
                    else
                    {
                        //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM post_operation_details where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                        string packSelectQuery = "SELECT part_no FROM packaging_details_master where part_no= '" + Application["partNo"].ToString() + "'";
                        SqlCommand selCmd = new SqlCommand(packSelectQuery, sqlCon);
                        Application["packSelectData"] = string.Empty;
                        SqlDataReader selReader = selCmd.ExecuteReader();
                        while (selReader.Read())
                        {
                            if (selReader["part_no"].ToString() != "")
                            {
                                Application["packSelectData"] = selReader["part_no"].ToString();
                            }
                        }
                        selReader.Close();
                        if (Application["packSelectData"].ToString() != "" && Application["rowCommand"] is false)
                        {
                            string postQuery = "DELETE FROM packaging_details_master where part_no= '" + Application["partNo"].ToString() + "'";
                            SqlCommand cmmd = new SqlCommand(postQuery, sqlCon);
                            cmmd.ExecuteNonQuery();
                            Application["packSelectData"] = null;
                        }
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM packaging_details_master where part_no = '" + Application["partNo"].ToString() + "'", sqlCon);
                        sqlDa.Fill(dtPackaging);
                    }
                }
                if (dtPackaging.Rows.Count > 0)
                {
                    BindPackagingDetailsGrid();

                    var drop2 = (DropDownList)packagingDetailsGrid.FooterRow.FindControl("packagingDropDownList");
                    drop2.Items.Insert(0, new ListItem("Select Packaging"));
                    drop2.Items.Add(new ListItem("N/A", "-1"));
                }
                else
                {
                    dtPackaging.Rows.Add(dtPackaging.NewRow());
                    BindPackagingDetailsGrid();

                    var drop2 = (DropDownList)packagingDetailsGrid.FooterRow.FindControl("packagingDropDownList");
                    drop2.Items.Insert(0, new ListItem("Select Packaging"));
                    drop2.Items.Add(new ListItem("N/A", "-1"));

                    //packagingDetailsDropDownList.Items.Insert(0, new ListItem("Select Packaging Type", ""));
                    packagingDetailsGrid.Rows[0].Cells.Clear();
                    packagingDetailsGrid.Rows[0].Cells.Add(new TableCell());
                    packagingDetailsGrid.Rows[0].Cells[0].ColumnSpan = dtPackaging.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    packagingDetailsGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadEditValues()
        {
            try
            {
                if (Application["postOperationDetailsReq"].ToString() == "NO")
                {
                    if (postOperationDropDownList.SelectedItem.Text == "YES")
                    {
                        postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("SELECT"));
                        postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("YES"));
                        postOperationDropDownList.Items.Insert(0, new ListItem("YES"));
                        LoadPostOperationValues();
                    }
                    else
                    {
                        postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("SELECT"));
                        postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("YES"));
                        postOperationDropDownList.SelectedItem.Text = Application["postOperationDetailsReq"].ToString();
                        postOperationDropDownList.Items.Insert(1, new ListItem("YES"));
                        LoadPostOperationValues();
                    }
                }
                else
                {
                    postOperationDropDownList.Items.Remove(postOperationDropDownList.Items.FindByValue("SELECT"));
                    LoadPostOperationValues();
                }

                if (Application["packagingDetailsReq"].ToString() == "NO")
                {
                    if (packagingDetailsDropDownList.SelectedItem.Text == "YES")
                    {
                        packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("SELECT"));
                        packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("YES"));
                        packagingDetailsDropDownList.Items.Insert(0, new ListItem("YES"));
                        LoadPackagingDetailsValues();
                    }
                    else
                    {
                        packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("SELECT"));
                        packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("YES"));
                        packagingDetailsDropDownList.SelectedItem.Text = Application["packagingDetailsReq"].ToString();
                        packagingDetailsDropDownList.Items.Insert(1, new ListItem("YES"));
                        LoadPackagingDetailsValues();
                    }
                }
                else
                {
                    packagingDetailsDropDownList.Items.Remove(packagingDetailsDropDownList.Items.FindByValue("SELECT"));
                    LoadPackagingDetailsValues();
                }

                var temp = Server.MapPath("~/UploadedFiles/Parts/");
                if (Directory.Exists(temp))
                {
                    DirectoryInfo di = new DirectoryInfo(temp);

                    FileInfo[] files = di.GetFiles(Application["partNo"] + "_*.*");

                    foreach (FileInfo m in files)
                    {
                        //System.Diagnostics.Debug.WriteLine(m.Name.ToString());
                        listUploadedFiles.Text += String.Format("<style> display:block; </style><br/> {0}", m.Name.ToString());
                        imgBtnExtra.ImageUrl = "~/Images/cancel.png";
                        //"<asp:ImageButton ID = 'btnExtraFile' runat = 'server' ImageUrl = '~/Images/cancel.png' Height = '20' Width = '20' OnClick = 'btnExtraFile_Click' />"
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application["partNo"].ToString() == "" || Application["partName"].ToString() == "" || Application["custName"].ToString() == "" || Application["prodCategory"].ToString() == "" || Application["moldName"].ToString() == "" || Application["moldMfgYear"].ToString() == "" || Application["noOfCavities"].ToString() == "" || Application["unit"].ToString() == "" || Application["partWeight"].ToString() == "" || Application["shotWeight"].ToString() == "" || Application["cycleTime"].ToString() == "" || Application["jigReq"].ToString() == "" || Application["moldProductionCycle"].ToString() == "" || Application["rawMaterial"].ToString() == "")
                {
                    lblErrorMessage.Text = "Missing Data";
                }
                else
                {
                    lblErrorMessage.Text = "";
                    Application["postOperationDetailsReq"] = postOperationDropDownList.SelectedItem.Text;
                    Application["packagingDetailsReq"] = packagingDetailsDropDownList.SelectedItem.Text;

                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();

                    if (Application["postOperationDetailsReq"].ToString() == "NO")
                    {
                        string query = "DELETE FROM post_operation_details where part_no = '" + Application["partNo"] + "'";
                        SqlCommand cmmd = new SqlCommand(query.ToString(), con);
                        cmmd.ExecuteNonQuery();
                    }
                    if (Application["packagingDetailsReq"].ToString() == "NO")
                    {
                        string query = "DELETE FROM packaging_details_master where part_no = '" + Application["partNo"] + "'";
                        SqlCommand cmmd = new SqlCommand(query.ToString(), con);
                        cmmd.ExecuteNonQuery();
                    }
                    if (Application["editFlag"] is true)
                    {
                        string query = "UPDATE parts_master SET part_name='" + Application["partName"] + "', customer_name='" + Application["custName"] + "', customer_part_no = '" + Application["custPartNo"] + "',product_category='" + Application["prodCategory"] + "',mold_name='" + Application["moldName"] + "',mold_mfg_year='" + Application["moldMfgYear"] + "',mold_life='" + Application["moldLife"] + "',no_of_cavities='" + Application["noOfCavities"] + "',unit_of_measurement = '" + Application["unit"] + "',part_weight = '" + Application["partWeight"] + "',shot_weight='" + Application["shotWeight"] + "',cycle_time='" + Application["cycleTime"] + "'," +
                            "jig_fixture_req='" + Application["jigReq"] + "',production_in_pcs = '" + Application["moldProductionCycle"] + "',sample_part_no = '" + Application["samplePartNo"] + "',part_photo='" + Application["partPhoto"] + "',mold_spec_sheet='" + Application["moldSpec"] + "',raw_material='" + Application["rawMaterial"] + "',rm_grade='" + Application["rmGrade"] + "',rm_make='" + Application["rmMake"] + "',rm_color='" + Application["rmColor"] + "',masterbatch='" + Application["masterbatch"] + "',alt_raw_material='" + Application["altRawMaterial"] + "',mb_name='" + Application["mbName"] + "',mb_grade='" + Application["mbGrade"] + "',mb_mfg='" + Application["mbMfg"] + "',mb_color='" + Application["mbColor"] + "',mb_color_code='" + Application["mbColorCode"] + "',mb_percentage='" + Application["mbPercentage"] + "',alt_rm_name='" + Application["altRMName"] + "',alt_rm_grade='" + Application["altRmGrade"] + "'" +
                            ",alt_rm_make='" + Application["altRmMake"] + "',alt_rm_color='" + Application["altRmColor"] + "',alt_masterbatch='" + Application["altMasterbatch"] + "',alt_mb_name='" + Application["altMasterbatchName"] + "',alt_mb_grade='" + Application["altMasterbatchGrade"] + "',alt_mb_mfg='" + Application["altMasterbatchMfg"] + "',alt_mb_color='" + Application["altMasterbatchColor"] + "',alt_mb_color_code='" + Application["altMasterbatchColorCode"] + "',alt_mb_percentage='" + Application["altMasterbatchPercentage"] + "',post_operation_required ='" + postOperationDropDownList.SelectedItem.Text + "' ,packaging_details_required = '" + packagingDetailsDropDownList.SelectedItem.Text + "' WHERE part_no = '" + Application["partNo"] + "'";
                        Application["query"] = query;

                        if (Application["postOperationDetailsReq"].ToString() == "YES")
                        {
                            string query2 = "UPDATE post_operation_details SET part_name='" + Application["partName"] + "' WHERE part_no = '"+Application["partNo"]+"'";
                            SqlCommand cmd2 = new SqlCommand(query2.ToString(), con);
                            cmd2.ExecuteNonQuery();
                        }
                        if (Application["packagingDetailsReq"].ToString() == "YES")
                        {
                            string query3 = "UPDATE packaging_details_master SET part_name='" + Application["partName"] + "' WHERE part_no = '" + Application["partNo"] + "'";
                            SqlCommand cmd3 = new SqlCommand(query3.ToString(), con);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string query = "INSERT INTO parts_master(part_no,part_name,customer_name,customer_part_no,product_category,mold_name,mold_mfg_year,mold_life,no_of_cavities,unit_of_measurement,part_weight,shot_weight,cycle_time,jig_fixture_req,production_in_pcs,sample_part_no,part_photo,mold_spec_sheet,raw_material,rm_grade,rm_make,rm_color,masterbatch,alt_raw_material,mb_name,mb_grade,mb_mfg,mb_color,mb_color_code,mb_percentage,alt_rm_name,alt_rm_grade," +
                                                    "alt_rm_make,alt_rm_color,alt_masterbatch,alt_mb_name,alt_mb_grade,alt_mb_mfg,alt_mb_color,alt_mb_color_code,alt_mb_percentage,post_operation_required,packaging_details_required)VALUES('" + Application["partNo"] + "','" + Application["partName"] + "','" + Application["custName"] + "','" + Application["custPartNo"] + "','" + Application["prodCategory"] + "','" + Application["moldName"] + "','" + Application["moldMfgYear"] + "','" + Application["moldLife"] + "','" + Application["noOfCavities"] + "','" + Application["unit"] + "','" + Application["partWeight"] + "','" + Application["shotWeight"] + "','" + Application["cycleTime"] + "','" + Application["jigReq"] + "','" + Application["moldProductionCycle"] + "','" + Application["samplePartNo"] + "','" + Application["partPhoto"] + "','" + Application["moldSpec"] + "','" + Application["rawMaterial"] + "','" + Application["rmGrade"] + "','" + Application["rmMake"] + "'" +
                                                    ",'" + Application["rmColor"] + "','" + Application["masterbatch"] + "','" + Application["altRawMaterial"] + "','" + Application["mbName"] + "','" + Application["mbGrade"] + "','" + Application["mbMfg"] + "','" + Application["mbColor"] + "','" + Application["mbColorCode"] + "','"+ Application["mbPercentage"] + "','" + Application["altRMName"] + "','" + Application["altRmGrade"] + "','" + Application["altRmMake"] + "','" + Application["altRmColor"] + "','" + Application["altMasterbatch"] + "','" + Application["altMasterbatchName"] + "','" + Application["altMasterbatchGrade"] + "','" + Application["altMasterbatchMfg"] + "','" + Application["altMasterbatchColor"] + "','" + Application["altMasterbatchColorCode"] + "','"+ Application["altMasterbatchPercentage"] + "','" + postOperationDropDownList.SelectedItem.Text + "','" + packagingDetailsDropDownList.SelectedItem.Text + "')";
                        Application["query"] = query;
                    }
                    SqlCommand cmd = new SqlCommand(Application["query"].ToString(), con);
                    cmd.ExecuteNonQuery();
                    Application["editFlag"] = null;
                    Application["altRmFlag"] = null;
                    Application["altMbFlag"] = null;
                    Application["mbFlag"] = null;
                    Application["query"] = null;
                    Application["partNo"] = null;
                    Application["rowCommand"] = null;
                    con.Close();
                    Response.Redirect("~/displayParts.aspx");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
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
                    using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO post_operation_details (type,target_quantity,process_description,photo,part_no,part_name) VALUES (@type,@targetQuantity,@processDesc,'" + Application["photoPath"] + "','" + partNo + "','" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@type", (postOperationGrid.FooterRow.FindControl("postOperationTypeDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@targetQuantity", (postOperationGrid.FooterRow.FindControl("txtTargetQuantityFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@processDesc", (postOperationGrid.FooterRow.FindControl("txtProcessDescriptionFooter") as TextBox).Text.Trim());
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();

                        if (Application["editFlag"] is true)
                        {
                            LoadEditValues();
                        }
                        else
                        {
                            Application["rowCommand"] = true;
                            LoadPostOperationValues();
                        }

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
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
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
                    using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO packaging_details_master (type,size,qty_per_package,photo,part_no,part_name) VALUES(@type,@size,@qtyPerPackage,'" + Application["photoPath"] + "','" + partNo + "','" + partName + "')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@type", (packagingDetailsGrid.FooterRow.FindControl("packagingDropDownList") as DropDownList).SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("@size", (packagingDetailsGrid.FooterRow.FindControl("txtPostSizeFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@qtyPerPackage", (packagingDetailsGrid.FooterRow.FindControl("txtPostQuantityFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@photo", (packagingDetailsGrid.FooterRow.FindControl("postPhotoUploadFooter") as FileUpload).FileName.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();

                        if (Application["editFlag"] is true)
                        {
                            LoadEditValues();
                        }
                        else
                        {
                            Application["rowCommand"] = true;
                            LoadPackagingDetailsValues();
                        }

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
            try
            {
                GridViewRow row = packagingDetailsGrid.FooterRow;

                if (((DropDownList)row.FindControl("packagingDropDownList")).SelectedItem.Text == "N/A")
                {
                    ((ImageButton)row.FindControl("packImgBtn")).Visible = false;
                }
                else
                {
                    ((ImageButton)row.FindControl("packImgBtn")).Visible = true;
                }
                if (((DropDownList)row.FindControl("packagingDropDownList")).SelectedItem.Text == "Select Packaging Type")
                {
                    ((TextBox)row.FindControl("txtPostSizeFooter")).Text = "";
                }
                else
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    using (con)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT size FROM packaging_master WHERE packaging_type= '" + ((DropDownList)row.FindControl("packagingDropDownList")).SelectedItem.Text + "' ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                ((TextBox)row.FindControl("txtPostSizeFooter")).Text = reader["size"].ToString();
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void packagingDetailsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
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
            try
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
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    Application["postQuery"] = string.Empty;

                    string postSelectQuery = "SELECT part_no FROM post_operation_details where part_no= '" + Application["partNo"].ToString() + "'";
                    SqlCommand selCmd = new SqlCommand(postSelectQuery, sqlCon);
                    SqlDataReader selReader = selCmd.ExecuteReader();
                    while (selReader.Read())
                    {
                        if (selReader["part_no"].ToString() != "" && Application["editFlag"] is false)
                        {
                            string postQuery = "DELETE FROM post_operation_details where part_no= '" + Application["partNo"].ToString() + "'";
                            Application["postQuery"] = postQuery;
                        }
                    }
                    selReader.Close();
                    if (Application["postQuery"].ToString() != "")
                    {
                        SqlCommand cmmd = new SqlCommand(Application["postQuery"].ToString(), sqlCon);
                        cmmd.ExecuteNonQuery();
                    }
                    sqlCon.Close();
                }

                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    Application["packQuery"] = string.Empty;

                    string packSelectQuery = "SELECT part_no FROM packaging_details_master where part_no= '" + Application["partNo"].ToString() + "'";
                    SqlCommand selCmd = new SqlCommand(packSelectQuery, sqlCon);
                    SqlDataReader selReader = selCmd.ExecuteReader();
                    while (selReader.Read())
                    {
                        if (selReader["part_no"].ToString() != "" && Application["editFlag"] is false)
                        {
                            string packQuery = "DELETE FROM packaging_details_master where part_no= '" + Application["partNo"].ToString() + "'";
                            Application["packQuery"] = packQuery;
                        }
                    }
                    selReader.Close();
                    if (Application["packQuery"].ToString() != "")
                    {
                        SqlCommand cmmd = new SqlCommand(Application["packQuery"].ToString(), sqlCon);
                        cmmd.ExecuteNonQuery();
                    }
                    sqlCon.Close();
                }

                if (Application["editFlag"] is true)
                    Application["editFlag"] = null;

                Application["altRmFlag"] = null;
                Application["altMbFlag"] = null;
                Application["mbFlag"] = null;
                Application["rowCommand"] = false;

                Response.Redirect("~/displayParts.aspx");
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void imgBtnExtra_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var temp = Server.MapPath("~/UploadedFiles/Parts/");
                if (Directory.Exists(temp))
                {
                    DirectoryInfo di = new DirectoryInfo(temp);

                    FileInfo[] files = di.GetFiles(Application["partNo"] + "_*.*");

                    foreach (FileInfo m in files)
                    {
                        File.Delete(Server.MapPath("~/UploadedFiles/Parts/" + m.ToString() + ""));
                        listUploadedFiles.Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void postOperationTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = postOperationGrid.FooterRow;
                if (((DropDownList)row.FindControl("postOperationTypeDropDownList")).SelectedItem.Text == "N/A")
                {
                    ((ImageButton)row.FindControl("postImgBtn")).Visible = false;
                    ((TextBox)row.FindControl("txtTargetQuantityFooter")).Text = "0";
                    ((TextBox)row.FindControl("txtTargetQuantityFooter")).ReadOnly = true;
                }
                else
                {
                    ((ImageButton)row.FindControl("postImgBtn")).Visible = true;
                    ((TextBox)row.FindControl("txtTargetQuantityFooter")).ReadOnly = false;
                    ((TextBox)row.FindControl("txtTargetQuantityFooter")).Text = "";
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}