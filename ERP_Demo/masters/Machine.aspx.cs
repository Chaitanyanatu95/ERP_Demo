using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo.masters
{
    public partial class Machine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM machines_master", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                machine.DataSource = dtbl;
                machine.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                machine.DataSource = dtbl;
                machine.DataBind();
                machine.Rows[0].Cells.Clear();
                machine.Rows[0].Cells.Add(new TableCell());
                machine.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                machine.Rows[0].Cells[0].Text = "No Data Found ..!";
                machine.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void machine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO machines_master (machine_no,cycle_time,machine_po_date) VALUES (@machine_no,@cycle_time,@machine_po_date)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@machine_no", (machine.FooterRow.FindControl("txtMachineNoFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@cycle_time", (machine.FooterRow.FindControl("txtCycleTimeFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@machine_po_date", (machine.FooterRow.FindControl("txtMachinePoDateFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview();
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void machine_RowEditing(object sender, GridViewEditEventArgs e)
        {
            machine.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void machine_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            machine.EditIndex = -1;
            PopulateGridview();
        }

        protected void machine_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "UPDATE machines_master SET machine_no=@machine_no,cycle_time=@cycle_time,machine_po_date=@machine_po_date WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@machine_no", (machine.Rows[e.RowIndex].FindControl("txtMachineNo") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@cycle_time", (machine.Rows[e.RowIndex].FindControl("txtCycleTime") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@machine_po_date", (machine.Rows[e.RowIndex].FindControl("txtMachinePoDate") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(machine.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    machine.EditIndex = -1;
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void machine_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-3F3SRHJ\SQLNEW;Initial Catalog=Pbplastics;Integrated Security=True"))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM machines_master WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(machine.DataKeys[e.RowIndex].Value.ToString()));
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
    }
}