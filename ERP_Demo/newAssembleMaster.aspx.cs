using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newAssembleMaster : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(settings.ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT unit_of_measurement FROM unit_of_measurement_master;", con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                uomDropDownList.DataSource = reader;
                uomDropDownList.DataBind();
                reader.Close();
                uomDropDownList.Items.Insert(0, new ListItem("Select Unit"));
            }
            con.Close();
        }

        protected void countbtn_Click(object sender, EventArgs e)
        {
            if (countTextBox.Text != "")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DropDownList");
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();
                int count = Convert.ToInt32(countTextBox.Text);
                for (int i = 0; i < count; i++)
                {
                        dt.Rows.Add("");
                }
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();
            }
            else
            {
                lblErrorMessage.Text = "Please enter numeric value.";
            }
        }

        protected void calculateBtn_Click(object sender, EventArgs e)
        {
            assWt.Text = "";
            int total = 0;
            SqlConnection con = new SqlConnection(settings.ToString());
            con.Open();
            foreach (RepeaterItem i in Repeater1.Items)
            {
                DropDownList drop = (DropDownList)i.FindControl("DropDownListField");
                TextBox txtExample = (TextBox)i.FindControl("qtyTextBox");
                string txtQty = txtExample.Text;
                string txtPartWt = "";
                string query = "SELECT part_weight FROM parts_master WHERE part_name = @PartName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PartName", drop.SelectedItem.Text);
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    txtPartWt = rdr["part_weight"].ToString();
                }
                rdr.Close();

                if (txtQty != null && txtPartWt != null)
                {
                    if (int.TryParse(txtQty, out int qty) && int.TryParse(txtPartWt, out int wt))
                    {
                        total += wt * qty;
                        assWt.Text = total.ToString();
                    }
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Hello", true);

            con.Close();
        }
    }
}