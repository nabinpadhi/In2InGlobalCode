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
            
        }

        private void LoadCSVData(string csvFName)
        {
            DataTable csvTable = new DataTable();
            try
            {
                csvTable = CSVReader.ReadCSVFile(HttpContext.Current.Server.MapPath("uploadedfiles\\" + csvFName), true);
                Session["csvTable"] = csvTable;
                foreach (DataColumn dc in csvTable.Columns)
                {
                    dc.ColumnName = dc.ColumnName.Replace(" ", "");

                }
                csvTable.AcceptChanges();
                grdCSVData.DataSource = csvTable.AsEnumerable().Skip(0).Take(1000).CopyToDataTable(); ;
                grdCSVData.DataBind();
                lblRecordCnt.Text = "Record Count :-" + csvTable.Rows.Count;
                ancDownload.HRef = "uploadedfiles\\" + csvFName;
            }
            catch(Exception ex)
            {
               
                lblRecordCnt.Text = ex.Message;
                ancDownload.InnerText = "";
              
            }
            //DataTable csvTable = CSVReader.ReadCSVFile(HttpContext.Current.Server.MapPath("uploadedfiles\\" + csvFName), true);
            //HttpContext.Current.Session["csvTable"] = csvTable;
            //grdCSVData.DataSource = csvTable;
            //grdCSVData.DataBind();
            //lblRecordCnt.Text = "Record Count :- " + csvTable.Rows.Count;
            //ancDownload.HRef = "uploadedfiles\\" + csvFName;
            //dataTable.AsEnumerable().Skip(100).Take(25);



        }

        protected void grdCSVData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCSVData.PageIndex = e.NewPageIndex;
            DataTable csvTable =(DataTable)Session["csvTable"];
            grdCSVData.DataSource = csvTable;
            grdCSVData.DataBind();
        }
        protected void grdCSVData_PreRender(object sender, EventArgs e)
        {

            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
            if (!IsPostBack)
            {
                string csvFName = Request.QueryString["csvfp"];

                LoadCSVData(csvFName);
            }

            if (grdCSVData.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grdCSVData.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grdCSVData.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }

        }
    }
}