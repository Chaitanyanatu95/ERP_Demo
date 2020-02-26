using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class rawMaterialPage : Page
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

        protected void LoadEditValuesInController()
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_name FROM raw_material_master where material_name NOT IN('" + Application["rawMaterialName"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        rawMaterialDropDownList.DataSource = reader;
                        rawMaterialDropDownList.DataBind();
                        rawMaterialDropDownList.Items.Insert(0, new ListItem(Application["rawMaterialName"].ToString()));
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_grade FROM raw_material_master where material_name = '" + Application["rawMaterialName"].ToString() + "' AND material_grade NOT IN('" + Application["rawMaterialGrade"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        rmGradeDropDownList.DataSource = reader;
                        rmGradeDropDownList.DataBind();
                        rmGradeDropDownList.Items.Insert(0, new ListItem(Application["rawMaterialGrade"].ToString()));
                        reader.Close();
                    }

                    rmMakeTextBox.Text = Application["rawMaterialMake"].ToString();
                    colourTextBox.Text = Application["rawMaterialColor"].ToString();

                    if (Application["masterbatchSelected"].ToString() == "YES")
                        masterbatchDropDownList.Items.Remove(masterbatchDropDownList.Items.FindByValue("NO"));
                    else
                        masterbatchDropDownList.Items.Remove(masterbatchDropDownList.Items.FindByValue("YES"));

                    masterbatchDropDownList.SelectedItem.Text = Application["masterbatchSelected"].ToString();

                    if (Application["masterbatchSelected"].ToString() == "YES")
                        masterbatchDropDownList.Items.Insert(1, new ListItem("NO"));
                    else
                        masterbatchDropDownList.Items.Insert(1, new ListItem("YES"));

                    if (Application["masterbatchSelected"].ToString() == "YES")
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_name FROM masterbatch_master where mb_name NOT IN('" + Application["mbNameSelected"].ToString() + "')", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            mbNameDropDownList.DataSource = reader;
                            mbNameDropDownList.DataBind();
                            mbNameDropDownList.Items.Insert(0, new ListItem(Application["mbNameSelected"].ToString()));
                            reader.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_grade FROM masterbatch_master where mb_name = '" + Application["mbNameSelected"].ToString() + "' AND mb_grade NOT IN('" + Application["mbGradeSelected"].ToString() + "') ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            mbGradeDropDownList.DataSource = reader;
                            mbGradeDropDownList.DataBind();
                            mbGradeDropDownList.Items.Insert(0, new ListItem(Application["mbGradeSelected"].ToString()));
                            reader.Close();
                        }

                        mbMfgTextBox.Text = Application["mbMfgSelected"].ToString();
                        mbColorTextBox.Text = Application["mbColorSelected"].ToString();
                        mbColorCodeTextBox.Text = Application["mbColorCodeSelected"].ToString();
                        mbPercentageTextBox.Text = Application["mbPercentageValue"].ToString();
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_name FROM masterbatch_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            mbNameDropDownList.DataSource = reader;
                            mbNameDropDownList.DataBind();
                            mbNameDropDownList.Items.Insert(0, new ListItem("Select Masterbatch", ""));
                            reader.Close();
                        }
                        mbMfgTextBox.Text = "";
                        mbColorTextBox.Text = "";
                        mbColorCodeTextBox.Text = "";
                    }

                    if (Application["altRawMaterialSelected"].ToString() == "YES")
                        altRawMaterialDropDownList.Items.Remove(altRawMaterialDropDownList.Items.FindByValue("NO"));
                    else
                        altRawMaterialDropDownList.Items.Remove(altRawMaterialDropDownList.Items.FindByValue("YES"));

                    altRawMaterialDropDownList.SelectedItem.Text = Application["altRawMaterialSelected"].ToString();

                    if (Application["altRawMaterialSelected"].ToString() == "YES")
                        altRawMaterialDropDownList.Items.Insert(1, new ListItem("NO"));
                    else
                        altRawMaterialDropDownList.Items.Insert(1, new ListItem("YES"));

                    if (Application["altRawMaterialSelected"].ToString() == "YES")
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_name FROM raw_material_master where material_name NOT IN ('" + Application["altRMSelected"].ToString() + "')", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRMDropDownList.DataSource = reader;
                            altRMDropDownList.DataBind();
                            altRMDropDownList.Items.Insert(0, new ListItem(Application["altRMSelected"].ToString()));
                            reader.Close();
                        }
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_grade FROM raw_material_master where material_name = '" + Application["altRMSelected"].ToString() + "' AND material_grade NOT IN('" + Application["altRMGradeSelected"].ToString() + "') ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRawMaterialGradeDropDownList.DataSource = reader;
                            altRawMaterialGradeDropDownList.DataBind();
                            altRawMaterialGradeDropDownList.Items.Insert(0, new ListItem(Application["altRMGradeSelected"].ToString()));
                            reader.Close();
                        }
                        altRawMaterialMakeTextBox.Text = Application["altRMMakeSelected"].ToString();
                        altColourTextBox.Text = Application["altRMColorSelected"].ToString();
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_name FROM raw_material_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRMDropDownList.DataSource = reader;
                            altRMDropDownList.DataBind();
                            altRMDropDownList.Items.Insert(0, new ListItem("Select Raw Material", ""));
                            reader.Close();
                        }
                        altRawMaterialMakeTextBox.Text = "";
                        altColourTextBox.Text = "";
                    }

                    if (Application["altRawMaterialMasterBatchSelected"].ToString() == "YES")
                        altRawMaterialMasterBatchDropDownList.Items.Remove(altRawMaterialMasterBatchDropDownList.Items.FindByValue("NO"));
                    else
                        altRawMaterialMasterBatchDropDownList.Items.Remove(altRawMaterialMasterBatchDropDownList.Items.FindByValue("YES"));

                    altRawMaterialMasterBatchDropDownList.SelectedItem.Text = Application["altRawMaterialMasterBatchSelected"].ToString();

                    if (Application["altRawMaterialMasterBatchSelected"].ToString() == "YES")
                    {
                        altRawMaterialMasterBatchDropDownList.Items.Insert(1, new ListItem("NO"));
                    }
                    else
                    {
                        altRawMaterialMasterBatchDropDownList.Items.Insert(1, new ListItem("YES"));
                    }
                    if (Application["altRawMaterialMasterBatchSelected"].ToString() == "YES")
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_name FROM masterbatch_master where mb_name NOT IN('" + Application["altRMMBNameSelected"].ToString() + "')", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRMBNameDropDownList.DataSource = reader;
                            altRMBNameDropDownList.DataBind();
                            altRMBNameDropDownList.Items.Insert(0, new ListItem(Application["altRMMBNameSelected"].ToString()));
                            reader.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_grade FROM masterbatch_master where mb_name = '" + Application["altRMMBNameSelected"].ToString() + "' AND mb_grade NOT IN('" + Application["altRMMBGradeSelected"].ToString() + "') ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRMBGradeDropDownList.DataSource = reader;
                            altRMBGradeDropDownList.DataBind();
                            altRMBGradeDropDownList.Items.Insert(0, new ListItem(Application["altRMMBGradeSelected"].ToString()));
                            reader.Close();
                        }

                        altRMBMfgTextBox.Text = Application["altRMMBMfgSelected"].ToString();
                        altRMBColorTextBox.Text = Application["altRMMBColorSelected"].ToString();
                        altRMBColorCodeTextBox.Text = Application["altRMMBColorCodeSelected"].ToString();
                        altRMBPercentageTextBox.Text = Application["altRMMBPercentageValue"].ToString();
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_name FROM masterbatch_master", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRMBNameDropDownList.DataSource = reader;
                            altRMBNameDropDownList.DataBind();
                            altRMBNameDropDownList.Items.Insert(0, new ListItem("Select Masterbatch", ""));
                            reader.Close();
                        }
                        altRMBNameDropDownList.Text = "";
                        altRMBGradeDropDownList.Text = "";
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
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_name FROM raw_material_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        rawMaterialDropDownList.DataSource = reader;
                        rawMaterialDropDownList.DataBind();
                        rawMaterialDropDownList.Items.Insert(0, new ListItem("Select Raw Material", ""));
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT material_name FROM raw_material_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        altRMDropDownList.DataSource = reader;
                        altRMDropDownList.DataBind();
                        altRMDropDownList.Items.Insert(0, new ListItem("Select Raw Material", ""));
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_name FROM masterbatch_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        mbNameDropDownList.DataSource = reader;
                        mbNameDropDownList.DataBind();
                        mbNameDropDownList.Items.Insert(0, new ListItem("Select Masterbatch", ""));
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT mb_name FROM masterbatch_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        altRMBNameDropDownList.DataSource = reader;
                        altRMBNameDropDownList.DataBind();
                        altRMBNameDropDownList.Items.Insert(0, new ListItem("Select Masterbatch", ""));
                        reader.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void masterBatchNameChanged(object sender, EventArgs e)
        {
            try
            {
                if (mbNameDropDownList.SelectedItem.Text == "Select Masterbatch")
                {
                    mbMfgTextBox.Text = "";
                    mbColorTextBox.Text = "";
                    mbColorCodeTextBox.Text = "";
                }
                else
                {
                    SqlConnection con = new SqlConnection(settings.ToString());
                    using (con)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT mb_grade FROM masterbatch_master WHERE mb_name= '" + mbNameDropDownList.SelectedItem.Value + "' ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            mbGradeDropDownList.DataSource = reader;
                            mbGradeDropDownList.DataBind();
                            reader.Close();
                            mbGradeDropDownList.Items.Insert(0, new ListItem("Select MasterBatch Grade", ""));
                        }
                    }
                    mbMfgTextBox.Text = "";
                    mbColorTextBox.Text = "";
                    mbColorCodeTextBox.Text = "";
                    mbPercentageTextBox.Text = "";
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void masterBatchGradeChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT mb_mfg,mb_color,mb_color_code FROM masterbatch_master WHERE mb_name= '" + mbNameDropDownList.SelectedItem.Value + "' AND mb_grade= '" + mbGradeDropDownList.SelectedItem.Text + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            mbMfgTextBox.Text = reader["mb_mfg"].ToString();
                            mbMfgTextBox.DataBind();

                            mbColorTextBox.Text = reader["mb_color"].ToString();
                            mbColorTextBox.DataBind();

                            mbColorCodeTextBox.Text = reader["mb_color_code"].ToString();
                            mbColorCodeTextBox.DataBind();
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void rawMaterialNameChanged(object sender, EventArgs e)
        {
            try
            {
                if (rawMaterialDropDownList.SelectedItem.Text == "Select Raw Material")
                {
                    rmMakeTextBox.Text = "";
                    colourTextBox.Text = "";
                }
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT material_grade FROM raw_material_master WHERE material_name= '" + rawMaterialDropDownList.SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        rmGradeDropDownList.DataSource = reader;
                        rmGradeDropDownList.DataBind();
                        reader.Close();
                        rmGradeDropDownList.Items.Insert(0, new ListItem("Select Raw Material Grade", ""));
                    }
                }
                rmMakeTextBox.Text = "";
                colourTextBox.Text = "";
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void rawMaterialGradeChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT material_make,material_color FROM raw_material_master WHERE material_name= '" + rawMaterialDropDownList.SelectedItem.Value + "' AND material_grade= '" + rmGradeDropDownList.SelectedItem.Text + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            rmMakeTextBox.Text = reader["material_make"].ToString();
                            rmMakeTextBox.DataBind();

                            colourTextBox.Text = reader["material_color"].ToString();
                            colourTextBox.DataBind();
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void altRawMaterialNameChanged(object sender, EventArgs e)
        {
            try
            {
                if (altRMDropDownList.SelectedItem.Text == "Select Raw Material")
                {
                    altRawMaterialMakeTextBox.Text = "";
                    altColourTextBox.Text = "";
                }
                else
                {

                    SqlConnection con = new SqlConnection(settings.ToString());
                    using (con)
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT material_grade FROM raw_material_master WHERE material_name= '" + altRMDropDownList.SelectedItem.Value + "' ", con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            altRawMaterialGradeDropDownList.DataSource = reader;
                            altRawMaterialGradeDropDownList.DataBind();
                            reader.Close();
                            altRawMaterialGradeDropDownList.Items.Insert(0, new ListItem("Select Raw Material Grade", ""));
                        }
                    }
                    altRawMaterialMakeTextBox.Text = "";
                    altColourTextBox.Text = "";
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void altRawMaterialGradeChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT material_make,material_color FROM raw_material_master WHERE material_name= '" + altRMDropDownList.SelectedItem.Value + "' AND material_grade= '" + altRawMaterialGradeDropDownList.SelectedItem.Text + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            altRawMaterialMakeTextBox.Text = reader["material_make"].ToString();
                            altRawMaterialMakeTextBox.DataBind();

                            altColourTextBox.Text = reader["material_color"].ToString();
                            altColourTextBox.DataBind();
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void altMasterBatchNameChanged(object sender, EventArgs e)
        {
            try
            {
                if (altRMBNameDropDownList.SelectedItem.Text == "Select Masterbatch")
                {
                    altRMBMfgTextBox.Text = "";
                    altRMBColorTextBox.Text = "";
                    altRMBColorCodeTextBox.Text = "";
                    altRMBPercentageTextBox.Text = "";
                }
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT mb_grade FROM masterbatch_master WHERE mb_name= '" + altRMBNameDropDownList.SelectedItem.Value + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        altRMBGradeDropDownList.DataSource = reader;
                        altRMBGradeDropDownList.DataBind();
                        reader.Close();
                        altRMBGradeDropDownList.Items.Insert(0, new ListItem("Select MasterBatch Grade", ""));
                    }
                }
                altRMBMfgTextBox.Text = "";
                altRMBColorTextBox.Text = "";
                altRMBColorCodeTextBox.Text = "";
                altRMBPercentageTextBox.Text = "";
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void altMasterBatchGradeChanged(object sender, EventArgs e)
        {
            try 
            { 
            SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT mb_mfg,mb_color,mb_color_code FROM masterbatch_master WHERE mb_name= '" + altRMBNameDropDownList.SelectedItem.Value + "' AND mb_grade= '" + altRMBGradeDropDownList.SelectedItem.Text + "' ", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            altRMBMfgTextBox.Text = reader["mb_mfg"].ToString();
                            altRMBMfgTextBox.DataBind();

                            altRMBColorTextBox.Text = reader["mb_color"].ToString();
                            altRMBColorTextBox.DataBind();

                            altRMBColorCodeTextBox.Text = reader["mb_color_code"].ToString();
                            altRMBColorCodeTextBox.DataBind();
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void NextPage_Click1(object sender, EventArgs e)
        {
            try
            {
                Application["rawMaterial"] = rawMaterialDropDownList.SelectedItem.Text;
                Application["rmGrade"] = rmGradeDropDownList.SelectedItem.Text;
                Application["rmMake"] = rmMakeTextBox.Text;
                Application["rmColor"] = colourTextBox.Text;
                Application["masterbatch"] = masterbatchDropDownList.SelectedItem.Text;
                Application["altRawMaterial"] = altRawMaterialDropDownList.SelectedItem.Text;
                Application["altMasterbatch"] = altRawMaterialMasterBatchDropDownList.SelectedItem.Text;
                Application["mbFlag"] = string.Empty;
                Application["altRmFlag"] = string.Empty;
                Application["altMBFlag"] = string.Empty;

                if (Application["masterbatch"].ToString() == "YES")
                {
                    Application["mbName"] = mbNameDropDownList.SelectedItem.Text.ToString();
                    Application["mbGrade"] = mbGradeDropDownList.SelectedItem.Text.ToString();
                    Application["mbMfg"] = mbMfgTextBox.Text;
                    Application["mbColor"] = mbColorTextBox.Text;
                    Application["mbColorCode"] = mbColorCodeTextBox.Text;
                    Application["mbPercentage"] = mbPercentageTextBox.Text;

                    if (Application["mbName"].ToString() != "Select Masterbatch" && Application["mbGrade"].ToString() != "Select MasterBatch Grade" && Application["mbMfg"].ToString() != null && Application["mbColor"].ToString() != null)
                    {
                        Application["mbFlag"] = "YES";
                    }
                    else
                    {
                        Application["mbFlag"] = "NO";
                    }
                }

                if (Application["altRawMaterial"].ToString() == "YES")
                {

                    Application["altRMName"] = altRMDropDownList.SelectedItem.Text;
                    Application["altRmGrade"] = altRawMaterialGradeDropDownList.SelectedItem.Text;
                    Application["altRmMake"] = altRawMaterialMakeTextBox.Text;
                    Application["altRmColor"] = altColourTextBox.Text;

                    if (Application["altRMName"].ToString() != "Select Raw Material" && Application["altRmGrade"].ToString() != "Select Raw Material Grade" && Application["altRmMake"] != null && Application["altRmColor"] != null)
                    {
                        Application["altRmFlag"] = "YES";
                    }
                    else
                    {
                        Application["altRmFlag"] = "NO";
                    }
                }
                else
                {
                    Application["altRmFlag"] = "NO";
                }

                if (Application["altMasterbatch"].ToString() == "YES")
                {
                    Application["altMasterbatchName"] = altRMBNameDropDownList.SelectedItem.Text;
                    Application["altMasterbatchGrade"] = altRMBGradeDropDownList.SelectedItem.Text;
                    Application["altMasterbatchMfg"] = altRMBMfgTextBox.Text;
                    Application["altMasterbatchColor"] = altRMBColorTextBox.Text;
                    Application["altMasterbatchColorCode"] = altRMBColorCodeTextBox.Text;
                    Application["altMasterbatchPercentage"] = altRMBPercentageTextBox.Text;

                    if (Application["altMasterbatchName"].ToString() != "Select Masterbatch" && Application["altMasterbatchGrade"].ToString() != "Select MasterBatch Grade" && Application["altMasterbatchMfg"] != null && Application["altMasterbatchColor"] != null)
                    {
                        Application["altMBFlag"] = "YES";
                    }
                    else
                    {
                        Application["altMBFlag"] = "NO";
                    }
                }
                else
                {
                    Application["altMBFlag"] = "NO";
                }

                //Check 

                if (Application["masterbatch"].ToString() == "YES" && Application["altRawMaterial"].ToString() == "NO")
                {
                    if (Application["mbFlag"].ToString() == "YES")
                    {
                        Response.Redirect("~/postOperationsPage.aspx");
                    }
                    else
                    {
                        mbNameLabel.Text = "Please enter Masterbatch fields.";
                        mbNameLabel.ForeColor = Color.Red;
                    }
                }
                else if (Application["masterbatch"].ToString() == "YES" && Application["altRawMaterial"].ToString() == "YES")
                {
                    if (Application["mbFlag"].ToString() == "YES" && Application["altRmFlag"].ToString() == "YES")
                    {
                        if (Application["altMasterbatch"].ToString() == "YES")
                        {
                            if (Application["altMBFlag"].ToString() == "YES")
                            {
                                Response.Redirect("~/postOperationsPage.aspx");
                            }
                            else
                            {
                                altRMBNameLabel.Text = "Please Enter Alt Masterbatch Fields.";
                                altRMBNameLabel.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            Response.Redirect("~/postOperationsPage.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Missing Fields.')", true);
                    }
                }
                else if (Application["masterbatch"].ToString() == "NO" && Application["altRawMaterial"].ToString() == "YES")
                {
                    if (Application["altRmFlag"].ToString() == "YES")
                    {
                        if (Application["altMasterbatch"].ToString() == "YES")
                        {
                            if (Application["altMBFlag"].ToString() == "YES")
                            {
                                Response.Redirect("~/postOperationsPage.aspx");
                            }
                            else
                            {
                                altRMBNameLabel.Text = "Please Enter Alt Masterbatch Fields.";
                                altRMBNameLabel.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            Response.Redirect("~/postOperationsPage.aspx");
                        }
                    }
                    else
                    {
                        altRMLabel.Text = "Please Enter Alt Raw Material Fields.";
                    }
                }
                else
                {
                    Response.Redirect("~/postOperationsPage.aspx");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Application["Duplicate"] = false;
            Response.Redirect("~/newPartsMaster.aspx");
            //backClick.Attributes.Add("onClick", "javascript:history.back(); return false;");
        }
    }
}