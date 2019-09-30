using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ERP_Demo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errlabel.Visible = false;
        }

        protected void loginbutton_click1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=pbplastics;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM worker_master WHERE user_id='" + userlabel.Text + "' and user_password='" + passlabel.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["username"] = reader["worker_name"].ToString();
                    //System.Diagnostics.Debug.WriteLine(reader["rights"].ToString());
                    if (reader["right1"].ToString() == "YES")
                        Session["roleAdmin"] = reader["right1"].ToString();
                    if (reader["right2"].ToString() == "YES")
                        Session["roleEditor"] = reader["right2"].ToString();
                    if (reader["right3"].ToString() == "YES")
                        Session["roleWorker"] = reader["right3"].ToString();
                    if (reader["right4"].ToString() == "YES")
                        Session["roleExtra"] = reader["right4"].ToString();
                    Response.Redirect("~/Default.aspx/");
                }
                else
                {
                    errlabel.Visible = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}