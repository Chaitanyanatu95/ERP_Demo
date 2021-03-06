﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class displayPostOperation : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
                Application["editFlag"] = false;
            }
        }

        void PopulateGridview()
        {
            try
            {
                DataTable dtbl = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM post_operation_master order by type", sqlCon);
                    sqlDa.Fill(dtbl);
                }
                if (dtbl.Rows.Count > 0)
                {
                    postOperationGridView.DataSource = dtbl;
                    postOperationGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    postOperationGridView.DataSource = dtbl;
                    postOperationGridView.DataBind();
                    postOperationGridView.Rows[0].Cells.Clear();
                    postOperationGridView.Rows[0].Cells.Add(new TableCell());
                    postOperationGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    postOperationGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    postOperationGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void postOperationGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string temp;
            postOperationGridView.EditIndex = e.NewEditIndex;
            //temp = customerGridView.Rows[0].Cells[0].Text;
            PopulateGridview();
        }

        protected void postOperationGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            postOperationGridView.EditIndex = -1;
            PopulateGridview();
        }

        protected void postOperationGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM post_operation_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(postOperationGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void postOperationButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newPostOperationMaster.aspx");
        }

        protected void postOperationGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "", true);
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Application["postOperationId"] = commandArgs[0];
                    bool editFlag = true;
                    Application["editFlag"] = editFlag;
                    Response.Redirect("~/newPostOperationMaster.aspx/");
                }
            }
            catch(Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
        protected void searchButton_Click(object sender, EventArgs e)
        {
            this.searchPO();
        }

        protected void searchPO()
        {
            using (SqlConnection con = new SqlConnection(settings.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM post_operation_master";
                    if (!string.IsNullOrEmpty(searchTextBox.Text.Trim()))
                    {
                        sql += " WHERE type LIKE '%' + @type + '%'";
                        cmd.Parameters.AddWithValue("@type", searchTextBox.Text.Trim());
                    }
                    sql += " EXCEPT SELECT * FROM post_operation_master WHERE type = 'N/A' EXCEPT SELECT * FROM post_operation_master WHERE id = 1";
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        postOperationGridView.DataSource = dt;
                        postOperationGridView.DataBind();
                    }
                }
            }
        }
        protected void postOperationGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            postOperationGridView.PageIndex = e.NewPageIndex;
            PopulateGridview();
        }
    }
}