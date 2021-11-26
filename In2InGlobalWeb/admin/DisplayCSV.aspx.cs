using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using In2InGlobal.presentation.Tools;
using System.Web.Services;
using Newtonsoft.Json;

namespace In2InGlobal.presentation.admin
{
    public partial class DisplayCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //DataTable csvTable = ConvertCSVtoDataTable(csvPath);
            if (!IsPostBack)
            {
                string csvFName = Request.QueryString["csvfp"];
                hdnQueryStringValue.Value = csvFName;
               // LoadCSVData(csvFName);
            }
        }

        [WebMethod(EnableSession = true)]
        public static string LoadCSVData(string csvFName)
        {
            try
            {
                DataTable csvTable = CSVReader.ReadCSVFile(HttpContext.Current.Server.MapPath("uploadedfiles\\" + csvFName), true);
                //HttpContext.Current.Session["csvTable"] = csvTable;
                /* grdCSVData.DataSource = csvTable;
                 grdCSVData.DataBind();
                 lblRecordCnt.Text = "Record Count :- " + csvTable.Rows.Count;
                 ancDownload.HRef = "uploadedfiles\\" + csvFName;*/
                string json = JsonConvert.SerializeObject(csvTable);
                return json;
            }
            catch (Exception)
            {
                //grdCSVData.EmptyDataText = "Requested file is invalid csv file.";
                //grdCSVData.DataSource = new DataTable();
                //grdCSVData.DataBind();
                return "Requested file is invalid csv file.";
            }
            return "Done!";
            //DataTable csvTable = CSVReader.ReadCSVFile(HttpContext.Current.Server.MapPath("uploadedfiles\\" + csvFName), true);
            //HttpContext.Current.Session["csvTable"] = csvTable;
            //grdCSVData.DataSource = csvTable;
            //grdCSVData.DataBind();
            //lblRecordCnt.Text = "Record Count :- " + csvTable.Rows.Count;
            //ancDownload.HRef = "uploadedfiles\\" + csvFName;

        }

      

        protected void grdCSVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCSVData.PageIndex = e.NewPageIndex;
            DataTable csvTable =(DataTable)Session["csvTable"];
            grdCSVData.DataSource = csvTable;
            grdCSVData.DataBind();
        }

    }
}