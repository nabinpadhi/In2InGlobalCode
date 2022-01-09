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
    public partial class CompareDataTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
           if(!IsPostBack)
            {
                LoadData();
              
            }

            
        }

        private void LoadData()
        {
            
            try
            {
                DataTable _olddataTable=GetOldData();
                DataTable _newDatatable=GetNewData();
                DataTable _modifiedDataTable = Tools.CSVReader.LoadModifiedData(_newDatatable, _olddataTable);//CompareDatatable(_newDatatable, _olddataTable);
                grdOldData.DataSource = _olddataTable;
                grdOldData.DataBind();
                grdNewData.DataSource = _newDatatable;
                grdNewData.DataBind();
                grdDiffData.DataSource = _modifiedDataTable;
                grdDiffData.DataBind();

            }
            catch(Exception ex)
            {
                throw ex;
               
            }          
        }

      

        private DataTable GetNewData()
        {
            DataTable table2 = new DataTable("articletable");
            table2.Columns.Add("articleID");
            table2.Columns.Add("title");
            table2.Columns.Add("content");

            for(Int64 i = 1; i < 40010; i++)
            {
                DataRow row = table2.NewRow();
                row[0] = i;
                if (i == 4)
                {
                    row[1] = "article name " + i.ToString() +" modified";
                }
                else
                {
                    row[1] = "article name" + i.ToString();
                }
                row[2] = "article written by nabin" + i.ToString();
                table2.Rows.Add(row);
            }
            return table2;
        }

        private DataTable GetOldData()
        {
            DataTable table2 = new DataTable("articletable");
            table2.Columns.Add("articleID");
            table2.Columns.Add("title");
            table2.Columns.Add("content");

            for (Int64 i = 1; i < 40000; i++)
            {
                DataRow row = table2.NewRow();
                row[0] = i;
                row[1] = "article name"+i.ToString();
                row[2] = "article written by nabin"+i.ToString();
                table2.Rows.Add(row);
            }
            return table2;
        }

        private DataTable CompareDatatable(DataTable oldTable,DataTable newTable)
        {
            oldTable.TableName = "oldTable";
            newTable.TableName = "newTable";

            //Create Empty Table
            DataTable diffTable = new DataTable("Difference");

            try
            {
                //Must use a Dataset to make use of a DataRelation object
                using (DataSet ds = new DataSet())
                {
                    //Add tables
                    ds.Tables.AddRange(new DataTable[] { oldTable.Copy(), newTable.Copy() });

                    //Get Columns for DataRelation
                    DataColumn[] firstcolumns = new DataColumn[ds.Tables[0].Columns.Count];

                    for (int i = 0; i < firstcolumns.Length; i++)
                    {
                        firstcolumns[i] = ds.Tables[0].Columns[i];
                    }

                    DataColumn[] secondcolumns = new DataColumn[ds.Tables[1].Columns.Count];

                    for (int i = 0; i < secondcolumns.Length; i++)
                    {
                        secondcolumns[i] = ds.Tables[1].Columns[i];
                    }

                    //Create DataRelation
                    DataRelation r = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);

                    ds.Relations.Add(r);

                    //Create columns for return table
                    for (int i = 0; i < oldTable.Columns.Count; i++)
                    {
                        diffTable.Columns.Add(oldTable.Columns[i].ColumnName, oldTable.Columns[i].DataType);
                    }

                    //If First Row not in Second, Add to return table.
                    diffTable.BeginLoadData();

                    foreach (DataRow parentrow in ds.Tables[0].Rows)
                    {
                        DataRow[] childrows = parentrow.GetChildRows(r);
                        if (childrows == null || childrows.Length == 0)
                        {
                            diffTable.LoadDataRow(parentrow.ItemArray, true);
                        }                       
                    }

                    diffTable.EndLoadData();

                }
            }
            finally
            {

            }

            return diffTable;

        }
    }
}