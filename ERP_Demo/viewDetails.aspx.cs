using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_Demo
{
    public partial class viewDetails : System.Web.UI.Page
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["PbplasticsConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadValuesInController();
            }
        }

        protected void LoadValuesInController()
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM parts_master where id='" + Application["viewDetailsId"] + "'", sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    partNameLabel.Text = reader["part_name"].ToString().ToUpper();
                    partPhoto.ImageUrl = reader["part_photo"].ToString();
                    textPartNo.Text = reader["part_no"].ToString();
                    textCustomerName.Text = reader["customer_name"].ToString();
                    textCustomerPartNo.Text = reader["customer_part_no"].ToString();
                    textProductCategory.Text = reader["product_category"].ToString();
                    textMoldName.Text = reader["mold_name"].ToString();
                    textMoldMfgYear.Text = reader["mold_mfg_year"].ToString();
                    textMoldLife.Text = reader["mold_life"].ToString();
                    textNoOfCavities.Text = reader["no_of_cavities"].ToString();
                    textUOM.Text = reader["unit_of_measurement"].ToString();
                    textPartWeight.Text = reader["part_weight"].ToString();
                    textShotWeight.Text = reader["shot_weight"].ToString();
                    textProductionInPcs.Text = reader["production_in_pcs"].ToString();
                    textCycleTime.Text = reader["cycle_time"].ToString();
                    textJigFixReq.Text = reader["jig_fixture_req"].ToString();
                    dataRawMaterial.Text = reader["raw_material"].ToString();
                    dataRawMaterialGrade.Text = reader["rm_grade"].ToString();
                    dataRawMaterialColor.Text = reader["rm_color"].ToString();
                    dataRawMaterialMake.Text = reader["rm_make"].ToString();
                    dataMasterbatch.Text = reader["masterbatch"].ToString();
                    dataMasterbatchName.Text = reader["mb_name"].ToString();
                    dataMasterbatchGrade.Text = reader["mb_grade"].ToString();
                    dataMasterbatchMfg.Text = reader["mb_mfg"].ToString();
                    dataMasterbatchColor.Text = reader["mb_color"].ToString();
                    dataMasterbatchColorCode.Text = reader["mb_color_code"].ToString();
                    dataAltRawMaterial.Text = reader["alt_raw_material"].ToString();
                    dataAltRawMaterialName.Text = reader["alt_rm_name"].ToString();
                    dataAltRawMaterialGrade.Text = reader["alt_rm_grade"].ToString();
                    dataAltRawMaterialColor.Text = reader["alt_rm_color"].ToString();
                    dataAltRawMaterialMake.Text = reader["alt_rm_make"].ToString();
                    dataAltMasterbatch.Text = reader["alt_masterbatch"].ToString();
                    dataAltMasterbatchName.Text = reader["alt_mb_name"].ToString();
                    dataAltMasterbatchGrade.Text = reader["alt_mb_grade"].ToString();
                    dataAltMasterbatchMfg.Text = reader["alt_mb_mfg"].ToString();
                    dataAltMasterbatchColor.Text = reader["alt_mb_color"].ToString();
                    dataAltMasterbatchColorCode.Text = reader["alt_mb_color_code"].ToString();
                    dataPostOperation.Text = reader["post_operation_required"].ToString();
                    dataPackagingDetails.Text = reader["packaging_details_required"].ToString();
                }
            }
        }

        protected void imageMouldSpecSheet_Click(object sender, ImageClickEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(settings.ToString()))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT mold_spec_sheet FROM parts_master where id='" + Application["viewDetailsId"] + "'", sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    System.Diagnostics.Process objDocProcess = new System.Diagnostics.Process();
                    objDocProcess.EnableRaisingEvents = false;
                    objDocProcess.StartInfo.FileName = @""+Server.MapPath(reader["mold_spec_sheet"].ToString())+"";
                    objDocProcess.Start();
                }
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            if(Session["roleTransactions"].ToString() == "YES")
            {
                Response.Redirect("~/displayPartsWorker.aspx");
            }
            else if(Session["roleReports"].ToString() == "YES")
            {
                Response.Redirect("~/displayPartsWorker.aspx");
            }
            else
            {
                Response.Redirect("~/displayParts.aspx");
            }
        }
    }
}