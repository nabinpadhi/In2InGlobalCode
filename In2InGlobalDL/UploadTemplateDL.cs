using Dapper;
using In2InGlobalBusinessEL;
using kss.ra.dataaccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.datalink
{
    /// <summary>
    /// Upload Template
    /// </summary>
    public class UploadTemplateDL
    {

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


        public long SaveUploadTemplate(DataTable dtUploadTemplate, UploadTemplateEntity uploadTemplateEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var connection = baseRepo.GetDBConnection();
            string tableName = uploadTemplateEntity.FileName;
            DataTable dt = new DataTable();
            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Open();
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
                            $"INSERT INTO dbo.{tableName} VALUES ({parameterNames})",
                            connection);
                        command.Parameters.AddRange(parameters.ToArray());
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }

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
    }
}
