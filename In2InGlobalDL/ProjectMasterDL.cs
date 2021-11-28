using In2InGlobalBusinessEL;
using kss.ra.dataaccess;
using Npgsql;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.datalink
{
    public class ProjectMasterDL  
    {
        /// <summary>
        /// This Function is used to Save the Project
        /// </summary>
        /// <param name="projectEntity"></param>
        /// <returns></returns>
        public long SaveProjectMaster(ProjectEntity projectEntity)  
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveprojectinfo(@projectname,@description,@createdby)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        projectname = projectEntity.ProjectName,
                        description = projectEntity.Description,                         
                        createdby = projectEntity.CreatedBy
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
            return projectEntity.ProjectId;
        }

        /// <summary>
        /// get Project Details
        /// </summary>
        /// <returns></returns>
        public DataSet getProjectDetails()
        { 
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsProject = new DataSet();
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
    }
}
