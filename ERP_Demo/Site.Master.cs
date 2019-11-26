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
                if (Session["roleFullAccess"] != null || Session["roleTransactions"] != null || Session["roleReports"] != null || Session["roleSelectedAccess"] != null)
                {
                    Message.Text = "WELCOME:" + Session["username"].ToString();

                    if(Session["roleReports"].ToString() == "YES")
                    {
                        if(Menu1.Items.Count > 0)
                        {
                            Menu1.Items.Remove(Menu1.FindItem("Masters"));
                            Menu1.Items.Remove(Menu1.FindItem("Transaction"));
                        }
                    }
                    else if(Session["roleSelectedAccess"].ToString() == "YES")
                    {
                        if (Menu1.Items.Count > 0)
                        {
                            if(Session["roleAccess"].ToString().Trim() == "Product Category")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayFamily.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "UOM")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayUnitOfMeasurement.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Raw Material")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayRawMaterial.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Masterbatch")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayMasterBatch.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Post Operation")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayPostOperation.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Packaging")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayPackaging.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Rejection")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayRejection.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Shift")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayShift.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Machine")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayMachine.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Down Time Code")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayDownTimeCode.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Customer")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayCustomer.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Worker")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayWorker.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Vendor")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayVendor.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "Parts")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("Masters").ChildItems.Clear();
                                Menu1.FindItem("Masters").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayParts.aspx";
                            }
                        }
                    }
                    else if(Session["roleTransactions"].ToString() == "YES")
                    {
                        if(Menu1.Items.Count > 0)
                        {
                            Menu1.Items.Add(new MenuItem()
                            {
                                Text = "View Parts",
                                NavigateUrl = "~/displayPartsWorker.aspx"
                            });
                            Menu1.Items.Remove(Menu1.FindItem("Masters"));
                            Menu1.Items.Remove(Menu1.FindItem("Reports"));
                        }
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
                Application["editFlag"] = null;
                Session["roleFullAccess"] = null;
                Session["roleTransactions"] = null;
                Session["roleReports"] = null;
                Session["roleSelectedAccess"] = null;
                Session["roleAccess"] = null;
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}