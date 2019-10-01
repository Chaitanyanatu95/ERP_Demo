using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["roleAdmin"] != null || Session["roleEditor"] != null || Session["roleWorker"] != null || Session["roleExtra"] != null)
                {
                    Message.Text = "WELCOME:" + Session["username"].ToString();
                    if (Session["roleWorker"].ToString().Trim() == "YES" && Session["roleAdmin"].ToString().Trim() != "YES"
                        && Session["roleEditor"].ToString().Trim() != "YES" && Session["roleExtra"].ToString().Trim() != "YES")
                    {
                        Menu1.Items.Remove(Menu1.FindItem("Reports"));
                        Menu1.Items.Remove(Menu1.FindItem("Masters"));
                    }
                    else if (Session["roleEditor"].ToString().Trim() == "YES" && Session["roleAdmin"].ToString().Trim() != "YES" && Session["roleWorker"].ToString().Trim() != "YES" && Session["roleExtra"].ToString().Trim() != "YES")
                    {
                        Menu1.Items.Remove(Menu1.FindItem("Masters"));
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            if(e.Item.Text == "LogOut")
            {
                Session["username"] = null;
                Session["roleAdmin"] = null;
                Session["roleWorker"] = null;
                Session["roleEditor"] = null;
                Session["roleExtra"] = null;
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}