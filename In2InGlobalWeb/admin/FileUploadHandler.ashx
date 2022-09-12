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
using System.Text.RegularExpressions;
using System.Web.SessionState;
using System.Reflection;
using CsvReader;
public class FileUploadHandler : IHttpHandler, IRequiresSessionState
{


    private void DeleteAnalysisDataFromDB(string uploadBy, string projectName, string fileName)
    {
        UploadTemplateEntity templateEntity = new UploadTemplateEntity();


        if (projectName != null)
        {
            templateEntity.FileName = fileName;
            templateEntity.ProjectName = projectName;
            templateEntity.CreatedBy = uploadBy;
            //templateEntity.RoleName = context.Session["UserRole"].ToString();
            templateEntity.UserEmail = HttpContext.Current.Session["UserEmail"].ToString();
            templateEntity.Status = "Success";

            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
            uploadTemplateBl.DeleteProjectDataFromDB(templateEntity);
        }

    }

    private void DeleteAnalysisProcessedDataFromDB(string uploadBy, string projectName, string fileName)
    {
        UploadTemplateEntity templateEntity = new UploadTemplateEntity();


        if (projectName != null)
        {
            templateEntity.FileName = fileName;
            templateEntity.ProjectName = projectName;
            templateEntity.CreatedBy = uploadBy;
            //templateEntity.RoleName = context.Session["UserRole"].ToString();
            templateEntity.UserEmail = HttpContext.Current.Session["UserEmail"].ToString();
            templateEntity.Status = "Success";

            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
            uploadTemplateBl.DeleteProjectProcessedDataFromDB(templateEntity);

            uploadTemplateBl.CallZohoApiToImoortData(templateEntity);

        }     
    }



    public void ProcessRequest(HttpContext context)
    {

        try
        {
            //string fileName = "";
            string uploadedBy = HttpContext.Current.Session["UserEmail"].ToString();
            bool deleteAndCreate = Convert.ToBoolean(context.Request["DeleteAndCreate"]);
            string filePath = HttpContext.Current.Session["targetfolder"].ToString(); //"./MasterTemplate/";

            if (deleteAndCreate)
            {

                string projectName = context.Session["SelectedProjectName"].ToString();

                string userNameWithSelectedProject = uploadedBy.Replace(" ", "") + "~" + projectName;
                new List<string>(Directory.GetFiles(context.Server.MapPath(filePath))).ForEach(file =>
                {
                    Regex re = new Regex(userNameWithSelectedProject, RegexOptions.IgnoreCase);
                    if (re.IsMatch(file))
                        //  fileName = file.ToString();
                        File.Delete(file);
                });

                string selectedTemplate = HttpContext.Current.Session["TemplateName"].ToString();
                //Nabin : - files have been deleted from directory, now delete from database.
                DeleteAnalysisProcessedDataFromDB(uploadedBy, projectName, selectedTemplate);
                DeleteAnalysisDataFromDB(uploadedBy, projectName, selectedTemplate);                
            }
            if (context.Request.Files.Count > 0)
            {


                HttpPostedFile file = context.Request.Files[0];
                if (!FileContainsDuplicate())
                {
                    if (HttpContext.Current.Session["ForScreen"].ToString() == "TemplateManagement")
                    {
                        StartTemplateManagementTask(context, file, filePath, uploadedBy);
                    }
                    else if (context.Request["ForScreen"] == "FileManagement")
                    {
                        StartFileManagementTask(context, file, filePath, uploadedBy);
                    }

                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File cannot be uploaded, <br> as it contains duplicate record(s).");
                    context.Response.End();
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
            context.Response.Write("File uploaded successfully.");
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
        _tableScript.AppendLine("ALTER TABLE dbo." + templateName + " " + "OWNER to spendpguser;");

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
        string TableName = templateName + "_" + "Processed";

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
        _tableScript.AppendLine("ALTER TABLE dbo." + TableName + " " + "OWNER to spendpguser;");
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
        string projectName = context.Session["SelectedProjectName"].ToString();
        string fileName = file.FileName;
        string selectedTemplate = HttpContext.Current.Session["TemplateName"].ToString(); ;
        fileName = fileName.Replace(".csv", "~" + uploadedBy.Replace(" ", "") + "~" + projectName + ".csv");
        string filePathWithFileName = context.Server.MapPath(filePath + fileName);

        if (CheckUploadedFileSchema(selectedTemplate, uploadedBy, projectName, context, file))
        {
            if (!CheckUploadedFileHaveOnlyHeader(file))
            {

                if (!System.IO.File.Exists(filePathWithFileName))
                {

                    file.SaveAs(filePathWithFileName);
                    //call the function to db entry
                    SaveUploadTemplateInformationInDB(fileName, uploadedBy, projectName, context, filePathWithFileName, "");

                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File Uploaded Successfully.");
                    context.Response.End();

                }
                else
                {
                    //Get the latest File Name with path
                    filePathWithFileName = GetLatestUploadedFileNameWithPath(filePathWithFileName, fileName, context, filePath);
                    string existingFileWithPath = filePathWithFileName;
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
                        //if (CheckUploadedFileSchema(fileName, uploadedBy, projectName, context, filePathWithFileName))
                        //{
                        file.SaveAs(context.Server.MapPath(System.IO.Path.Combine(filePath, fileName)));
                        SaveUploadTemplateInformationInDB(fileName, uploadedBy, projectName, context, filePathWithFileName, existingFileWithPath);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("File Uploaded Successfully.");
                        context.Response.End();
                        //}
                        //else
                        //{
                        //    context.Response.ContentType = "text/plain";
                        //    context.Response.Write("Upload Failed :- Invalid Template Header.");
                        //    context.Response.End();
                        //}
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
        else
        {
            //delete uploaded file    
            context.Response.ContentType = "text/plain";
            context.Response.Write("Upload Failed :- Invalid Template Header.");
            context.Response.End();
        }

    }
    private bool FileContainsDuplicate()
    {
        bool result = false;
        HttpPostedFile _file = System.Web.HttpContext.Current.Request.Files[0];
        Stream strm = _file.InputStream;
        DataTable uploadedTemplateDataTable = new DataTable();
        StreamReader uploadedFS = new StreamReader(_file.InputStream);
        uploadedFS.BaseStream.Position = 0;
        TextReader uploaderFileTextReader = new StreamReader(uploadedFS.BaseStream);
        CsvReader.CsvReader _csvTableLoader = new CsvReader.CsvReader(uploaderFileTextReader, true);

        uploadedTemplateDataTable.Load(_csvTableLoader);

        if (uploadedTemplateDataTable.Rows.Count > 0)
        {

            var UniqueRows = uploadedTemplateDataTable.AsEnumerable().Distinct(DataRowComparer.Default);
            DataTable uniqueDataTable = UniqueRows.CopyToDataTable();
            if (uniqueDataTable.Rows.Count != uploadedTemplateDataTable.Rows.Count)
            {
                result = true;
            }

        }
        else
        {
            result = false;
        }

        return result;
    }
    private string GetLatestUploadedFileNameWithPath(string filePathWithFileName, string fileName, HttpContext context, string filePath)
    {
        string tempfileName;
        string orgFileNameWithPath = filePathWithFileName;
        int counter = 1;
        while (System.IO.File.Exists(filePathWithFileName))
        {
            fileName = fileName.Replace(".csv", "");
            tempfileName = fileName + "-" + "V-" + counter.ToString() + ".csv";
            filePathWithFileName = filePath + tempfileName;
            filePathWithFileName = context.Server.MapPath(filePathWithFileName);
            counter++;
        }
        if (counter == 2)
        {
            filePathWithFileName = orgFileNameWithPath;
        }
        else if (counter > 2)
        {
            counter = counter - 2;
            fileName = fileName.Replace(".csv", "");
            tempfileName = fileName + "-" + "V-" + counter.ToString() + ".csv";
            filePathWithFileName = filePath + tempfileName;
            filePathWithFileName = context.Server.MapPath(filePathWithFileName);
        }
        return filePathWithFileName;
    }

    private bool CheckUploadedFileSchema(string fileName, string uploadBy, string projectName, HttpContext context, HttpPostedFile file)
    {
        UploadTemplateEntity templateEntity = new UploadTemplateEntity();
        Boolean _result = false;
        if (projectName != null && fileName != null)
        {
            templateEntity.FileName = fileName;
            templateEntity.ProjectName = projectName;
            templateEntity.CreatedBy = uploadBy;
            templateEntity.RoleName = context.Session["UserRole"].ToString();
            templateEntity.UserEmail = context.Session["UserEmail"].ToString();
            templateEntity.Status = "Success";

            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();

            DataTable _processedTable = uploadTemplateBl.GetAnalyticsProcessedDataSchema(templateEntity).Tables[0];

            StreamReader uploadedFS = new StreamReader(file.InputStream);
            uploadedFS.BaseStream.Position = 0;
            TextReader uploaderFileTextReader = new StreamReader(uploadedFS.BaseStream);
            //(Stream)uploaderFileTextReader.BaseStream.Position = 0;
            DataTable _uploadedTemplateDataTable = new CSVReader(uploaderFileTextReader).CreateDataTable(true);

            for (int col = _uploadedTemplateDataTable.Columns.Count - 1; col >= 0; col--)
            {
                if (_uploadedTemplateDataTable.Columns[col].ColumnName.IndexOf("Column") > -1)
                {
                    _uploadedTemplateDataTable.Columns.RemoveAt(col);
                }
            }

            _uploadedTemplateDataTable.AcceptChanges();
            //_uploadedTemplateDataTable = GetUpdatedTemplateDataTable(fileName, filePathWithFileName, templateEntity);

            foreach (DataColumn _dc in _uploadedTemplateDataTable.Columns)
            {
                _dc.ColumnName = _dc.ColumnName.Replace(" / ", "_or_").Replace(" ", "_");
            }
            _processedTable.Columns.Remove("project_name");
            _processedTable.Columns.Remove("user_email");
            _processedTable.Columns.Remove("uploaded_by");
            _processedTable.Columns.Remove("is_processed");
            _result = IsBothTableSchemaSame(_uploadedTemplateDataTable, _processedTable);
        }
        return _result;
    }
    private bool IsBothTableSchemaSame(DataTable sessionDataTable, DataTable processedDataTable)
    {
        bool _result = false;

        if (sessionDataTable != null && processedDataTable != null)
        {
            if (sessionDataTable.Columns.Count != processedDataTable.Columns.Count)
            {
                _result = false;
            }
            else
            {
                for (int i = 0; i < processedDataTable.Columns.Count; i++)
                {
                    if (processedDataTable.Columns[i].ColumnName.ToLower() != sessionDataTable.Columns[i].ColumnName.ToLower())
                    {
                        _result = false;
                        break;
                    }
                    _result = true;
                }
            }
        }
        else
        {
            _result = false;
        }
        return _result;
    }
    private void SaveUploadTemplateInformationInDB(string fileName, string uploadBy, string projectName, HttpContext context, string filePathWithFileName, string existingFileWithPath)
    {
        UploadTemplateEntity templateEntity = new UploadTemplateEntity();

        bool deleteAndCreatedata = Convert.ToBoolean(context.Request["DeleteAndCreate"]);
        if (projectName != null && fileName != null)
        {
            templateEntity.FileName = fileName;
            templateEntity.ProjectName = projectName;
            templateEntity.CreatedBy = uploadBy;
            templateEntity.RoleName = context.Session["UserRole"].ToString();
            templateEntity.UserEmail = context.Session["UserEmail"].ToString();
            templateEntity.Status = "Success";
            templateEntity.IsDeleteAndCreate = deleteAndCreatedata;

            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();
            uploadTemplateBl.SaveAssignedTemplate(templateEntity);
            DataTable _uploadedTemplateDataTable = GetUpdatedTemplateDataTable(fileName, filePathWithFileName, templateEntity);

            string filePathzoho = HttpContext.Current.Session["targetfolder"].ToString();
            string filePathdirectoryzoho = "./CSV/";
            string filePathWithFileNameZoho = context.Server.MapPath(filePathdirectoryzoho);
            templateEntity.filePathZoho = filePathWithFileNameZoho;

            if (existingFileWithPath != "")
            {
                DataTable _existingTemplateDataTable = GetUpdatedTemplateDataTable(fileName, existingFileWithPath, templateEntity);
                DataTable _NewTemplateTable = RemoveDuplicateRecords(_existingTemplateDataTable, _uploadedTemplateDataTable);
                createCsvFile(context, _NewTemplateTable, templateEntity.FileName, templateEntity);
                uploadTemplateBl.SaveUploadTemplate(_NewTemplateTable, templateEntity);
            }
            else
            {
                DataTable dtZoho = new DataTable();
                dtZoho = uploadTemplateBl.zohoTable(_uploadedTemplateDataTable, templateEntity);
                if (dtZoho.Rows.Count > 0)
                {
                    createCsvFile(context, dtZoho, templateEntity.FileName, templateEntity);
                }
                else
                {
                    createCsvFile(context, _uploadedTemplateDataTable, templateEntity.FileName, templateEntity);
                }
                uploadTemplateBl.SaveUploadTemplate(_uploadedTemplateDataTable, templateEntity);

            }
        }

    }


    private void createCsvFile(HttpContext context, DataTable dataTable, string fileName, UploadTemplateEntity templateEntity)
    {
        string filePath = HttpContext.Current.Session["targetfolder"].ToString();
        filePath = "./CSV/";
        string filePathWithFileName = context.Server.MapPath(filePath);
        templateEntity.filePathZoho = filePathWithFileName;
        string filecreate = filePathWithFileName + fileName + ".csv";
        try
        {
            if (!Directory.Exists(filePathWithFileName))
            {
                System.IO.Directory.CreateDirectory(filePathWithFileName);
            }

            string[] files = Directory.GetFiles(filePathWithFileName);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }

            using (var myFile = File.Create(filecreate))
            {
                // interact with myFile here, it will be disposed automatically
            }
            UploadTemplateBL uploadTemplateBl = new UploadTemplateBL();

            uploadTemplateBl.EXPORT_CSV(dataTable, filecreate);
            //ToCSV(dataTable, filePathWithFileName);
            uploadTemplateBl.CallZohoApiToImoortData(templateEntity);
        }

        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    private DataTable RemoveDuplicateRecords(DataTable oldTable, DataTable newTable)
    {
        foreach (DataRow row1 in oldTable.Rows)
        {
            foreach (DataRow row2 in newTable.Rows)
            {
                var array1 = row1.ItemArray;
                var array2 = row2.ItemArray;

                if (array1.SequenceEqual(array2))
                {
                    row2.Delete();
                }
            }
            newTable.AcceptChanges();
        }

        return newTable;
    }
    private DataTable GetUpdatedTemplateDataTable(string fileName, string filePathWithFileName, UploadTemplateEntity templateEntity)
    {
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
        return _uploadedTemplateDataTable;

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
        else if (!CheckColumns(_uploadedTemplateDataTable))
        {
            _result = false;
        }
        templateUploadFile.InputStream.Position = 0;
        return _result;
    }
    private bool CheckColumns(DataTable uploadedTemplateDataTable)
    {
        bool _result = true;
        foreach (DataColumn dc in uploadedTemplateDataTable.Columns)
        {
            if (HasSpecialChar(dc.ColumnName))
            {
                _result = false;
            }
        }
        return _result;
    }
    private bool HasSpecialChar(string input)
    {
        /* string specialChar = @"\|!#$%&()=?»«@£§€{}.;'<>,";
         foreach (var item in specialChar)
         {
             if (input.Contains(item)) return true;
         }

         return false;*/
        return true;
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