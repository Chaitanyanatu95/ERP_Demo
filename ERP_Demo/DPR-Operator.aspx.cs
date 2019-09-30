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

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if ((Convert.ToInt32(Application["cycleTime"]) != 0) && (Convert.ToInt32(Application["noOfCavities"]) != 0) && (Convert.ToInt32(Application["shiftTime"]) != 0))
                {
                    calculateExpectedQuantity();
                }

                if (!String.IsNullOrEmpty(noShotsTextBox.Text) && !String.IsNullOrEmpty(rejectionPCSTextBox.Text))
                {
                    calculateActualQuantity();
                }

                if (!String.IsNullOrEmpty(downTimeTextBox.Text) && !String.IsNullOrEmpty(actQuantityTextBox.Text) && !String.IsNullOrEmpty(expQuantityTextBox.Text))
                {
                    calculateEfficiency();
                }
            }
        }

        protected void LoadValuesInControlls()
        {
            /*if (Session["username"] is null)
            {
                Response.Redirect("~/Login.aspx/");
            }*/
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT part_name FROM parts_master", con))
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

                using (SqlCommand cmd = new SqlCommand("SELECT down_time_code FROM down_time_master", con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    downTimeCodeDropDownList.DataSource = reader;
                    downTimeCodeDropDownList.DataBind();
                    reader.Close();
                    downTimeCodeDropDownList.Items.Insert(0, new ListItem("Select Machine Used", ""));
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
                    reader.Close();
                }
            }
        }

        /*protected void materialGradeChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
            using (con)
            {
                con.Open();
                using (SqlCommand cmd2 = new SqlCommand("SELECT material_qty FROM raw_material_master where material_grade = '" + materialGradeDropDownList.SelectedItem.Value + "'", con))
                {
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["oldQuantity"] = int.Parse(reader["material_qty"].ToString());
                        materialQtyLabel.Text = "Quantity Available In Stock:- " + Application["oldQuantity"].ToString();
                    }
                    reader.Close();
                }
            }
        }*/

        /*protected void materialQtyTextBox_changed(object sender, EventArgs e)
        {
            Application["newQty"] = int.Parse(materialQtyTextBox.Text);
            if (int.Parse(Application["oldQuantity"].ToString()) < 200)
            {
                lblErrorMessage.Text = "STOCK QTY = " + Application["oldQuantity"].ToString() + "(STOCK QTY BELOW THRESHOLD.(200))";
                materialQtyTextBox.Text = "";
                lblSuccessMessage.Text = "";
                materialQtyTextBox.ReadOnly = true;
            }
            else if (int.Parse(Application["newQty"].ToString()) > int.Parse(Application["oldQuantity"].ToString()))
            {
                lblErrorMessage.Text = "Quantity entered more than the stock quantity..! please enter again!";
                materialQtyTextBox.Text = "";
                lblSuccessMessage.Text = "";
            }
            else if((int.Parse(Application["oldQuantity"].ToString()) - int.Parse(Application["newQty"].ToString())) < 200)
            {
                lblErrorMessage.Text = "Quantity entered will make stock quantity get below threshold..! please enter again!";
                materialQtyTextBox.Text = "";
                lblSuccessMessage.Text = "";
            }
            else
            {
                lblErrorMessage.Text = "";
                lblSuccessMessage.Text = "Accepted Value";
            }
        }*/

        protected void calculateExpectedQuantity()
        {
            expQuantityTextBox.Text = Convert.ToString((3600 / Convert.ToInt32(Application["cycleTime"])) * (Convert.ToInt32(Application["noOfCavities"]) * Convert.ToInt32(Application["shiftTime"]) * 0.9));
            System.Diagnostics.Debug.WriteLine(expQuantityTextBox.Text, "Exp. Qty");
        }

        protected void calculateActualQuantity()
        {
            actQuantityTextBox.Text = Convert.ToString(((int.Parse(noShotsTextBox.Text) * Convert.ToInt32(Application["noOfCavities"])) - int.Parse(rejectionPCSTextBox.Text)));
            System.Diagnostics.Debug.WriteLine(actQuantityTextBox.Text, "Act. Qty");
        }

        protected void calculateEfficiency()
        {
            System.Diagnostics.Debug.WriteLine(actQuantityTextBox.Text, "Actual Qty");
            System.Diagnostics.Debug.WriteLine(downTimeTextBox.Text, "DownTime");
            System.Diagnostics.Debug.WriteLine(Convert.ToInt32(Application["cycleTime"]));
            System.Diagnostics.Debug.WriteLine(Convert.ToInt32(Application["noOfCavities"]));
            System.Diagnostics.Debug.WriteLine(expQuantityTextBox.Text, "Exp Qty");
            efficiencyTextBox.Text = Convert.ToString(Math.Round(((float.Parse(actQuantityTextBox.Text) + ((float.Parse(downTimeTextBox.Text) * 3600) / Convert.ToDouble(Application["cycleTime"])) * Convert.ToDouble(Application["noOfCavities"])) / float.Parse(expQuantityTextBox.Text)) * 100, 2));
            System.Diagnostics.Debug.WriteLine(efficiencyTextBox.Text, "Efficiency");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application["cycleTime"].ToString() == "" || Application["noOfCavities"].ToString() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Data Properly, Missing Data..!')", true);
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO production(worker_name,part_name,material_grade,machine_no,shift_details,exp_qty,no_of_shots,rejection_pcs,rejection_kgs,act_qty,downtime_hrs,down_time_code,efficiency,date)VALUES('" + Session["username"].ToString() + "','" + partNameDropDownList.SelectedItem.Text + "','" + materialGradeDropDownList.SelectedItem.Text + "','" + machineUsedDropDownList.SelectedItem.Text + "','" + shiftDetailsDropDownList.SelectedItem.Text + "','" + expQuantityTextBox.Text + "','" + noShotsTextBox.Text + "','" + rejectionPCSTextBox.Text + "','" + rejectionKGSTextBox.Text + "','" + actQuantityTextBox.Text + "','" + downTimeTextBox.Text + "','" + downTimeCodeDropDownList.SelectedItem.Text + "','" + efficiencyTextBox.Text + "','" + dateSelection.Value + "')", con);
                    cmd.ExecuteNonQuery();

                    /*if (materialQtyTextBox.Text != null)
                    {
                        string command = "";
                        command = "UPDATE raw_material_master SET material_qty = '" + (int.Parse(Application["oldQuantity"].ToString()) - int.Parse(Application["newQty"].ToString())).ToString() + "' where material_grade = '" + materialGradeDropDownList.SelectedItem.Text + "'";
                        SqlCommand cmd2 = new SqlCommand(command, con);
                        cmd2.ExecuteNonQuery();
                        con.Close();
                    }*/

                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
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