using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace In2InGlobal.presentation.admin
{
    public partial class DisplayCSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string csvPath = Request.QueryString["csvfp"];
           DataTable csvTable = ConvertCSVtoDataTable(csvPath);
            grdCSVData.DataSource = csvTable;
            grdCSVData.DataBind();

        }
        public DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            try
            {
                StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath("uploadedfiles\\" + strFilePath));
                string[] headers = sr.ReadLine().Split(',');
               
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }
            catch(System.IO.FileNotFoundException ex)
            {
                grdCSVData.EmptyDataText = "Requested file not available or removed permanently.";
            }
            
            return dt;
        }
    }
}