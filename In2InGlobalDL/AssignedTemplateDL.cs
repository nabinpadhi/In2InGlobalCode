using Dapper;
using In2InGlobalBusinessEL;
using kss.ra.dataaccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.datalink
{
    /// <summary>
    /// This class used for Assigned the Template for user and Project
    /// </summary>
    public class AssignedTemplateDL
    {
        /// <summary>
        /// Populate all project Name to assigned Template
        /// </summary> 
        /// <returns></returns>
        public DataSet PopulateProjectNameForTemplate() 
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
        public DataSet PopulateAllUserEmailForAssignedProject(long projectId) 
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
        public DataSet PopulateTemplateNameForAssignedProjectAndUser(long projectid,long userid) 
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
        /// Save Assigned Template
        /// </summary>
        /// <param name="assignedTemplateEntity"></param> 
        /// <returns></returns>
        public long SaveAssignedTemplate(AssignedTemplateEntity assignedTemplateEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveassignedtemplate(@templateid,@userid,@projectid,@createdby)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        templateid = assignedTemplateEntity.TemplateId,                         
                        userid = assignedTemplateEntity.UserId,
                        projectid = assignedTemplateEntity.ProjectId,
                        createdby = assignedTemplateEntity.AssignedBy, 
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                      //throw (" failed to create company").ToString();
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
            return assignedTemplateEntity.Id;
        }

        /// <summary>
        /// Populate All Assigned Template
        /// </summary>
        /// <returns></returns>  
        public DataSet PopulateAllAssignedTemplateGrid()
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsAssignedTemplate = new DataSet(); 
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.populateassignedtemplategrid()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);   
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsAssignedTemplate);
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
                return dsAssignedTemplate;
            }
        }

        /// <summary>
        /// Delete Assigned Template
        /// </summary>
        /// <param name="assignedTempEntity"></param>
        /// <returns></returns>
        public long DeleteAssignedTemplate(AssignedTemplateEntity assignedTempEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.deleteassignedtemplate(@templateid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        companyid = assignedTempEntity.TemplateId

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
            return assignedTempEntity.TemplateId; 
        }

        /// <summary>
        /// Create Project
        /// </summary>
        /// <param name="createProject"></param>
        /// <returns></returns>
        public long CreateProject(ProjectEntity createProject)    
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveprojectinfo(@templateid,@userid,@projectid,@createdby)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        projectname = createProject.ProjectName,
                        description = createProject.Description,
                        createdby = createProject.CreatedBy
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //throw (" failed to create company").ToString();
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
            return createProject.ProjectId;
        }

        /// <summary>
        /// Populate Create Project Grid
        /// </summary>
        /// <returns></returns>
        public DataSet PopulateCreateProjectGrid() 
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsCreateProject = new DataSet(); 
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.fillprojectgrid()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsCreateProject);
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
                return dsCreateProject;
            }
        }

    }
}
