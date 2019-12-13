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
                SqlConnection con = new SqlConnection(@"Data Source=pbplastics.database.windows.net;Initial Catalog=pbplasticserp;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM worker_master WHERE user_id='" + userlabel.Text + "' and user_password='" + passlabel.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["roleFullAccess"] = Session["roleTransactions"] = Session["roleReports"] = Session["roleSelectedAccess"] = Session["roleAccess"] = string.Empty;
                    Session["username"] = reader["worker_name"].ToString();
                    //System.Diagnostics.Debug.WriteLine(reader["rights"].ToString());
                    if (reader["Full_Access"].ToString() == "YES")
                        Session["roleFullAccess"] = reader["Full_Access"].ToString();
                    if (reader["Transactions"].ToString() == "YES")
                        Session["roleTransactions"] = reader["Transactions"].ToString();
                    if (reader["Reports"].ToString() == "YES")
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
                Response.Write(ex.Message);
            }
        }
    }
}