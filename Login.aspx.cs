using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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
                //Server String
                //Server=tcp:pbplastics.database.windows.net,1433;Initial Catalog=pbplasticserp;Persist Security Info=False;User ID=pbplastics;Password=Pranav_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM worker_master WHERE user_id='" + userlabel.Text + "' and user_password='" + passlabel.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["roleFullAccess"] = Session["roleTransactions"] = Session["roleReports"] = Session["roleSelectedAccess"] = Session["roleAccess"] = string.Empty;
                    Session["username"] = reader["worker_name"].ToString();
                    //System.Diagnostics.Debug.WriteLine(reader["rights"].ToString());
                    Session["roleFullAccess"] = reader["Full_Access"].ToString();
                    Session["roleTransactions"] = reader["Transactions"].ToString();
                    Session["roleReports"] = reader["Reports"].ToString();
                    if (reader["Selected_Access"].ToString() == "YES")
                    {
                        Session["roleSelectedAccess"] = reader["Selected_Access"].ToString();
                        Session["roleAccess"] = reader["Access"].ToString();
                    }
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
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}