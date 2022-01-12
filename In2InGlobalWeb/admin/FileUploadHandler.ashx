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
using System.Text;
using System.Web.SessionState;
using System.Reflection;
using CsvReader;
public class FileUploadHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        try
        {

            string filePath = HttpContext.Current.Session["targetfolder"].ToString(); //"./MasterTemplate/";
            if (context.Request.Files.Count > 0)
            {
                string uploadedBy = HttpContext.Current.Session["UserEmail"].ToString();

                HttpPostedFile file = context.Request.Files[0];

                if (HttpContext.Current.Session["ForScreen"].ToString() == "TemplateManagement")
                {
                    StartTemplateManagementTask(context, file, filePath, uploadedBy);
                }
                else if (context.Request["ForScreen"] == "FileManagement")
                {
                    StartFileManagementTask(context, file, filePath, uploadedBy);
                }

            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("ShowException");
                context.Response.End();
            }
        }

    }

    private void StartTemplateManagementTask(HttpContext context, HttpPostedFile file, string filePath, string uploadedBy)
    {

        //HttpPostedFile csvfileChecker = file;
        if (CheckUploadedFileHaveOnlyHeader(file))
        {
            string filePathWithFileName = context.Server.MapPath(filePath + file.FileName);
            file.SaveAs(filePathWithFileName);
            string _templateTableScript = GenerateTemplateTableScript(file.FileName.Replace(".csv", ""), filePathWithFileName).ToString();

            SaveUploadMasterTemplateFile(filePath, file.FileName.Replace(".csv", ""), uploadedBy);

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

    }
    private StringBuilder GenerateTemplateTableScript(string templateName, string fileNameWithPath)
    {
        StringBuilder _tableScript = new StringBuilder();
        DataTable _uploadedTemplateDataTable = new DataTable(templateName);
        using (CsvReader.CsvReader _csvTableLoader = new CsvReader.CsvReader(new StreamReader(System.IO.File.OpenRead(fileNameWithPath)), true))
        {
            _uploadedTemplateDataTable.Load(_csvTableLoader);
        }

        _tableScript.AppendLine("CREATE TABLE IF NOT EXISTS dbo." + templateName);
        _tableScript.AppendLine("(");
        _tableScript.AppendLine("");
        _tableScript.AppendLine("id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 100000000000 CACHE 1 ) " + ",");
        _tableScript.AppendLine("project_name character varying(200) NOT NULL" + ",");
        _tableScript.AppendLine("user_email character varying(200) NOT NULL" + ",");
        _tableScript.AppendLine("uploaded_by character varying(200)" + ",");
        _tableScript.AppendLine("is_processed character varying(50)" + ",");

        int colIndex = 1;
        foreach (DataColumn _dc in _uploadedTemplateDataTable.Columns)
        {
            string _columnScriptRow = "";

            if (colIndex < _uploadedTemplateDataTable.Columns.Count)
            {
                _columnScriptRow = _dc.ColumnName.Replace(" / ", "_or_").Replace(" ", "_") + " " + _dc.DataType.ToString() + ",";
            }
            else
            {
                _columnScriptRow = _dc.ColumnName.Replace(" / ", "_or_").Replace(" ", "_") + " " + _dc.DataType.ToString();
            }
            _columnScriptRow = _columnScriptRow.ToLower().Replace("system.string", "character varying");

            _tableScript.AppendLine(_columnScriptRow);
            colIndex = colIndex + 1;

        }
        _tableScript.AppendLine(")");
        _tableScript.AppendLine("");
        _tableScript.AppendLine("" + " " + "TABLESPACE pg_default;");
        _tableScript.AppendLine("ALTER TABLE dbo." + templateName + " " + "OWNER to postgres;");

        CreateMasterTemplateTable(_tableScript.ToString());
        CrateMasterTemplateProcessedTable(templateName, fileNameWithPath);
        return _tableScript;
    }


    private void CrateMasterTemplateProcessedTable(string templateName, string fileNameWithPath)
    {
        StringBuilder _tableScript = new StringBuilder();
        DataTable _uploadedTemplateDataTable = new DataTable(templateName);
        using (CsvReader.CsvReader _csvTableLoader = new CsvReader.CsvReader(new StreamReader(System.IO.File.OpenRead(fileNameWithPath)), true))
        {
            _uploadedTemplateDataTable.Load(_csvTableLoader);
        }
        string TableName = templateName+"_" +"Processed";

        _tableScript.AppendLine("CREATE TABLE IF NOT EXISTS dbo." + TableName);
        _tableScript.AppendLine("(");
        _tableScript.AppendLine("");
        _tableScript.AppendLine("id bigint NOT NULL" + ",");
        _tableScript.AppendLine("project_name character varying(200) NOT NULL" + ",");
        _tableScript.AppendLine("user_email character varying(200) NOT NULL" + ",");
        _tableScript.AppendLine("uploaded_by character varying(200)" + ",");
        _tableScript.AppendLine("is_processed character varying(50)" + ",");

        int colIndex = 1;
        foreach (DataColumn _dc in _uploadedTemplateDataTable.Columns)
        {
            string _columnScriptRow = "";

            if (colIndex < _uploadedTemplateDataTable.Columns.Count)
            {
                _columnScriptRow = _dc.ColumnName.Replace(" / ", "_or_").Replace(" ", "_") + " " + _dc.DataType.ToString() + ",";
            }
            else
            {
                _columnScriptRow = _dc.ColumnName.Replace(" / ", "_or_").Replace(" ", "_") + " " + _dc.DataType.ToString();
            }
            _columnScriptRow = _columnScriptRow.ToLower().Replace("system.string", "character varying");

            _tableScript.AppendLine(_columnScriptRow);
            colIndex = colIndex + 1;

        }
        _tableScript.AppendLine(")");
        _tableScript.AppendLine("");
        _tableScript.AppendLine("" + " " + "TABLESPACE pg_default;");
        _tableScript.AppendLine("ALTER TABLE dbo." + TableName + " " + "OWNER to postgres;");
        CreateMasterTemplateProcesedTable(_tableScript.ToString());
    }

    private void CreateMasterTemplateProcesedTable(string createTable)
    {
        if (createTable != null)
        {
            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
            uploadTemplateBl.CreateTableForMasterTemplate(createTable);
        }
    }


    private void CreateMasterTemplateTable(string createTable)
    {
        if (createTable != null)
        {
            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
            uploadTemplateBl.CreateTableForMasterTemplate(createTable);
        }

    }



    private void StartFileManagementTask(HttpContext context, HttpPostedFile file, string filePath, string uploadedBy)
    {
        if (!CheckUploadedFileHaveOnlyHeader(file))
        {
            string projectName = context.Session["SelectedProjectName"].ToString();
            string fileName = file.FileName;

            fileName = fileName.Replace(".csv", "~" + uploadedBy.Replace(" ", "") + "~" + projectName + ".csv");
            string filePathWithFileName = context.Server.MapPath(filePath + fileName);

            if (!System.IO.File.Exists(filePathWithFileName))
            {
                file.SaveAs(filePathWithFileName);
                //call the function to db entry
                SaveUploadTemplateInformationInDB(fileName, uploadedBy, projectName, context, filePathWithFileName);

                context.Response.ContentType = "text/plain";
                context.Response.Write("File Uploaded Successfully.");
                context.Response.End();
            }
            else
            {
                if (!IsBothCSVFileDataAreSame(filePathWithFileName, file))
                {
                    string tempfileName = "";
                    int counter = 1;
                    while (System.IO.File.Exists(filePathWithFileName))
                    {
                        fileName = fileName.Replace(".csv", "");
                        tempfileName = fileName + "-" + "V-" + counter.ToString() + ".csv";
                        filePathWithFileName = filePath + tempfileName;
                        filePathWithFileName = context.Server.MapPath(filePathWithFileName);
                        counter++;
                    }
                    fileName = tempfileName;

                    file.SaveAs(context.Server.MapPath(System.IO.Path.Combine(filePath, fileName)));
                    SaveUploadTemplateInformationInDB(fileName, uploadedBy, projectName, context, filePathWithFileName);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File Uploaded Successfully.");
                    context.Response.End();
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Uploaded data already exists.");
                    context.Response.End();
                }
                //context.Response.ContentType = "text/plain";
                //context.Response.Write("File Uploaded Successfully.");
                //context.Response.End();

            }
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Upload Failed : File is not in correct format.");
            context.Response.End();
        }

    }

    private void SaveUploadTemplateInformationInDB(string fileName, string uploadBy, string projectName, HttpContext context, string filePathWithFileName)
    {
        UploadTemplateEntity templateEntity = new UploadTemplateEntity();


        if (projectName != null && fileName != null)
        {
            templateEntity.FileName = fileName;
            templateEntity.ProjectName = projectName;
            templateEntity.CreatedBy = uploadBy;
            templateEntity.RoleName = context.Session["UserRole"].ToString();
            templateEntity.UserEmail = context.Session["UserEmail"].ToString();
            templateEntity.Status = "Success";

            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
            uploadTemplateBl.SaveAssignedTemplate(templateEntity);
            string templateName = fileName.Replace(".csv", "");
            string masterTemplateName = HttpContext.Current.Session["TemplateName"].ToString();
            DataTable _uploadedTemplateDataTable = new DataTable(masterTemplateName);
            _uploadedTemplateDataTable = AddOptionalColumns(_uploadedTemplateDataTable);
            using (CsvReader.CsvReader _csvTableLoader = new CsvReader.CsvReader(new StreamReader(System.IO.File.OpenRead(filePathWithFileName)), true))
            {

                _uploadedTemplateDataTable.Load(_csvTableLoader);
            }
            foreach (DataColumn _dc in _uploadedTemplateDataTable.Columns)
            {
                _dc.ColumnName = _dc.ColumnName.Replace(" / ", "_or_").Replace(" ", "_");
            }
            templateEntity.FileName = masterTemplateName;

            _uploadedTemplateDataTable = UpdateOptionalColumnValue(_uploadedTemplateDataTable, templateEntity);
            //Table name will tell u where tp insert data;
            uploadTemplateBl.SaveUploadTemplate(_uploadedTemplateDataTable, templateEntity);
        }

    }
    private DataTable UpdateOptionalColumnValue(DataTable uploadedTemplateDataTable, UploadTemplateEntity templateEntity)
    {
        foreach (DataRow dr in uploadedTemplateDataTable.Rows)
        {
            dr["project_name"] = templateEntity.ProjectName;
            dr["user_email"] = templateEntity.UserEmail;
            dr["uploaded_by"] = templateEntity.UserEmail;
            dr["is_processed"] = false;
        }

        uploadedTemplateDataTable.AcceptChanges();
        return uploadedTemplateDataTable;
    }
    private DataTable AddOptionalColumns(DataTable uploadedTemplateDataTable)
    {
        uploadedTemplateDataTable.Columns.Add("project_name", typeof(string));
        uploadedTemplateDataTable.Columns.Add("user_email", typeof(string));
        uploadedTemplateDataTable.Columns.Add("uploaded_by", typeof(string));
        uploadedTemplateDataTable.Columns.Add("is_processed", typeof(string));

        uploadedTemplateDataTable.AcceptChanges();

        return uploadedTemplateDataTable;
    }

    private bool CheckUploadedFileHaveOnlyHeader(HttpPostedFile templateUploadFile)
    {
        bool _result = true;
        templateUploadFile.InputStream.Position = 0;
        /*using (CsvReader.CsvReader _csvTableLoader = new CsvReader.CsvReader(new StreamReader(templateUploadFile.InputStream), true))
        {
            _uploadedTemplateDataTable.Load(_csvTableLoader);
        }*/

        StreamReader uploadedFS = new StreamReader(templateUploadFile.InputStream);
        uploadedFS.BaseStream.Position = 0;
        TextReader uploaderFileTextReader = new StreamReader(uploadedFS.BaseStream);
        //(Stream)uploaderFileTextReader.BaseStream.Position = 0;
        DataTable _uploadedTemplateDataTable = new CSVReader(uploaderFileTextReader).CreateDataTable(true);
        if (_uploadedTemplateDataTable.Rows.Count > 0)
        {
            _result = false;
        }
        templateUploadFile.InputStream.Position = 0;
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

        if (fileName != null)
        {
            TemplateMasterEntity templateEntity = new TemplateMasterEntity();
            templateEntity.FileName = fileName;
            templateEntity.FilePath = filePath;
            templateEntity.CreatedBy = createdby;
            TemplateMasterBl templateMasterBl = new TemplateMasterBl();
            templateMasterBl.SaveUploadTemplateMaster(templateEntity);
        }

    }
    private bool IsBothCSVFileDataAreSame(string existingFileName, HttpPostedFile Uploadedfile)
    {
        bool _result = true;
        StreamReader fsOld = new StreamReader(existingFileName);
        string _existingData = fsOld.ReadToEnd();
        string _uploadedData = GetUploadedContent(Uploadedfile);
        if (_existingData != null && _uploadedData != null)
        {
            if (_existingData == _uploadedData)
            {
                _result = true;
            }
            else
            {
                _result = false;
            }
        }

        fsOld.Close();
        return _result;
    }

    private string GetUploadedContent(HttpPostedFile file)
    {
        int BUFFER_SIZE = file.ContentLength;
        int nBytesRead = 0;
        Byte[] Buffer = new Byte[BUFFER_SIZE];
        StringBuilder strUploadedContent = new StringBuilder("");
        file.InputStream.Position = 0;
        Stream theStream = file.InputStream;
        nBytesRead = theStream.Read(Buffer, 0, BUFFER_SIZE);

        while (0 != nBytesRead)
        {
            strUploadedContent.Append(Encoding.ASCII.GetString(Buffer, 0, nBytesRead));
            nBytesRead = theStream.Read(Buffer, 0, BUFFER_SIZE);
        }
        return strUploadedContent.ToString();
    }
    private string GetMasterTemplatesJSON()
    {
        DataSet dsUploadedTempFiles = new DataSet();
        TemplateMasterBl templateMasterBL = new TemplateMasterBl();
        dsUploadedTempFiles = templateMasterBL.PopulateUploadMasterTemplateName();
        string JSONresult = JsonConvert.SerializeObject(dsUploadedTempFiles.Tables[0]);
        return JSONresult;
    }

}  