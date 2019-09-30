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
                /*if (Session["role"] != null)
                {
                    Message.Text += Session["username"].ToString();
                    if (Session["roleWorker"].ToString().Trim() == "YES" && Session["roleAdmin"].ToString().Trim() != "YES"
                        && Session["roleEditor"].ToString().Trim() != "YES")
                    {
                        Menu1.Items.Remove(Menu1.FindItem("Reports"));
                        Menu1.Items.Remove(Menu1.FindItem("Masters"));
                    }
                    else if(Session["roleEditor"].ToString().Trim() == "YES" && Session["roleAdmin"].ToString().Trim() != "YES" && Session["roleWorker"].ToString().Trim() != "YES")
                    {
                        Menu1.Items.Remove(Menu1.FindItem("Masters"));
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }*/
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            if(e.Item.Text == "LogOut")
            {
                Response.Write("You Clicked"+ e.Item.Text);
                Session["username"] = null;
                Session["role"] = null;
                Response.Redirect("/Login.aspx");
            }
        }
    }
}