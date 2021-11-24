using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GridUtils
/// </summary>
namespace InGlobal.presentation
{
    public class GridUtils
    {
        public GridUtils()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string OnColSortSelection(DataGrid oGrid, DataGridSortCommandEventArgs e)
        {

            foreach (DataGridColumn oCol in oGrid.Columns)
            {

                StringBuilder SbImage = new StringBuilder();
                StringBuilder Sbimagedown = new StringBuilder();
                SbImage.Append("</a><span>&nbsp;&nbsp;<img src='../images/uparrow1.gif'></span><a>");
                Sbimagedown.Append("</a><span>&nbsp;&nbsp;<img src='../images/downarrow1.gif'></span><a>");
                // Find the right column
                if (e.SortExpression.ToLower().CompareTo(oCol.SortExpression.ToLower()) == 0)
                {
                    oCol.HeaderText = oCol.HeaderText.Replace(SbImage.ToString(), "").Replace(Sbimagedown.ToString(), "");
                    if (e.SortExpression.IndexOf(" ASC") > 1)
                    {
                        oCol.SortExpression = e.SortExpression.Replace(" ASC", " DESC");
                        oCol.HeaderText = oCol.HeaderText + Sbimagedown;
                        return oCol.SortExpression;
                    }
                    else if (e.SortExpression.IndexOf(" DESC") > 1)
                    {
                        oCol.SortExpression = e.SortExpression.Replace(" DESC", "");
                        return oCol.SortExpression;
                    }
                    else
                    {
                        oCol.SortExpression = e.SortExpression + " ASC";
                        oCol.HeaderText = oCol.HeaderText + SbImage;
                        return oCol.SortExpression;
                    }
                }
            }

            return "";
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
            catch (System.IO.FileNotFoundException ex)
            {
                throw (ex);
            }

            return dt;
        }
    }
}