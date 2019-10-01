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
                    Session["roleAdmin"] = Session["roleEditor"] = Session["roleWorker"] = Session["roleExtra"] = string.Empty;
                    Session["username"] = reader["worker_name"].ToString();
                    //System.Diagnostics.Debug.WriteLine(reader["rights"].ToString());
                    if (reader["Admin"].ToString() == "YES")
                        Session["roleAdmin"] = reader["Admin"].ToString();
                    if (reader["Editor"].ToString() == "YES")
                        Session["roleEditor"] = reader["Editor"].ToString();
                    if (reader["Worker"].ToString() == "YES")
                        Session["roleWorker"] = reader["Worker"].ToString();
                    if (reader["Extra"].ToString() == "YES")
                        Session["roleExtra"] = reader["Extra"].ToString();
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