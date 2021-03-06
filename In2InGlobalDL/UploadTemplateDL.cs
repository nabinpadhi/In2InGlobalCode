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

namespace In2InGlobal.datalink
{
    /// <summary>
    /// Upload Template
    /// </summary>
    public class UploadTemplateDL
    {

        string CLIENT_ID = "1000.MO8JJ8U6GPTBQMI9FH9B301MM5AW5N";
        string CLIENT_SECRET = "ca62a259b6e2d969f0b05d2b5fcad9d1e666fc4647";
        string REFRESH_TOKEN = "1000.c703e338195b3ef8ac30b4a319afb5ec.1e3d14dd8bd974828bbe77ad7984cc39";
        string EMAIL = "info@in2inglobal.com";
        string DBNAME = "Spend_Analytics_WS";
        string TBNAME = "Spend_Analytics";


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
                }
            }

            try
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    if (analyticsProcessedData.Tables[0].Rows.Count > 0 && dtComapareTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtComapareTable.Rows)
                        {
                            // Create an NpgsqlParameter for every field in the column
                            var parameters = new List<DbParameter>();

                            for (var i = 0; i < dtComapareTable.Columns.Count; i++)
                            {
                                parameters.Add(new NpgsqlParameter($"@p{i}", row[i]));
                            }
                            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));

                            // Create an INSERT SQL query which inserts the data from the current row into PostgreSql table
                            var command = new NpgsqlCommand(
                                $"INSERT INTO dbo.{tableName} VALUES (DEFAULT,{parameterNames})",
                                connection);
                            command.Parameters.AddRange(parameters.ToArray());
                            command.ExecuteNonQuery();
                        }
                        WriteToCsvFile(dtComapareTable, tableName);
                    }
                    else
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
                                $"INSERT INTO dbo.{tableName} VALUES (DEFAULT,{parameterNames})",
                                connection);
                            command.Parameters.AddRange(parameters.ToArray());
                            command.ExecuteNonQuery();
                        }
                        WriteToCsvFile(dtUploadTemplate, tableName);
                    }
                    transaction.Commit();

                }

                InsertDataInProcessedTable(uploadTemplateEntity);

                //Call ti ZohoAPI to Import the data
                CallZohoApiToImoortData();


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
        /// Call Zoho API
        /// </summary>
        public void CallZohoApiToImoortData()
        {
            IReportClient rc = getClient();
            importData(rc);
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

                    if (dsAnalyticsProcessedData.Tables[0].Columns.Count > 0)
                    {
                        if (dsAnalyticsProcessedData.Tables[0].Columns.Contains("id"))
                            dsAnalyticsProcessedData.Tables[0].Columns.Remove("id");
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


        private void SaveUploadTemplateDataInTable(DataTable dtUploadTemplate)
        {
            BaseRepository baseRepo = new BaseRepository();
            var connection = baseRepo.GetDBConnection();

            using (var transaction = connection.BeginTransaction())
            {
                //using (var adap =new NpgsqlDataAdapter)
                {
                    //DataSet dataSetUploadTemplate = new DataSet();
                    //adap.TableMappings.Add("Table",table.TableName)
                    //adap.InsertCommand.Connection = connection;
                    //adap.InsertCommand.Parameters.Add(DataColumn);
                    //dataSetUploadTemplate.Tables.Add(table);
                    //adap.Update(dataSetUploadTemplate);
                    //dataSetUploadTemplate.AcceptChanges();
                    //transaction.Commit();
                }

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

                    if (dsAnalyticsProcessedData.Tables[0].Columns.Count > 0)
                    {
                        if (dsAnalyticsProcessedData.Tables[0].Columns.Contains("id"))
                            dsAnalyticsProcessedData.Tables[0].Columns.Remove("id");
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

                return dsAnalyticsProcessedData;
            }
        }

        /// <summary>
        /// Write data to CSV file before update to Zoho
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="filename"></param>
        public void WriteToCsvFile(DataTable dataTable, string filename)
        {
            string folderPath = "C:\\CSV\\"; 

            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }


            System.IO.File.WriteAllText(folderPath + filename + ".csv", fileContent.ToString());
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


            public void copyDatabase(IReportClient RepClient)
            {
                string dbURI = RepClient.GetURI(EMAIL, DBNAME);
                string newDBName = "newdbname";
                string newDBDesc = "dbdesc";
                bool withData = true;
                string copyDBKey = "************";
                long dbid = RepClient.CopyDatabase(dbURI, newDBName, newDBDesc,
                            withData, copyDBKey, null);
            }



        /// <summary>
        /// Import data to zoho
        /// </summary>
        /// <param name="RepClient"></param>
        public void importData(IReportClient RepClient)
        {
           // string myFileName = @"C:\workspace\spend.csv";
            var fileArray = Directory.GetFiles(@"c:\CSV\");

            try
            {
                string tableURI = RepClient.GetURI(EMAIL, DBNAME, TBNAME);
                Dictionary<string, string> ImportConfig = new Dictionary<string, string>();
                ImportConfig.Add("ZOHO_ON_IMPORT_ERROR", "ABORT");
                ImportConfig.Add("ZOHO_CREATE_TABLE", "FALSE");
                ImportConfig.Add("ZOHO_AUTO_IDENTIFY", "TRUE");
                Dictionary<string, string> ImportRes = RepClient.ImportData(tableURI, ZohoReportsConstants.APPEND, fileArray[0].ToString(), ImportConfig);
            }
            catch (Exception ex)
            {
                // throw new System.Exception();
                ex.ToString();
            }
        }


    }
}
