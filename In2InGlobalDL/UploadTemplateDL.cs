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

        /// <summary>
        /// Save Upload Template
        /// </summary>
        /// <param name="templateEntity"></param>
        /// <returns></returns>
        public long SaveUploadTemplate(UploadTemplateEntity uploadTemplateEntity,DataTable uploadTemplateData) 
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveuploadtemplateinfo(@templateid,@projectid,@userid,@templatename,@uploadededby,@status)";  
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        templateid = uploadTemplateEntity.TemplateId,
                        projectid = uploadTemplateEntity.ProjectId,
                        userid = uploadTemplateEntity.UserId,
                        templatename = uploadTemplateEntity.TemplateName,
                        uploadedby= uploadTemplateEntity.UploadedBy,
                        status= uploadTemplateEntity.Status                     

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

        private void SaveUploadTemplate(DataTable dtUploadTemplate)  
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
                        $"INSERT INTO dbo.spend_analytics VALUES ({parameterNames})",
                        connection);
                    command.Parameters.AddRange(parameters.ToArray());
                    command.ExecuteNonQuery();
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
    }
}
