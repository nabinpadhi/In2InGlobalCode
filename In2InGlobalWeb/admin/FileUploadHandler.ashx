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
using Newtonsoft.Json;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string filePath = "./MasterTemplate/";
        if (context.Request.Files.Count > 0)
        {
            string uploadedBy = context.Request.QueryString["upb"].ToString();
            HttpFileCollection files = context.Request.Files;
            // for (int i = 0; i < files.Count; i++)
            //{
            HttpPostedFile file = files[0];
            string filePathWithFileName = context.Server.MapPath(filePath + file.FileName);
            if (CheckUploadedFileHaveOnlyHeader(file))
            {
                file.SaveAs(filePathWithFileName);
                //call the function to db entry

                SaveUploadMasterTemplateFile(filePath, file.FileName.Replace(".csv",""),uploadedBy);

                context.Response.ContentType = "text/plain";
                context.Response.Write(GetMasterTemplatesJSON());
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
                    _result = false;
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


    private void SaveUploadMasterTemplateFile(string filePath, string fileName, string createdby)
    {
        try
        {
            if (fileName != null)
            {
                TemplateMasterEntity templateEntity = new TemplateMasterEntity();
                templateEntity.FileName = fileName;
                templateEntity.FilePath = filePath;
                templateEntity.CreatedBy = createdby ;
                TemplateMasterBl templateMasterBl = new TemplateMasterBl();
                templateMasterBl.SaveUploadTemplateMaster(templateEntity);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

 private string GetMasterTemplatesJSON()
    {
            DataSet dsloadTemplare = new DataSet();
            TemplateMasterBl templateMasterBL = new TemplateMasterBl();
            dsloadTemplare = templateMasterBL.PopulateUploadMasterTemplateName();            
             string JSONresult = JsonConvert.SerializeObject(dsloadTemplare.Tables[0]);
            return JSONresult;
        }
   

}  