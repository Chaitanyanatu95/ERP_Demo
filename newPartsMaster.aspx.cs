using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ERP_Demo
{
    public partial class newPartsMaster : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
                else
                {
                    LoadValuesInController();
                }
            }
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            try
            {
                Application["Duplicate"] = false;
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    if (Application["editFlag"] is true)
                    {
                        string sqlqueryE = "SELECT part_name FROM parts_master EXCEPT SELECT part_name FROM parts_master where part_no = '" + partNoTextBox.Text.ToString() + "'";
                        Application["queryD"] = sqlqueryE;
                    }
                    else
                    {
                        string sqlqueryN = "SELECT part_name FROM parts_master";
                        Application["queryD"] = sqlqueryN;
                    }
                    using (SqlCommand cmd = new SqlCommand(Application["queryD"].ToString(), con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (partNameTextBox.Text.ToLower().Trim() == reader["part_name"].ToString().ToLower())
                            {
                                Application["Duplicate"] = true;
                            }
                        }
                        reader.Close();
                    }
                    con.Close();
                }

                if (Application["Duplicate"] is true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Part name already exists!')", true);
                }
                else
                {
                    Application["Duplicate"] = false;
                    if (partPhotoFileUpload.HasFile)
                    {
                        string fileName = Path.GetFileName(partPhotoFileUpload.PostedFile.FileName);

                        string filePath = "~/UploadedFiles/Parts/" + fileName;

                        partPhotoFileUpload.PostedFile.SaveAs(Server.MapPath(filePath));
                        Application["partPhoto"] = filePath.ToString();
                    }

                    if (moldSpecFileUpload.HasFile)
                    {
                        string fileName = Path.GetFileName(moldSpecFileUpload.PostedFile.FileName);

                        string filePath = "~/UploadedFiles/Machine/" + fileName;

                        moldSpecFileUpload.PostedFile.SaveAs(Server.MapPath(filePath));
                        Application["moldSpec"] = filePath.ToString();
                    }

                    Application["partNo"] = partNoTextBox.Text;
                    Application["partName"] = partNameTextBox.Text;
                    Application["custName"] = customerNameDropDownList.SelectedItem.Text;
                    Application["custPartNo"] = custPartNoTextBox.Text;
                    Application["prodCategory"] = familyDropDownList.SelectedItem.Text;
                    Application["moldName"] = moldNameTextBox.Text;
                    Application["moldMfgYear"] = moldYearTextBox.Text;
                    Application["moldLife"] = moldLifeTextBox.Text;
                    Application["noOfCavities"] = noOfCavitiesTextBox.Text;
                    Application["unit"] = unitMeasurementDropDownList.SelectedItem.Text;
                    Application["partWeight"] = partWeightTextBox.Text;
                    Application["shotWeight"] = shotWeightTextBox.Text;
                    Application["cycleTime"] = cycleTimeTextBox.Text;
                    Application["jigReq"] = jigReqDropDownList.SelectedItem.Text;
                    Application["moldProductionCycle"] = moldProductionCycleTextBox.Text;
                    Application["samplePartNo"] = samplePartNo.Text;
                    Application["tempPartName"] = "";
                    Application["queryD"] = "";
                    if (Application["Duplicate"] is false)
                    {
                        Application["Duplicate"] = null;
                        Response.Redirect("~/rawMaterialPage.aspx");
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void LoadEditValuesInController()
        {
            try
            {
                Application["Duplicate"] = false;
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    String sqlquery = "SELECT * FROM parts_master where part_no = @part_no";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@part_no", (Application["partNoEdit"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            partNoTextBox.Text = reader["part_no"].ToString();
                            partNameTextBox.Text = reader["part_name"].ToString();
                            Application["CustName"] = reader["customer_name"].ToString();
                            custPartNoTextBox.Text = reader["customer_part_no"].ToString();
                            Application["ProdCategory"] = reader["product_category"].ToString();
                            moldNameTextBox.Text = reader["mold_name"].ToString();
                            moldYearTextBox.Text = reader["mold_mfg_year"].ToString();
                            moldLifeTextBox.Text = reader["mold_life"].ToString();
                            noOfCavitiesTextBox.Text = reader["no_of_cavities"].ToString();
                            Application["Abbreviation"] = reader["unit_of_measurement"].ToString();
                            Application["jigFixtureReq"] = reader["jig_fixture_req"].ToString();
                            partWeightTextBox.Text = reader["part_weight"].ToString();
                            shotWeightTextBox.Text = reader["shot_weight"].ToString();
                            cycleTimeTextBox.Text = reader["cycle_time"].ToString();
                            lblFile.Text = reader["part_photo"].ToString();
                            lblMoldFile.Text = reader["mold_spec_sheet"].ToString();
                            moldProductionCycleTextBox.Text = reader["production_in_pcs"].ToString();

                            /*** RAW MATERIAL FOR THE PART ***/

                            Application["rawMaterialName"] = reader["raw_material"].ToString();
                            Application["rawMaterialGrade"] = reader["rm_grade"].ToString();
                            Application["rawMaterialMake"] = reader["rm_make"].ToString();
                            Application["rawMaterialColor"] = reader["rm_color"].ToString();


                            /*** POST OPERATIONS ***/
                            Application["postOperationDetailsReq"] = reader["post_operation_required"].ToString();
                            Application["packagingDetailsReq"] = reader["packaging_details_required"].ToString();

                            /*** MASTERBATCH FOR THE PART ***/

                            Application["masterbatchSelected"] = reader["masterbatch"].ToString();
                            if (Application["masterbatchSelected"].ToString() == "YES")
                            {
                                Application["mbNameSelected"] = reader["mb_name"].ToString();
                                Application["mbGradeSelected"] = reader["mb_grade"].ToString();
                                Application["mbMfgSelected"] = reader["mb_mfg"].ToString();
                                Application["mbColorSelected"] = reader["mb_color"].ToString();
                                Application["mbColorCodeSelected"] = reader["mb_color_code"].ToString();
                                Application["mbPercentageValue"] = reader["mb_percentage"].ToString();

                            }

                            /*** ALt Raw Material Of The Part ***/
                            Application["altRawMaterialSelected"] = reader["alt_raw_material"].ToString();
                            if (Application["altRawMaterialSelected"].ToString() == "YES")
                            {
                                Application["altRMSelected"] = reader["alt_rm_name"].ToString();
                                Application["altRMGradeSelected"] = reader["alt_rm_grade"].ToString();
                                Application["altRMMakeSelected"] = reader["alt_rm_make"].ToString();
                                Application["altRMColorSelected"] = reader["alt_rm_color"].ToString();
                            }

                            /*** Alt Raw Material Masterbatch Of The Part ***/
                            Application["altRawMaterialMasterBatchSelected"] = reader["alt_masterbatch"].ToString();
                            if (Application["altRawMaterialMasterBatchSelected"].ToString() == "YES")
                            {
                                Application["altRMMBNameSelected"] = reader["alt_mb_name"].ToString();
                                Application["altRMMBGradeSelected"] = reader["alt_mb_grade"].ToString();
                                Application["altRMMBMfgSelected"] = reader["alt_mb_mfg"].ToString();
                                Application["altRMMBColorSelected"] = reader["alt_mb_color"].ToString();
                                Application["altRMMBColorCodeSelected"] = reader["alt_mb_color_code"].ToString();
                                Application["altRMMBPercentageValue"] = reader["alt_mb_percentage"].ToString();
                            }
                        }
                        reader.Close();
                    }

                    if (Application["jigFixtureReq"].ToString() == "YES")
                        jigReqDropDownList.Items.Remove(jigReqDropDownList.Items.FindByValue("YES"));
                    else
                        jigReqDropDownList.Items.Remove(jigReqDropDownList.Items.FindByValue("NO"));

                    jigReqDropDownList.Items.Insert(0, new ListItem(Application["jigFixtureReq"].ToString()));
                    
                    using (SqlCommand cmd = new SqlCommand("SELECT Family FROM family_master where Family NOT IN('" + Application["ProdCategory"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        familyDropDownList.DataSource = reader;
                        familyDropDownList.DataBind();
                        reader.Close();
                        familyDropDownList.Items.Insert(0, new ListItem(Application["ProdCategory"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT abbreviation FROM unit_of_measurement_master where abbreviation NOT IN('" + Application["Abbreviation"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        unitMeasurementDropDownList.DataSource = reader;
                        unitMeasurementDropDownList.DataBind();
                        reader.Close();
                        unitMeasurementDropDownList.Items.Insert(0, new ListItem(Application["Abbreviation"].ToString()));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT customer_name FROM customer_master where customer_name NOT IN('" + Application["CustName"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        customerNameDropDownList.DataSource = reader;
                        customerNameDropDownList.DataBind();
                        reader.Close();
                        customerNameDropDownList.Items.Insert(0, new ListItem(Application["CustName"].ToString()));
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void LoadValuesInController()
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM parts_master ORDER BY ID DESC", con))
                    {
                        // Int32 count = (Int32)cmd.ExecuteScalar();
                        //System.Diagnostics.Debug.WriteLine(count);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["input"] = reader["part_no"];
                        }
                        reader.Close();
                        if (Application["input"] == null)
                            Application["input"] = 0;
                        int input = int.Parse(Regex.Replace(Application["input"].ToString(), "[^0-9]+", string.Empty));
                        //System.Diagnostics.Debug.WriteLine(input);
                        partNoTextBox.Text = "PBP#" + (input + 1);
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT Family FROM family_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        familyDropDownList.DataSource = reader;
                        familyDropDownList.DataBind();
                        reader.Close();
                        familyDropDownList.Items.Insert(0, new ListItem("Select Family", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT abbreviation FROM unit_of_measurement_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        unitMeasurementDropDownList.DataSource = reader;
                        unitMeasurementDropDownList.DataBind();
                        reader.Close();
                        unitMeasurementDropDownList.Items.Insert(0, new ListItem("Select Unit Of Measurement", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT customer_name FROM customer_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        customerNameDropDownList.DataSource = reader;
                        customerNameDropDownList.DataBind();
                        reader.Close();
                        customerNameDropDownList.Items.Insert(0, new ListItem("Select Customer Name", ""));
                    }

                    jigReqDropDownList.Items.Insert(0, new ListItem("SELECT"));

                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void moldProductionCycleTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cycleTime = cycleTimeTextBox.Text.Trim();
                string noOfCavities = noOfCavitiesTextBox.Text.Trim();
                double cycleNum;
                double cavitiesNum;
                bool isCycleNum = double.TryParse(cycleTime, out cycleNum);
                bool isCavitiesNum = double.TryParse(noOfCavities, out cavitiesNum);

                if (isCycleNum && isCavitiesNum && cycleNum != 0 && cavitiesNum != 0 && cycleTime != null && noOfCavities != null)
                    moldProductionCycleTextBox.Text = Math.Ceiling((3600 / double.Parse(cycleTime)) * double.Parse(noOfCavities) * 0.9).ToString();
                else if (cycleTime == "0" || noOfCavities == "0")
                    moldProductionCycleTextBox.Text = "0";
                else
                    moldProductionCycleTextBox.Text = "";
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/displayParts.aspx");
        }

        protected void btnFile_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());

                if (lblFile.Text != null)
                {
                    if (Application["editFlag"] is true)
                    {
                        var path = Server.MapPath(lblFile.Text);
                        //System.Diagnostics.Debug.WriteLine(path);
                        if (File.Exists(path))
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE parts_master set part_photo = '' where part_no = '" + Application["partNoEdit"] + "'", con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                File.Delete(Server.MapPath(lblFile.Text));
                            }
                            errorFile.Text = "";
                            lblFile.Text = "";
                            con.Close();
                        }
                    }
                    else
                    {
                        errorFile.Text = "";
                        lblFile.Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void btnMoldFile_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());

                if (lblMoldFile.Text != null)
                {
                    if (Application["editFlag"] is true)
                    {
                        var path = Server.MapPath(lblMoldFile.Text);
                        //System.Diagnostics.Debug.WriteLine(path);
                        if (File.Exists(path))
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE parts_master set mold_spec_sheet = '' where part_no = '" + Application["partNoEdit"] + "'", con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                File.Delete(Server.MapPath(lblMoldFile.Text));
                            }
                            errorMoldFile.Text = "";
                            lblMoldFile.Text = "";
                            con.Close();
                        }
                        else
                        {
                            errorMoldFile.Text = "";
                            lblMoldFile.Text = "";
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

        protected void noOfCavitiesTextBox_TextChanged(object sender, EventArgs e)
        {
            cycleTimeTextBox.Text = "";
            moldProductionCycleTextBox.Text = "";
        }
    }
}