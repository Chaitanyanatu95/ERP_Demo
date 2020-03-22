using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class newAssembleMaster : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(settings.ToString());
            con.Open();

            if (!IsPostBack)
            {
                PopulateGridview();

                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT abbreviation FROM unit_of_measurement_master;", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        uomDropDownList.DataSource = reader;
                        uomDropDownList.DataBind();
                        reader.Close();
                        uomDropDownList.Items.Insert(0, new ListItem("Select Unit"));
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM assembly_master ORDER BY ID DESC", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["input"] = reader["assembly_no"];
                        }
                        reader.Close();
                        if (Application["input"] == null)
                            Application["input"] = 0;
                        int input = int.Parse(Regex.Replace(Application["input"].ToString(), "[^0-9]+", string.Empty));
                        assemblyNo.Text = "PBPAY#" + (input + 1);
                    }

                    var drop = (DropDownList)assemblyGridView.FooterRow.FindControl("partNameDropDownListFooter");
                    var qty = (TextBox)assemblyGridView.FooterRow.FindControl("txtQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    drop.Items.Insert(0, new ListItem("Select Part"));
                    drop.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)assemblyGridView.FooterRow.FindControl("assImgBtn");
                    img.Visible = false;
                }

                string postSelectQuery = "SELECT assembly_no FROM assembly_master where assembly_no= '" + assemblyNo.Text.Trim() + "'";
                SqlCommand selCmd = new SqlCommand(postSelectQuery, con);
                Application["assemblySelectData"] = "";
                SqlDataReader selReader = selCmd.ExecuteReader();
                while (selReader.Read())
                {
                    if (selReader["assembly_no"].ToString() != "")
                    {
                        Application["assemblySelectData"] = selReader["assembly_no"].ToString();
                    }
                }
                selReader.Close();
                if (Application["assemblySelectData"].ToString() == "" && Application["rowCommand"] is false)
                {
                    string postQuery = "DELETE FROM assembly_operation_details where assembly_id= '" + assemblyNo.Text.Trim() + "'";
                    SqlCommand cmmd = new SqlCommand(postQuery, con);
                    cmmd.ExecuteNonQuery();

                    string postQuery2 = "DELETE FROM assembly_operation_saved_details where assembly_id= '" + assemblyNo.Text.Trim() + "'";
                    SqlCommand cmmd2 = new SqlCommand(postQuery2, con);
                    cmmd2.ExecuteNonQuery();
                    Application["assemblySelectData"] = null;
                }
            }
            else
            {
                Application["rowCommand"] = false;
            }
            con.Close();
        }

        void PopulateGridview()
        {
            try
            {
                DataTable dtbl = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    

                    if (Application["editFlag"] is true)
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM assembly_operation_saved_details WHERE assembly_id = '"+Application["assemblyNo"].ToString()+"'", sqlCon);
                        sqlDa.Fill(dtbl);
                    }
                    else
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM assembly_operation_details WHERE assembly_id = '"+assemblyNo.Text.ToString()+"'", sqlCon);
                        sqlDa.Fill(dtbl);
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    assemblyGridView.DataSource = dtbl;
                    assemblyGridView.DataBind();
                }
                else
                {
                    dtbl.Rows.Add(dtbl.NewRow());
                    assemblyGridView.DataSource = dtbl;
                    assemblyGridView.DataBind();
                    assemblyGridView.Rows[0].Cells.Clear();
                    assemblyGridView.Rows[0].Cells.Add(new TableCell());
                    assemblyGridView.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                    //assemblyGridView.Rows[0].Cells[0].Text = "No Data Found ..!";
                    assemblyGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadValuesInController()
        {
            try
            {
                /************** ASSEMBLY OPERATION ****************/
                GridViewRow row = assemblyGridView.FooterRow;
                DataTable dtAssembly = new DataTable();
                dtAssembly.Columns.AddRange(new DataColumn[2] { new DataColumn("PART NAME"), new DataColumn("QTY") });
                Application["AssemblyOperation"] = dtAssembly;
                Application["tempAssNo"] = string.Empty;
                BindPostGrid();
                using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                {
                    sqlCon.Open();
                    if (Application["editFlag"] is true)
                    {
                        string query = "SELECT assembly_name FROM assembly_master where Id = '" + Application["assemblyId"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Application["tempAssNo"] = reader["assembly_name"];
                        }
                        reader.Close();
                        if (Application["tempAssNo"].ToString() != "")
                        {
                            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM assembly_operation_saved_details where assembly_id = '" + Application["assemblyNo"].ToString() + "'", sqlCon);
                            Application["tempAssNo"] = null;
                            sqlDa.Fill(dtAssembly);
                        }
                    }
                    else
                    {
                        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM assembly_operation_details where assembly_id = '" + assemblyNo.Text.Trim() + "'", sqlCon);
                        sqlDa.Fill(dtAssembly);
                        
                        lblErrorMessage.Text = "";
                    }
                }
                if (dtAssembly.Rows.Count > 0)
                {
                    BindPostGrid();
                    var drop = (DropDownList)assemblyGridView.FooterRow.FindControl("partNameDropDownListFooter");
                    var qty = (TextBox)assemblyGridView.FooterRow.FindControl("txtQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    drop.Items.Insert(0, new ListItem("Select Part"));
                    drop.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)assemblyGridView.FooterRow.FindControl("assImgBtn");
                    img.Visible = false;
                }
                else
                {
                    dtAssembly.Rows.Add(dtAssembly.NewRow());
                    BindPostGrid();
                    var drop = (DropDownList)assemblyGridView.FooterRow.FindControl("partNameDropDownListFooter");
                    var qty = (TextBox)assemblyGridView.FooterRow.FindControl("txtQuantityFooter");
                    qty.BackColor = Color.WhiteSmoke;
                    drop.Items.Insert(0, new ListItem("Select Part"));
                    drop.Items.Add(new ListItem("N/A", "-1"));
                    var img = (ImageButton)assemblyGridView.FooterRow.FindControl("assImgBtn");
                    img.Visible = false;
                    assemblyGridView.Rows[0].Cells.Clear();
                    assemblyGridView.Rows[0].Cells.Add(new TableCell());
                    assemblyGridView.Rows[0].Cells[0].ColumnSpan = dtAssembly.Columns.Count;
                    //postOperationGrid.Rows[0].Cells[0].Text = "No Data Found ..!";
                    assemblyGridView.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void LoadEditValuesInController()
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());
                using (con)
                {
                    con.Open();

                    String sqlquery = "SELECT DISTINCT * FROM assembly_master where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", (Application["assemblyId"]).ToString().Trim());
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            assemblyNo.Text = reader["assembly_no"].ToString();
                            assemblePartName.Text = reader["assembly_name"].ToString();
                            assWt.Text = reader["assembly_weight"].ToString();
                            lblAssemblySpec.Text = reader["assembly_file_upload"].ToString();
                            targetQtyTextBox.Text = reader["target_quantity"].ToString();
                            Application["uom"] = reader["uom"].ToString();
                        }
                        reader.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT abbreviation FROM unit_of_measurement_master where unit_of_measurement NOT IN('" + Application["uom"].ToString() + "')", con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        uomDropDownList.DataSource = reader;
                        uomDropDownList.DataBind();
                        uomDropDownList.Items.Remove(uomDropDownList.Items.FindByValue(Application["uom"].ToString()));
                        uomDropDownList.Items.Insert(0, new ListItem(Application["uom"].ToString()));
                        reader.Close();
                    }

                    var temp = Server.MapPath("~/UploadedFiles/Assembly/");
                    if (Directory.Exists(temp))
                    {
                        DirectoryInfo di = new DirectoryInfo(temp);

                        FileInfo[] files = di.GetFiles(Application["partNo"] + "_*.*");

                        foreach (FileInfo m in files)
                        {
                            assemblyFileLabel.Text += String.Format("<style> display:block; </style><br/> {0}", m.Name.ToString());
                            //btnAssemblySpecs.ImageUrl = "~/Images/cancel.png";
                            //"<asp:ImageButton ID = 'btnExtraFile' runat = 'server' ImageUrl = '~/Images/cancel.png' Height = '20' Width = '20' OnClick = 'btnExtraFile_Click' />"
                        }
                    }
                    con.Close();
                    LoadValuesInController();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void btnAssemblySpecs_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(settings.ToString());

                if (lblAssemblySpec.Text != null)
                {
                    if (Application["editFlag"] is true)
                    {
                        var path = Server.MapPath(lblAssemblySpec.Text);
                        //System.Diagnostics.Debug.WriteLine(path);
                        if (File.Exists(path))
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE assembly_master set assembly_file_upload = '' where Id = '" + Application["assemblyId"] + "'", con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                                File.Delete(Server.MapPath(lblAssemblySpec.Text));
                            }
                            lblAssemblySpec.Text = "";
                            con.Close();
                        }
                        else
                        {
                            lblAssemblySpec.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var quant = targetQtyTextBox.Text;
                var uom = uomDropDownList.SelectedItem.Text;
                if (quant != "" && int.Parse(quant) > 0 && uom != "Select Unit")
                {
                    targetQuantLbl.Text="";
                    Application["Duplicate"] = false;
                    if (assemblyFileUpload.HasFile)
                    {
                        assemblyFileUpload.SaveAs(Server.MapPath("~/UploadedFiles/Assembly/") + assemblyFileUpload.FileName);
                    }

                    SqlConnection con = new SqlConnection(settings.ToString());
                    con.Open();

                    //Delete rejection history from master
                    string query2 = "DELETE FROM assembly_operation_details;";
                    SqlCommand sqlCmd = new SqlCommand(query2, con);
                    sqlCmd.ExecuteNonQuery();

                    if (Application["editFlag"] is true)
                    {
                        string sqlqueryE = "SELECT assembly_name FROM assembly_master EXCEPT SELECT assembly_name FROM assembly_master where assembly_name = '" + Application["assemblyName"].ToString() + "'";
                        Application["queryD"] = sqlqueryE;
                    }
                    else
                    {
                        string sqlqueryN = "SELECT assembly_name FROM assembly_master";
                        Application["queryD"] = sqlqueryN;
                    }

                    using (SqlCommand cmmd = new SqlCommand(Application["queryD"].ToString(), con))
                    {
                        SqlDataReader reader = cmmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (assemblePartName.Text.ToLower().Trim() == reader["assembly_name"].ToString().ToLower().Trim())
                            {
                                Application["Duplicate"] = true;
                                break;
                            }
                            else
                            {
                                Application["Duplicate"] = false;
                            }
                        }
                        reader.Close();
                    }
                    if (Application["Duplicate"] is false && Application["editFlag"] is true)
                    {
                        string query = "UPDATE assembly_master SET assembly_no='" + assemblyNo.Text.ToString() + "',assembly_name='" + assemblePartName.Text.ToString() + "',uom = '" + uomDropDownList.SelectedItem.Text + "',assembly_weight = '" + assWt.Text + "',target_quantity = '" + targetQtyTextBox.Text + "',assembly_file_upload='" + assemblyFileUpload.FileName + "' WHERE Id='" + Application["assemblyId"] + "'";
                        SqlCommand cmd = new SqlCommand(query.ToString(), con);
                        cmd.ExecuteNonQuery();
                    }
                    else if (Application["Duplicate"] is false)
                    {
                        string query = "INSERT INTO assembly_master(assembly_no,assembly_name,uom,assembly_weight,target_quantity,assembly_file_upload)VALUES('" + assemblyNo.Text + "','" + assemblePartName.Text + "','" + uomDropDownList.SelectedItem.Text + "','" + assWt.Text + "','" + targetQtyTextBox.Text + "','" + assemblyFileUpload.FileName + "')";
                        SqlCommand cmd = new SqlCommand(query.ToString(), con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Assembly name already exists.')", true);
                    }
                    con.Close();
                    if (Application["Duplicate"] is false)
                    {
                        Application["Duplicate"] = null;
                        Application["assemblyId"] = null;
                        Application["assemblyNo"] = null;
                        Application["assemblyName"] = null;
                        Application["editFlag"] = null;
                        Response.Redirect("~/displayAssemble.aspx");
                    }
                }
                else
                {
                    targetQuantLbl.Text = "Please enter valid target quantity";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                string query = "";
                sqlCon.Open();
                query = "DELETE FROM assembly_operation_details WHERE assembly_id = @assId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@assId", assemblyNo.Text.ToString());
                sqlCmd.ExecuteNonQuery();

            }
                if (Application["editFlag"] is true)
                   Application["editFlag"] = null;

            Application["assemblyNo"] = null;
            Application["assemblyId"] = null;
            Application["assemblyName"] = null;
            Application["rowCommand"] = false;
            Response.Redirect("~/displayAssemble.aspx");
        }

        protected void BindPostGrid()
        {
            assemblyGridView.DataSource = (DataTable)Application["AssemblyOperation"];
            assemblyGridView.DataBind();
        }

        protected void assemblyGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    var uom = uomDropDownList.SelectedItem.Text;
                    if (uom != "Select Unit")
                    {
                        uomValidator.Text = "";
                        //string partName = Application["partName"].ToString();
                        GridViewRow row = assemblyGridView.FooterRow;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "'"+Application["PostOperation"].ToString()+"'", true);
                        using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
                        {
                            sqlCon.Open();
                            string query = "INSERT INTO assembly_operation_details (child_part,child_part_qty,assembly_id) OUTPUT INSERTED.Id VALUES (@partName,@quantity,@assemblyId)";
                            SqlCommand cmd = new SqlCommand(query, sqlCon);

                            cmd.Parameters.AddWithValue("@partName", (row.FindControl("partNameDropDownListFooter") as DropDownList).SelectedItem.Text.Trim());
                            cmd.Parameters.AddWithValue("@quantity", (row.FindControl("txtQuantityFooter") as TextBox).Text.Trim());
                            cmd.Parameters.AddWithValue("@assemblyId", assemblyNo.Text.ToString().Trim());
                            //cmd.ExecuteNonQuery();

                            var reservationId = (int)cmd.ExecuteScalar();

                            string query2 = "INSERT INTO assembly_operation_saved_details (child_part,child_part_qty,assembly_id,operation_id) VALUES (@partName,@quantity,@assemblyId,@operationId)";
                            SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
                            cmd2.Parameters.AddWithValue("@partName", (row.FindControl("partNameDropDownListFooter") as DropDownList).SelectedItem.Text.Trim());
                            cmd2.Parameters.AddWithValue("@quantity", (row.FindControl("txtQuantityFooter") as TextBox).Text.Trim());
                            cmd2.Parameters.AddWithValue("@assemblyId", assemblyNo.Text.ToString().Trim());
                            cmd2.Parameters.AddWithValue("@operationId", reservationId);
                            cmd2.ExecuteNonQuery();

                            sqlCon.Close();

                            if (Application["editFlag"] is true)
                            {
                                LoadEditValuesInController();
                            }
                            else
                            {
                                Application["rowCommand"] = true;
                                LoadValuesInController();
                            }

                            lblSuccessMessage.Text = "Record Added";
                            lblErrorMessage.Text = "";
                        }
                    }
                    else
                    {
                        uomValidator.Text = "Please select UOM";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void assemblyGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();

               /* GridViewRow row = assemblyGridView.Rows[e.RowIndex];
                Label assName = (Label)row.FindControl("partNameLabel");*/

               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ assemblyGridView.DataKeys[e.RowIndex].Value.ToString() + "')", true);

                if(Application["editFlag"] is true)
                {
                    string query = "DELETE FROM assembly_operation_saved_details WHERE id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(assemblyGridView.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                }

                string query2 = "DELETE FROM assembly_operation_saved_details WHERE operation_id = @id";
                SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
                sqlCmd2.Parameters.AddWithValue("@id", Convert.ToInt32(assemblyGridView.DataKeys[e.RowIndex].Value.ToString()));
                sqlCmd2.ExecuteNonQuery();

                string query3 = "DELETE FROM assembly_operation_details WHERE id = @id";
                SqlCommand sqlCmd3 = new SqlCommand(query3, sqlCon);
                sqlCmd3.Parameters.AddWithValue("@id", Convert.ToInt32(assemblyGridView.DataKeys[e.RowIndex].Value.ToString()));
                sqlCmd3.ExecuteNonQuery();

                if (Application["editFlag"] is true)
                {
                    LoadEditValuesInController();
                }
                else
                {
                    LoadValuesInController();
                }
                lblSuccessMessage.Text = "Selected Record Deleted";
                lblErrorMessage.Text = "";
            }
        }

        protected void partNameDropDownListFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drop = (DropDownList)assemblyGridView.FooterRow.FindControl("partNameDropDownListFooter");
            var T = (TextBox)assemblyGridView.FooterRow.FindControl("txtQuantityFooter");
            var img = (ImageButton)assemblyGridView.FooterRow.FindControl("assImgBtn");

            if (drop.SelectedItem.Text == "N/A")
            {
                T.ReadOnly = true;
                T.Text = "";
                T.BackColor = Color.WhiteSmoke;
                img.Visible = false;
                assWt.Text = "";
                double total = 0;
                SqlConnection con = new SqlConnection(settings.ToString());
                con.Open();
                foreach (GridViewRow i in assemblyGridView.Rows)
                {
                    Label partName = (Label)i.FindControl("partNameLabel");
                    Label qtyLabel = (Label)i.FindControl("qtyLabel");
                    string txtQty = qtyLabel.Text;
                    string txtPartWt = "";

                    string query = "SELECT part_weight FROM parts_master WHERE part_name = @PartName";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PartName", partName.Text);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        txtPartWt = rdr["part_weight"].ToString();
                    }
                    rdr.Close();

                    if (txtQty != null && txtPartWt != null)
                    {
                        if (int.TryParse(txtQty, out int qty) && double.TryParse(txtPartWt, out double wt))
                        {
                            total += wt * qty;
                            assWt.Text = total.ToString();
                        }
                    }
                }
                con.Close();
            }
            else if(drop.SelectedItem.Text == "Select Part")
            {
                T.ReadOnly = true;
                T.BackColor = Color.WhiteSmoke;
                img.Visible = false;
            }
            else
            {
                T.ReadOnly = false;
                T.BackColor = Color.White;
                img.Visible = true;
            }
        }
    }
}