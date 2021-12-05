<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using In2InGlobal.businesslogic;
using In2InGlobal.presentation.Tools;
using In2InGlobalBL;
using In2InGlobalBusinessEL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {

            HttpFileCollection files = context.Request.Files;
            // for (int i = 0; i < files.Count; i++)
            //{
            HttpPostedFile file = files[0];
            string fname = context.Server.MapPath("./MasterTemplate/" + file.FileName);
            if (CheckUploadedFileHaveOnlyHeader(file))
            {
                file.SaveAs(fname);
                //call the function to db entry
                 context.Response.ContentType = "text/csv";
                context.Response.Write("File Uploaded Successfully!");
                context.Response.End();
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Upload Failed : File is not in correct format.");
                context.Response.End();
            }
            //}
           
        }

    }

    private bool CheckUploadedFileHaveOnlyHeader(HttpPostedFile templateUploadFile)
    {
        bool _result = true;
        using (StreamReader uploadedFS = new StreamReader(templateUploadFile.InputStream))
        {
            TextReader uploaderFileTextReader = new StreamReader(uploadedFS.BaseStream);
            using (DataTable table = new CSVReader(uploaderFileTextReader).CreateDataTable(true))
            {

                if (table.Rows.Count > 0)
                {
                    _result= false;
                }
            }
        }
        return _result;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}  