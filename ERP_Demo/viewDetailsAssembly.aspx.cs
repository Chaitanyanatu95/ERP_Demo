using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ERP_Demo
{
    public partial class viewDetailsAssembly : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadValuesInController();
            }
        }

        protected void LoadValuesInController()
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM assembly_master where id='" + Application["viewDetailsId"] + "'", sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    assNameLabel.Text = reader["assembly_name"].ToString().ToUpper();
                    textPartNo.Text = reader["assembly_no"].ToString();
                    textUOM.Text = reader["uom"].ToString();
                    textAssWeight.Text = reader["assembly_weight"].ToString();
                    textTargetQuant.Text = reader["target_quantity"].ToString();
                }
            }
        }

        protected void imageMouldSpecSheet_Click(object sender, ImageClickEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT assembly_file_upload FROM assembly_master where id='" + Application["viewDetailsId"] + "'", sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    System.Diagnostics.Process objDocProcess = new System.Diagnostics.Process();
                    objDocProcess.EnableRaisingEvents = false;
                    objDocProcess.StartInfo.FileName = @"" + Server.MapPath(reader["assembly_file_upload"].ToString()) + "";
                    objDocProcess.Start();
                }
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            /*if(Session["roleTransactions"].ToString() == "YES")
            {
                Response.Redirect("~/displayPartsWorker.aspx");
            }
            else if(Session["roleReports"].ToString() == "YES")
            {
                Response.Redirect("~/displayPartsWorker.aspx");
            }
            else
            {*/
            Application["assemblyNo"] = null;
            Application["viewDetailsId"] = null;
            Response.Redirect("~/displayAssemble.aspx");
            //}
        }

        protected void childPartDataGrid_Load(object sender, EventArgs e)
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM assembly_operation_saved_details where assembly_id='" + Application["assemblyNo"] + "'", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                childPartGridView.Visible = true;
                childPartGridView.DataSource = dtbl;
                childPartGridView.DataBind();
            }
            else
            {
                childPartGridView.Visible = false;
            }
        }
    }
}