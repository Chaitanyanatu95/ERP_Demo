using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ERP_Demo
{
    public partial class Dpr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadValuesInControlls();
            }
        }

        protected void LoadValuesInControlls()
        {
            if (Session["username"] is null)
            {
                Response.Redirect("~/Login.aspx/");
            }
            else
            {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                using (con)
                {
                    workerNameTextBox.Text = Session["username"].ToString();
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT part_name FROM parts_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        partNameDropDownList.DataSource = reader;
                        partNameDropDownList.DataBind();
                        reader.Close();
                        partNameDropDownList.Items.Insert(0, new ListItem("Select Part Name", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT shift_time FROM shift_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        shiftDetailsDropDownList.DataSource = reader;
                        shiftDetailsDropDownList.DataBind();
                        reader.Close();
                        shiftDetailsDropDownList.Items.Insert(0, new ListItem("Select Shift Details", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT machine_no FROM machine_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        machineUsedDropDownList.DataSource = reader;
                        machineUsedDropDownList.DataBind();
                        reader.Close();
                        machineUsedDropDownList.Items.Insert(0, new ListItem("Select Machine Used", ""));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT down_time_type FROM down_time_master", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        downTimeCodeDropDownList.DataSource = reader;
                        downTimeCodeDropDownList.DataBind();
                        reader.Close();
                        downTimeCodeDropDownList.Items.Insert(0, new ListItem("Select Down Time Type", ""));
                    }
                }
            }
        }

        protected void machineUsedChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT cycle_time FROM parts_master WHERE part_name= '" + partNameDropDownList.SelectedItem.Value + "' ", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["cycleTime"] = reader["cycle_time"].ToString();
                    }
                    reader.Close();
                }
            }
        }

        protected void partNameChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT no_of_cavities FROM parts_master WHERE part_name= '" + partNameDropDownList.SelectedItem.Value + "' ", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["noOfCavities"] = reader["no_of_cavities"].ToString();
                    }
                    reader.Close();
                }
                using (SqlCommand cmd2 = new SqlCommand("SELECT rm_grade FROM parts_master where part_name = '" + partNameDropDownList.SelectedItem.Value + "'", con))
                {
                    SqlDataReader reader = cmd2.ExecuteReader();
                    materialGradeDropDownList.DataSource = reader;
                    materialGradeDropDownList.DataBind();
                    reader.Close();
                    materialGradeDropDownList.Items.Insert(0, new ListItem("Select Material Grade", ""));
                }

            }
        }

        protected void shiftDetailsChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT hours FROM shift_master WHERE shift_time= '" + shiftDetailsDropDownList.SelectedItem.Value + "' ", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["shiftTime"] = reader["hours"].ToString();
                    }
                    calculateExpectedQuantity();
                    reader.Close();
                }
            }
        }

        protected void calculateExpectedQuantity()
        {
            string cycleTime = Application["cycleTime"].ToString();
            string noOfCavities = Application["noOfCavities"].ToString();
            string shiftTime = Application["shiftTime"].ToString();
            expQuantityTextBox.Text = Math.Ceiling((3600 / double.Parse(cycleTime)) * (double.Parse(noOfCavities) * double.Parse(shiftTime)) * 0.9).ToString();
        }

        protected void calculateActualQuantity()
        {
            actQuantityTextBox.Text = Convert.ToString(((int.Parse(noShotsTextBox.Text) * Convert.ToInt32(Application["noOfCavities"])) - int.Parse(rejectionPCSTextBox.Text)));
        }

        protected void calculateEfficiency()
        {
            efficiencyTextBox.Text = Convert.ToString(Math.Round(((float.Parse(actQuantityTextBox.Text) + ((float.Parse(downTimeTextBox.Text) * (3600 / Convert.ToDouble(Application["cycleTime"])))*0.9) * Convert.ToDouble(Application["noOfCavities"])) / float.Parse(expQuantityTextBox.Text)) * 100, 2));
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application["noOfCavities"].ToString() == "" || Application["cycleTime"].ToString() == "" || noShotsTextBox.Text == "" || actQuantityTextBox.Text == "" || machineUsedDropDownList.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO production(worker_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots,rejection_pcs,rejection_kgs,act_qty,downtime_hrs,down_time_code,efficiency,date_dpr)VALUES('" + Session["username"].ToString() + "','" + partNameDropDownList.SelectedItem.Text + "','" + materialGradeDropDownList.SelectedItem.Text + "','" + machineUsedDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "','" + expQuantityTextBox.Text + "','" + noShotsTextBox.Text + "','" + rejectionPCSTextBox.Text + "','" + rejectionKGSTextBox.Text + "','" + actQuantityTextBox.Text + "','" + downTimeTextBox.Text + "','" + downTimeCodeDropDownList.SelectedItem.Text + "','" + efficiencyTextBox.Text + "','" + dateSelectionTextBox.Value + "')", con);
                    cmd.ExecuteNonQuery();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void noShotsEndTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(noShotsStartTextBox.Text.ToString().Trim()) > int.Parse(noShotsEndTextBox.Text.ToString().Trim()) || int.Parse(noShotsEndTextBox.Text.ToString().Trim()) == '0' || int.Parse(noShotsStartTextBox.Text.ToString().Trim()) == '0')
                {
                    noShotsEndTextBox.Text = string.Empty;
                    noShotsTextBox.Text = string.Empty;
                    validationShots.Text = "Value cannot be 0 or less starting value of shots";
                }
                else
                {
                    noShotsTextBox.Text = (int.Parse(noShotsEndTextBox.Text.ToString().Trim()) - int.Parse(noShotsStartTextBox.Text.ToString().Trim())).ToString();
                    validationShots.Text = string.Empty;
                }
            }
            catch(Exception ne)
            {
              ne.Message.ToString();
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (Application["editFlag"] is true)
                Application["editFlag"] = null;
            Response.Redirect("~/Default.aspx");
        }

        protected void downTimeCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (downTimeCodeDropDownList.SelectedItem.Text == "N/A")
            {
                downTimeTextBox.ReadOnly = true;
                downTimeTextBox.Text = "0";
                if (actQuantityTextBox.Text != "" && noShotsEndTextBox.Text != "" && expQuantityTextBox.Text != "")
                    calculateEfficiency();
            }
            else
            {
                downTimeTextBox.ReadOnly = false;
            }
        }

        protected void downTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            if(actQuantityTextBox.Text != "" && noShotsEndTextBox.Text != "" && expQuantityTextBox.Text!= "")
                calculateEfficiency();
        }

        protected void rejectionPCSTextBox_TextChanged(object sender, EventArgs e)
        {
            if(noShotsEndTextBox.Text !="" && rejectionPCSTextBox.Text != "")
            {
                calculateActualQuantity();
            }
        }
    }
}