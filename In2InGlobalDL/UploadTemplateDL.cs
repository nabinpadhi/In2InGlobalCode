using Dapper;
using In2InGlobalBusinessEL;
using kss.ra.dataaccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZReports;
using ZohoAnalytics;
using CsvHelper;
using System.Globalization;
using System.Configuration;

namespace In2InGlobal.datalink
{
    /// <summary>
    /// Upload Template
    /// </summary>
    public class UploadTemplateDL
    {

        // string CLIENT_ID = "1000.MO8JJ8U6GPTBQMI9FH9B301MM5AW5N";
        // string CLIENT_SECRET = "ca62a259b6e2d969f0b05d2b5fcad9d1e666fc4647";
        //string REFRESH_TOKEN = "1000.c703e338195b3ef8ac30b4a319afb5ec.1e3d14dd8bd974828bbe77ad7984cc39";
        // string EMAIL = "info@in2inglobal.com";
        // string DBNAME = "PO_Analytics";
        //string TBNAME = "PO_Analytics";
        //string DBID = "210664000000333001";

        string CLIENT_ID = ConfigurationManager.AppSettings["CLIENT_ID"];
        string CLIENT_SECRET = ConfigurationManager.AppSettings["CLIENT_SECRET"];
        string REFRESH_TOKEN = ConfigurationManager.AppSettings["REFRESH_TOKEN"];
        string EMAIL = ConfigurationManager.AppSettings["EMAIL"];
        string DBNAME = ConfigurationManager.AppSettings["DBNAME"];
        string TBNAME = ConfigurationManager.AppSettings["TBNAME"];
        public DataTable Zohotable = new DataTable();

        // var CLIENT_ID = ConfigurationManager.["CLIENT_ID"].ConnectionString;
        /// <summary>
        /// Load Project Name For Template
        /// </summary>
        /// <returns></returns>
        public DataSet CreateTableForMasterTemplate(string tableQuery)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsProject = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = tableQuery;
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, commandType: CommandType.Text);

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsProject;
            }
        }


        /// <summary>
        /// Load Project Name For Template
        /// </summary>
        /// <returns></returns>
        public DataSet LoadProjectNameForTemplate()
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsProject = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.fillProject()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsProject);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsProject;
            }
        }

        /// <summary>
        /// Populate All UserEmail For Assigned Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet LoadAllUserEmailForNotAssignedProject(long projectId)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsEmail = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.populatealluseremailforproject(@projectid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@projectid", projectId);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsEmail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsEmail;
            }
        }

        /// <summary>
        /// Populate Template Name For Project And User
        /// </summary>
        /// <returns></returns>
        public DataSet LoadTemplateForNotAssignedProjectAndUser(long projectid, string userid)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsTemplate = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.fillallassignedtemplateforprojectanduser(@projectid,@userid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@projectid", projectid);
                    npgsqlCommand.Parameters.AddWithValue("@userid", userid);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsTemplate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsTemplate;
            }
        }

        public DataSet LoadUploadFileTemplateGrid(string userRole, string userEmail, int pid)
        {
            string query = string.Empty;
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsEmail = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            if (userRole == "Admin")
            {
                query = @"SELECT * FROM dbo.populateadmintemplategrid(@useremail,@userrole,@projectname)";
            }
            else
            {
                query = @"SELECT * FROM dbo.populateusertemplategrid(@useremail,@userrole,@projectname)";
            }
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@useremail", userEmail);
                    npgsqlCommand.Parameters.AddWithValue("@userrole", userRole);
                    npgsqlCommand.Parameters.AddWithValue("@projectname", pid);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsEmail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsEmail;
            }
        }

        public DataSet LoadSearchTemplateGrid(string userRole, string userEmail, int pid)
        {
            string query = string.Empty;
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsEmail = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();
            if (userRole == "Admin")
            {
                query = @"SELECT * FROM dbo.searchadmintemplategrid(@useremail,@userrole,@projectname)";
            }
            else
            {
                query = @"SELECT * FROM dbo.searchusertemplategrid(@useremail,@userrole,@projectname)";
            }
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@useremail", userEmail);
                    npgsqlCommand.Parameters.AddWithValue("@userrole", userRole);
                    npgsqlCommand.Parameters.AddWithValue("@projectname", pid);

                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsEmail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsEmail;
            }
        }



        /// <summary>
        /// Save Upload Template
        /// </summary>
        /// <param name="templateEntity"></param>
        /// <returns></returns>
        public long SaveUploadTemplate(UploadTemplateEntity uploadTemplateEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.uploadtemplateinfo(@filename,@projectname,@createdby,@useremail,@status,@rolename)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        filename = uploadTemplateEntity.FileName,
                        projectname = uploadTemplateEntity.ProjectName,
                        createdby = uploadTemplateEntity.CreatedBy,
                        useremail = uploadTemplateEntity.UserEmail,
                        status = uploadTemplateEntity.Status,
                        rolename = uploadTemplateEntity.RoleName
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        // throw (" failed to create company").ToString();
                    }
                    // companyEntity.CompanyId = Convert.ToInt64(result.First().CompanyId);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }

            }
            return uploadTemplateEntity.TemplateId;
        }


        public long UpdateUploadTemplate(UploadTemplateEntity uploadTemplateEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.updateuploadtemplateinfo(@filename,@projectname,@createdby,@useremail,@status,@rolename)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        filename = uploadTemplateEntity.FileName,
                        projectname = uploadTemplateEntity.ProjectName,
                        createdby = uploadTemplateEntity.CreatedBy,
                        useremail = uploadTemplateEntity.UserEmail,
                        status = uploadTemplateEntity.Status,
                        rolename = uploadTemplateEntity.RoleName
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        // throw (" failed to create company").ToString();
                    }
                    // companyEntity.CompanyId = Convert.ToInt64(result.First().CompanyId);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }

            }
            return uploadTemplateEntity.TemplateId;
        }

        public DataTable GetZohoDataTable(DataTable dtUploadTemplate, UploadTemplateEntity uploadTemplateEntity)
        {
            BaseRepository baseRepo = new BaseRepository();

            //The below table to hold the old analytics data
            DataSet analyticsProcessedData = new DataSet();

            //The below table to hold new data after compare
            DataTable dtComapareTable = new DataTable();

            var connection = baseRepo.GetDBConnection();

            //The below vatriable hold actual table name in db
            string tableName = uploadTemplateEntity.FileName;


            if (tableName != string.Empty)
            {
                //Delete Previous Data
                DeleteAnalysisData(uploadTemplateEntity);

                //Get old data from processed table based on ProjectName and UserEmail
                analyticsProcessedData = LoadAnalyticsProcessedData(uploadTemplateEntity);

                //Check if the procesesed table having the data
                if (analyticsProcessedData.Tables[0].Rows.Count > 0)
                {
                    dtComapareTable = CompareDatatable(analyticsProcessedData.Tables[0], dtUploadTemplate);
                }
            }

            return dtComapareTable;
        }

        /// <summary>
        /// Save Upload Template
        /// </summary>
        /// <param name="dtUploadTemplate"></param>
        /// <param name="uploadTemplateEntity"></param>
        /// <returns></returns>
        public long SaveUploadTemplate(DataTable dtUploadTemplate, UploadTemplateEntity uploadTemplateEntity)
        {
            BaseRepository baseRepo = new BaseRepository();

            //The below table to hold the old analytics data
            DataSet analyticsProcessedData = new DataSet();

            //The below table to hold new data after compare
            DataTable dtComapareTable = new DataTable();

            var connection = baseRepo.GetDBConnection();

            //The below vatriable hold actual table name in db
            string tableName = uploadTemplateEntity.FileName;


            if (tableName != string.Empty)
            {
                //Delete Previous Data
                DeleteAnalysisData(uploadTemplateEntity);

                //Get old data from processed table based on ProjectName and UserEmail
                analyticsProcessedData = LoadAnalyticsProcessedData(uploadTemplateEntity);

                //Check if the procesesed table having the data
                if (analyticsProcessedData.Tables[0].Rows.Count > 0)
                {
                    dtComapareTable = CompareDatatable(analyticsProcessedData.Tables[0], dtUploadTemplate);
                    Zohotable = dtComapareTable;
                }
            }

            try
            {
                string _tempCSVFile = uploadTemplateEntity.UploadedFilePath + "_temp-" + uploadTemplateEntity.UserEmail + "-"+uploadTemplateEntity.ProjectName + ".csv";

                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    if (analyticsProcessedData.Tables[0].Rows.Count > 0 && dtComapareTable.Rows.Count > 0)
                    {                       
                        ToCSV(dtComapareTable, _tempCSVFile);
                        string sql = string.Format("COPY dbo.{0} FROM '{1}' DELIMITER ',' CSV Header;", tableName, _tempCSVFile);

                        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                        WriteToCsvFile(dtComapareTable, tableName, uploadTemplateEntity);
                    }
                    else
                    {                        
                        ToCSV(dtUploadTemplate, _tempCSVFile);
                        string sql = string.Format("COPY dbo.{0} FROM '{1}' DELIMITER ',' CSV Header;", tableName, _tempCSVFile);

                        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        WriteToCsvFile(dtUploadTemplate, tableName, uploadTemplateEntity);
                    }
                    transaction.Commit();
                   
                    File.Delete(_tempCSVFile);
                }

                InsertDataInProcessedTable(uploadTemplateEntity);

                //Call ti ZohoAPI to Import the data
                // CallZohoApiToImoortData(uploadTemplateEntity);


            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                connection.Dispose();
            }

            return uploadTemplateEntity.TemplateId;
        }

        /// <summary>
        /// Delete Analysis Data
        /// </summary>
        /// <param name="uploadTemplateEntity"></param>
        /// <returns></returns>
        public long DeleteAnalysisData(UploadTemplateEntity uploadTemplateEntity)
        {
            string tableName = uploadTemplateEntity.FileName;
            BaseRepository baseRepo = new BaseRepository();
            var query = $"Delete FROM dbo.{tableName} where user_email ='{uploadTemplateEntity.UserEmail}' AND project_name ='{uploadTemplateEntity.ProjectName}'";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }
                    // companyEntity.CompanyId = Convert.ToInt64(result.First().CompanyId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return uploadTemplateEntity.Id;
        }

        /// <summary>
        /// Delete Analysis Data
        /// </summary>
        /// <param name="uploadTemplateEntity"></param>
        /// <returns></returns>
        public long DeleteAnalysisProcessedData(UploadTemplateEntity uploadTemplateEntity)
        {
            string tableName = uploadTemplateEntity.FileName + "_Processed";
            BaseRepository baseRepo = new BaseRepository();
            var query = $"Delete FROM dbo.{tableName} where user_email ='{uploadTemplateEntity.UserEmail}' AND project_name ='{uploadTemplateEntity.ProjectName}'";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }
                    // companyEntity.CompanyId = Convert.ToInt64(result.First().CompanyId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return uploadTemplateEntity.Id;
        }


        /// <summary>
        /// Delete Analysis Data
        /// </summary>
        /// <param name="uploadTemplateEntity"></param>
        /// <returns></returns>
        public long DeleteUploadTemplateData(UploadTemplateEntity uploadTemplateEntity)
        {
            string tableName = "upload_template";            
            BaseRepository baseRepo = new BaseRepository();
            var query = $"Delete FROM dbo.{tableName} where user_email ='{uploadTemplateEntity.UserEmail}'AND template_file_name'{uploadTemplateEntity.FileToDelete}  AND project_name ='{uploadTemplateEntity.ProjectName}'";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }
                    // companyEntity.CompanyId = Convert.ToInt64(result.First().CompanyId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }
            }
            return uploadTemplateEntity.Id;
        }






        /// <summary>
        /// Load Analytics Processed Data
        /// </summary>
        /// <param name="uploadTemplateEntity"></param>
        /// <returns></returns>
        public DataSet LoadAnalyticsProcessedData(UploadTemplateEntity uploadTemplateEntity)
        {
            string tableName = uploadTemplateEntity.FileName;

            tableName = tableName + "_" + "Processed";
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsAnalyticsProcessedData = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = $"SELECT * FROM dbo.{tableName}  where user_email ='{uploadTemplateEntity.UserEmail}' AND project_name ='{uploadTemplateEntity.ProjectName}' ";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsAnalyticsProcessedData);

                   /* if (dsAnalyticsProcessedData.Tables[0].Columns.Count > 0)
                    {
                        if (dsAnalyticsProcessedData.Tables[0].Columns.Contains("id"))
                            dsAnalyticsProcessedData.Tables[0].Columns.Remove("id");
                    }*/

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }

                return dsAnalyticsProcessedData;
            }
        }

        /// <summary>
        /// Compare Datatable
        /// </summary>
        /// <param name="oldTable"></param>
        /// <param name="newTable"></param>
        /// <returns></returns>
        private DataTable CompareDatatable(DataTable oldTable, DataTable newTable)
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
                    ds.Tables.AddRange(new DataTable[] { newTable.Copy(), oldTable.Copy() });

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

        /// <summary>
        /// Insert DataIn Processed Table
        /// </summary>
        /// <param name="uploadTemplateEntity"></param>
        private void InsertDataInProcessedTable(UploadTemplateEntity uploadTemplateEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            string tableName = uploadTemplateEntity.FileName;

            tableName = tableName + "_" + "Processed";

            string query = $"INSERT INTO dbo.{tableName}" + " " +
                           $"SELECT * FROM dbo.{uploadTemplateEntity.FileName} " +
                           $"where user_email ='{uploadTemplateEntity.UserEmail}' " +
                           $"AND project_name ='{uploadTemplateEntity.ProjectName}' ";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        // throw (" failed to create company").ToString();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }

            }

        }

        /// <summary>
        /// SaveUploadTemplateDataInTable
        /// </summary>
        /// <param name="dtUploadTemplate"></param>
        private void SaveUploadTemplateDataInTable(DataTable dtUploadTemplate)
        {
            BaseRepository baseRepo = new BaseRepository();
            var connection = baseRepo.GetDBConnection();

            using (var transaction = connection.BeginTransaction())
            {

                foreach (DataRow row in dtUploadTemplate.Rows)
                {
                    // Create an NpgsqlParameter for every field in the column
                    var parameters = new List<DbParameter>();
                    for (var i = 0; i < dtUploadTemplate.Columns.Count; i++)
                    {
                        parameters.Add(new NpgsqlParameter($"@p{i}", row[i]));
                    }
                    var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));

                    // Create an INSERT SQL query which inserts the data from the current row into PostgreSql table
                    var command = new NpgsqlCommand(
                        $"INSERT INTO table_which_we_want_to_migrate VALUES ({parameterNames})",
                        connection);
                    command.Parameters.AddRange(parameters.ToArray());
                    command.ExecuteNonQuery();
                }

            }

        }

        /// <summary>
        /// Get Analytics Data
        /// </summary>
        /// <param name="uploadTemplateEntity"></param>
        /// <returns></returns>
        public DataSet GetAnalyticsProcessedDataSchema(UploadTemplateEntity uploadTemplateEntity)
        {
            string tableName = uploadTemplateEntity.FileName;

            tableName = tableName + "_" + "Processed";
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsAnalyticsProcessedData = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = $"SELECT * FROM dbo.{tableName}  where user_email ='{uploadTemplateEntity.UserEmail}' AND project_name ='{uploadTemplateEntity.ProjectName}' limit 1 ";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsAnalyticsProcessedData);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }

                return dsAnalyticsProcessedData;
            }
        }


        /// <summary>
        /// Call Zoho API
        /// </summary>
        public void CallZohoApiToImoortData(UploadTemplateEntity uploadTemplateEntity)
        {
            IReportClient rc = getClient();
            CopyWorkSpaceAndRename(rc, uploadTemplateEntity);
        }

        /// <summary>
        /// Get Zoho clinet details
        /// </summary>
        /// <returns></returns>
        public IReportClient getClient()
        {
            IReportClient RepClient = new ReportClient(CLIENT_ID, CLIENT_SECRET, REFRESH_TOKEN);
            return RepClient;
        }


        /// <summary>
        /// Write data to CSV file before update to Zoho
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="filename"></param>
        public void WriteToCsvFile(DataTable dataTable, string filename, UploadTemplateEntity ute)
        {
            //var filePath = ute.filePathZoho;
            // if (!Directory.Exists(filePath))
            // {
            //     Directory.CreateDirectory(filePath);
            // }

            // string filecreates = filePath + filename + ".csv";

            // string[] files = Directory.GetFiles(filePath);

            // if (files.Length > 0)
            // {
            //     string filecreate = filePath + filename + ".csv";

            //     foreach (string file in files)
            //     {
            //         File.Delete(file);
            //     }
            // }

            // using (var myFile = File.Create(filecreates))
            // {
            //     // interact with myFile here, it will be disposed automatically
            // }

            // EXPORT_CSV(dataTable, filecreates);
        }

        public void EXPORT_CSV(DataTable dataTable, string filecreates)
        {
            using (var textWriter = File.CreateText(filecreates))

            using (var csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture, true))
            {
                // Write columns
                foreach (DataColumn column in dataTable.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Write row values
                foreach (DataRow row in dataTable.Rows)
                {
                    for (var i = 0; i < dataTable.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }
        }



        public void SaveToCSV(DataTable DT, string filecreates)
        {
            char csvDelimiter = ';';
            try
            {
                // code block for writing headers of data table

                int columnCount = DT.Columns.Count;
                string columnNames = "";
                string[] output = new string[DT.Rows.Count + 1];
                for (int i = 0; i < columnCount; i++)
                {
                    columnNames += DT.Columns[i].ToString() + csvDelimiter;
                }
                output[0] += columnNames;

                // code block for writing rows of data table
                for (int i = 1; (i - 1) < DT.Rows.Count; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        output[i] += DT.Rows[i - 1][j].ToString() + csvDelimiter;
                    }
                }

                System.IO.File.WriteAllLines(filecreates, output, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }


        /// <summary>
        /// Import data to zoho
        /// </summary>
        /// <param name="RepClient"></param>
        public void importData(IReportClient RepClient, string workspace, string DBNAME, string filename, string filezohopath)
        {
            var filePath = filezohopath;
            // string rootFolder = @"C:\inetpub\wwwroot\SpendAnalysis\admin\CSV\"; 

            string filecreates = filePath + filename + ".csv";
            try
            {
                string tableURI = RepClient.GetURI(EMAIL, workspace, DBNAME);
                Dictionary<string, string> ImportConfig = new Dictionary<string, string>();
                ImportConfig.Add("ZOHO_ON_IMPORT_ERROR", "ABORT");
                ImportConfig.Add("ZOHO_CREATE_TABLE", "FALSE");
                ImportConfig.Add("ZOHO_AUTO_IDENTIFY", "TRUE");
                Dictionary<string, string> ImportRes = RepClient.ImportData(tableURI, ZohoReportsConstants.APPEND, filecreates, ImportConfig);
            }
            catch (Exception ex)
            {
                // throw new System.Exception();
                ex.ToString();
            }
        }

        //public void copyDatabase(IReportClient RepClient, UploadTemplateEntity uploadTemplateEntity)
        //{
        //    string dbURI = RepClient.GetURI(EMAIL, DBNAME);

        //    string TBNAME = uploadTemplateEntity.FileName;
        //    string newDBName = uploadTemplateEntity.FileName + "WS";
        //    string newDBDesc = uploadTemplateEntity.FileName + "WorkSpace";
        //    bool withData = false;
        //    copyDatabaseKey(RepClient);

        //    string copyDBKey = "086cc1f4fcbed31733ea7eb13fe05436";
        //    long dbid = RepClient.CopyDatabase(dbURI, newDBName, newDBDesc, withData, copyDBKey, null);
        //}

        public void CopyWorkSpaceAndRename(IReportClient RepClient, UploadTemplateEntity uploadTemplateEntity)
        {
            long dbid = 0;
            string companyName = GetCompanyName(uploadTemplateEntity.UserEmail);
            string workspace=companyName.Trim() + "_" + uploadTemplateEntity.ProjectName.Trim() + "_" + uploadTemplateEntity.UserEmail.Trim() + "_" + uploadTemplateEntity.FileName.Trim();
            string TBNAME = uploadTemplateEntity.FileName;
            string dbURI = RepClient.GetURI(EMAIL, DBNAME);

            string newDBName = workspace;
            string newDBDesc = workspace + "WorkSpace";
            bool withData = false;

            try
            {

                //dbid = RepClient.GetDatabaseID(dbURI, newDBName, null);
                string copyDBKey = RepClient.GetCopyDBKey(dbURI, null);

                DataSet isWorkSpacePresent = new DataSet();

                isWorkSpacePresent = GetWorkSpaceName(uploadTemplateEntity, workspace, TBNAME);

                if (isWorkSpacePresent.Tables[0].Rows.Count > 0)
                {
                    if (uploadTemplateEntity.IsDeleteAndCreate)
                    {
                        DeleteZohoWOrksheet(RepClient, EMAIL, workspace);
                        DeleteWorkSpaceInfo(uploadTemplateEntity, workspace, TBNAME);
                        SaveWorkSpaceInfo(uploadTemplateEntity, workspace, TBNAME);
                        dbid = RepClient.CopyDatabase(dbURI, newDBName, newDBDesc, withData, copyDBKey, null);
                        importData(RepClient, workspace, TBNAME, uploadTemplateEntity.FileName, uploadTemplateEntity.filePathZoho);
                    }
                    else
                    {
                        importData(RepClient, workspace, TBNAME, uploadTemplateEntity.FileName, uploadTemplateEntity.filePathZoho);
                    }
                }
                else
                {
                    if (isWorkSpacePresent.Tables[0].Rows.Count > 0)
                    {
                        //string uri = RepClient.GetURI(EMAIL);
                        //long result = RepClient.GetDatabaseID(uri, workspace, null);
                        importData(RepClient, workspace, TBNAME, uploadTemplateEntity.FileName, uploadTemplateEntity.filePathZoho);
                    }
                    else
                    {
                        string dbURIs = RepClient.GetURI(EMAIL, DBNAME);
                        string copyDBKeyy = RepClient.GetCopyDBKey(dbURIs, null);
                       
                        bool withDatas = false;
                        string copyDBKeys = copyDBKeyy;
                        //RepClient.CopyDatabase(dbURI, newDBName, newDBDesc, withData, copyDBKey, null);

                        dbid = RepClient.CopyDatabase(dbURIs, newDBName, newDBDesc, withDatas, copyDBKeys, null);
                       // dbid = RepClient.CopyDatabase(dbURI, newDBName, newDBDesc, withData, copyDBKey, null);
                        SaveWorkSpaceInfo(uploadTemplateEntity, workspace, TBNAME);
                        importData(RepClient, workspace, TBNAME, uploadTemplateEntity.FileName, uploadTemplateEntity.filePathZoho);
                    }

                }

                string dashboardurl = getEmbedURL(RepClient, companyName, uploadTemplateEntity);
            }

            catch (Exception ex)
            {
                ex.ToString();
            }
        }



        public void DeleteZohoWOrksheet(IReportClient RepClient, string email, string workspacename)
        {
            string userURI = RepClient.GetURI(email);
            string databaseName = workspacename;
            RepClient.DeleteDatabase(userURI, databaseName, null);
        }


        public void GetDashboards(IAnalyticsClient ac, long workspaceids, string company, UploadTemplateEntity uploadtemplateEntity)
        {
            try
            {
                Dictionary<string, List<Dictionary<string, object>>> dashboards = ac.GetDashboards();
                var viewId = "viewId";
                var workspaceId = "workspaceId";
                var orgId = "orgId";

                long viewIdx = 0;
                long workspaceIdx = 0;
                long orgIdx = 0;
                int count = 0;

                foreach (var item in dashboards)
                {
                    if (count == 0)
                    {
                        var its = item.Key.ToString();
                        int counter = 0;
                        for (int i = 0; i < item.Value.Count; i++)
                        {
                            var it = item.Value[i];
                            if (counter == 0)
                            {
                                foreach (var items in it)
                                {
                                    var viewparam = items.Key.ToString();
                                    var viewparamvalue = items.Value;
                                    if (viewparam == viewId)
                                    {
                                        viewIdx = Convert.ToInt64(viewparamvalue);
                                    }

                                    if (viewparam == workspaceId)
                                    {
                                        workspaceIdx = Convert.ToInt64(viewparamvalue);
                                        if (workspaceids == workspaceIdx)
                                        {
                                            counter = counter + 1;
                                        }
                                    }
                                    if (viewparam == orgId)
                                    {
                                        orgIdx = Convert.ToInt64(viewparamvalue);
                                    }
                                }
                            }
                        }
                        count = count + 1;
                    }
                }

                DataSet dsDashboard = new DataSet();
                dsDashboard = getDashboardData(uploadtemplateEntity.ProjectName, uploadtemplateEntity.UserEmail, workspaceIdx, viewIdx);

                if (dsDashboard.Tables[0].Rows.Count > 0)
                {
                    //View url already present for this dashboard
                }
                else
                {
                    GetViewUrl(ac, orgIdx, workspaceIdx, viewIdx, company, uploadtemplateEntity);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void GetViewUrl(IAnalyticsClient ac, long orgId, long workspaceId, long viewId, string company, UploadTemplateEntity uploadtemplateEntity)
        {
            try
            {
                IViewAPI view = ac.GetViewInstance(orgId, workspaceId, viewId);
                string viewUrl = view.GetViewURL(null);
                SaveDashboardConfiguration(orgId, workspaceId, viewId, company, uploadtemplateEntity, viewUrl);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public string getEmbedURL(IReportClient rc, string company, UploadTemplateEntity uploadtemplateEntity)
        {
            string result = String.Empty;
            //string DBNAME = uploadtemplateEntity.FileName;
           // DBNAME = company + "_" + DBNAME;

            string companyName = GetCompanyName(uploadtemplateEntity.UserEmail);
            string DBNAME = companyName + "_" + uploadtemplateEntity.ProjectName + "_" + uploadtemplateEntity.UserEmail + "_" + uploadtemplateEntity.FileName;


            string TBNAME = uploadtemplateEntity.FileName;

            try
            {
                string uri = rc.GetURI(EMAIL);
                long workspaceidresult = rc.GetDatabaseID(uri, DBNAME, null);
                long workspaceid = workspaceidresult;

                string CLIENT_ID = ConfigurationManager.AppSettings["CLIENT_ID"];
                string CLIENT_SECRET = ConfigurationManager.AppSettings["CLIENT_SECRET"];
                string REFRESH_TOKEN = ConfigurationManager.AppSettings["REFRESH_TOKEN"];

                IAnalyticsClient ac = new AnalyticsClient(CLIENT_ID, CLIENT_SECRET, REFRESH_TOKEN);
                GetDashboards(ac, workspaceid, company, uploadtemplateEntity);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return result;
        }


        /// <summary>
        /// Save Upload Template
        /// </summary>
        /// <param name="templateEntity"></param>
        /// <returns></returns>
        public void SaveDashboardConfiguration(long orgId, long workspaceId, long viewId, string company, UploadTemplateEntity uploadtemplateEntity, string url)
        {
            DataSet projectdata = new DataSet();
            DataSet companydata = new DataSet();
            projectdata = getProjectData(uploadtemplateEntity.ProjectName);
            companydata = getCompanyData(company);
            string userid = string.Empty;
            string companyid = string.Empty;
            string projectid = string.Empty;
            string createdby = string.Empty;


            if (projectdata.Tables[0].Rows.Count > 0)
            {
                userid = projectdata.Tables[0].Rows[0]["user_id"].ToString();
                projectid = projectdata.Tables[0].Rows[0]["project_id"].ToString();
                createdby = projectdata.Tables[0].Rows[0]["created_by"].ToString();
            }

            if (companydata.Tables[0].Rows.Count > 0)
            {
                companyid = companydata.Tables[0].Rows[0]["company_id"].ToString();
            }

            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveanalyticconfiguration(@projectid ,@dashboardid ,@companyid ,@userid ,@templateid ,@workspaceid,@createdby ,@dashboardurl,@templatename)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        projectid = Convert.ToInt64(projectid),
                        dashboardid = Convert.ToInt64(viewId),
                        companyid = Convert.ToInt64(companyid),
                        userid = Convert.ToInt64(userid),
                        templateid = Convert.ToInt64(uploadtemplateEntity.TemplateId),
                        workspaceid = Convert.ToInt64(workspaceId),
                        createdby = createdby,
                        dashboardurl = url,
                        templatename = uploadtemplateEntity.FileName
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        // throw (" failed to create company").ToString();
                    }
                    // companyEntity.CompanyId = Convert.ToInt64(result.First().CompanyId);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }
            }
        }

        public DataSet getProjectData(string projectname)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsProject = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();
            string query = string.Empty;

            query = @"SELECT * FROM dbo.getProjectDetails(@projectname)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@projectname", projectname);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsProject);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsProject;
            }
        }

        public DataSet getDashboardData(string projectname, string useremail, long workspaceid, long dashboardid)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsDashboard = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();
            string query = string.Empty;

            query = @"SELECT * FROM dbo.getDashboardData(@projectname,@workspaceid,@useremail,@dashboardid)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@projectname", projectname);
                    npgsqlCommand.Parameters.AddWithValue("@workspaceid", workspaceid);
                    npgsqlCommand.Parameters.AddWithValue("@useremail", useremail);
                    npgsqlCommand.Parameters.AddWithValue("@dashboardid", dashboardid);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsDashboard);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsDashboard;
            }
        }


        public DataSet getCompanyData(string companyname)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsCompany = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();
            string query = string.Empty;

            query = @"SELECT * FROM dbo.getCompanyData(@companyname)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@companyname", companyname);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsCompany);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return dsCompany;
            }
        }






        //public void deleteRow(IReportClient RepClient, UploadTemplateEntity uploadTemplateEntity, string company)
        //{
        //    try
        //    {
        //        string DBNAME = uploadTemplateEntity.FileName;
        //        DBNAME = company + "_" + DBNAME;
        //        string TBNAME = uploadTemplateEntity.FileName;

        //        string tableURI = RepClient.GetURI(EMAIL, DBNAME, TBNAME);
        //        string criteria = $"\"user_email\"=='{uploadTemplateEntity.UserEmail}'";

        //        var id = RepClient.DeleteData(tableURI, null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}


        public void copyDatabaseKey(IReportClient RepClient)
        {
            string dbURI = RepClient.GetURI(EMAIL, DBNAME);
            string key = RepClient.GetCopyDBKey(dbURI, null);
        }


        public DataSet GetWorkSpaceName(UploadTemplateEntity uploadTemplateEntity, string WorkSpaceName, string WorkspacetableName)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet workspacedata = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getworkspacename(@workspacename,@workspacetablename,@projectname,@email)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@workspacename", WorkSpaceName);
                    npgsqlCommand.Parameters.AddWithValue("@workspacetablename", WorkspacetableName);
                    npgsqlCommand.Parameters.AddWithValue("@projectname", uploadTemplateEntity.ProjectName);
                    npgsqlCommand.Parameters.AddWithValue("@email", uploadTemplateEntity.UserEmail);

                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(workspacedata);

                    if (workspacedata.Tables[0].Columns.Count > 0)
                    {

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }

                return workspacedata;
            }
        }


        /// <summary>
        /// Save Upload Template
        /// </summary>
        /// <param name="templateEntity"></param>
        /// <returns></returns>
        public void SaveWorkSpaceInfo(UploadTemplateEntity uploadTemplateEntity, string workspacename, string workspacetablename)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveworkspaceinfo(@workspacename,@workspacetablename,@projectname,@email,@createdby)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        workspacename = workspacename,
                        workspacetablename = workspacetablename,
                        projectname = uploadTemplateEntity.ProjectName,
                        createdby = uploadTemplateEntity.CreatedBy,
                        email = uploadTemplateEntity.UserEmail

                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        // throw (" failed to create company").ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }

            }

        }

        /// <summary>
        /// Save Upload Template
        /// </summary>
        /// <param name="templateEntity"></param>
        /// <returns></returns>
        public void DeleteWorkSpaceInfo(UploadTemplateEntity uploadTemplateEntity, string workspacename, string workspacetablename)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.deleteworkspaceinfo(@workspacename,@workspacetablename,@projectname,@email,@createdby)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        workspacename = workspacename,
                        workspacetablename = workspacetablename,
                        projectname = uploadTemplateEntity.ProjectName,
                        createdby = uploadTemplateEntity.CreatedBy,
                        email = uploadTemplateEntity.UserEmail

                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        // throw (" failed to create company").ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                }

            }

        }





        /// <summary>
        /// Populate All UserEmail For Assigned Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public string GetCompanyName(string emailid)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsEmail = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();
            string companyName = "";
            string query = @"SELECT * FROM dbo.getloginusercompany(@emailid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@emailid", emailid);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsEmail);

                    if (dsEmail.Tables[0].Rows.Count > 0)
                    {

                        companyName = dsEmail.Tables[0].Rows[0]["company_name"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Dispose();
                    npgsqlDataAdapter.Dispose();
                }
                return companyName;
            }
        }
    }
}
