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
                    Message.Text = "WELCOME:- " + Session["username"].ToString();

                    if(Session["roleReports"].ToString() == "YES")
                    {
                        if (Menu1.Items.Count > 0)
                        {
                            if (Session["roleTransactions"].ToString() == "NO")
                            {
                                Menu1.Items.Remove(Menu1.FindItem("TRANSACTION"));
                            }
                        }
                    }
                    if(Session["roleSelectedAccess"].ToString() == "YES")
                    {
                        //Menu1.Items.Remove(Menu1.FindItem("Masters"));

                        if (Menu1.Items.Count > 0)
                        {
                            if (Session["roleReports"].ToString().Trim() == "NO")
                            {
                                Menu1.Items.Remove(Menu1.FindItem("REPORTS"));
                            }

                            if (Session["roleAccess"].ToString().Trim() == "ASSEMBLY")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayAssemble.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "PRODUCT CATEGORY")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayFamily.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "UOM")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayUnitOfMeasurement.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "RAW MATERIAL")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayRawMaterial.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "MASTERBATCH")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayMasterBatch.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "POST OPERATION")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayPostOperation.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "PACKAGING")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayPackaging.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "REJECTION")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayRejection.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "SHIFT")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayShift.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "MACHINE")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayMachine.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "DOWN TIME CODE")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayDownTimeCode.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "CUSTOMER")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayCustomer.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "WORKER")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayWorker.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "VENDOR")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayVendor.aspx";
                            }
                            if (Session["roleAccess"].ToString().Trim() == "PARTS")
                            {
                                MenuItem temp = new MenuItem(Session["roleAccess"].ToString());
                                Menu1.FindItem("MASTERS").ChildItems.Clear();
                                Menu1.FindItem("MASTERS").ChildItems.Add(temp);
                                temp.NavigateUrl = "/displayParts.aspx";
                            }
                        }
                    }
                    if(Session["roleTransactions"].ToString() == "YES")
                    {
                        Menu1.Items.Remove(Menu1.FindItem("MASTERS"));

                        if (Menu1.Items.Count > 0)
                        {
                            if (Session["roleReports"].ToString().Trim() == "NO")
                            {
                                Menu1.Items.Remove(Menu1.FindItem("REPORTS"));
                            }
                            Menu1.Items.Add(new MenuItem()
                            {
                                Text = "VIEW PARTS",
                                NavigateUrl = "~/displayPartsWorker.aspx"
                            });
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            if(e.Item.Text == "LOGOUT")
            {
                Application["Duplicate"] = null;
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