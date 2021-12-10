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
    public class AnalyticsDL
    {
        /// <summary>
        /// This function is used to Get Company Name
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyName()
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dscompany = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getcompanyname()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dscompany);

                    return dscompany;
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

            }

        }

        /// <summary>
        /// getUserEmailByCompany
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataSet getUserEmailByCompany(long companyId)    
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsuseremail = new DataSet(); 
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getuseremailbycompanyid(@companyid)"; 
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);                    
                    npgsqlCommand.Parameters.AddWithValue("@companyid", companyId);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsuseremail);

                    return dsuseremail;
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

            }

        }

        /// <summary>
        /// getProjectNameByUserEmail
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="useremail"></param>
        /// <returns></returns>
        public DataSet getProjectNameByUserEmail(int companyId,string useremail)  
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsprojectName = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getprojectnamebyuseremail(@companyid,@useremail)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@companyid", companyId);
                    npgsqlCommand.Parameters.AddWithValue("@useremail", useremail);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsprojectName);

                    return dsprojectName;
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

            }

        }

        /// <summary>
        /// SaveAnalyticConfiguration
        /// </summary>
        /// <param name="analyticsEntity"></param>
        /// <returns></returns>
        public long SaveAnalyticConfiguration(AnalyticsEntity analyticsEntity)   
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveanalyticconfiguration(@projectid ,@dashboardid,@companyid,@userid,@templateid,@workspaceid,@createdby,@dashboardurl)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {                        
                        projectid = analyticsEntity.ProjectId,
                        dashboardid = analyticsEntity.DashboardId,
                        companyid = analyticsEntity.CompanyId,
                        userid = analyticsEntity.UserId,
                        templateid = analyticsEntity.TemplateId,
                        workspaceid = analyticsEntity.WorkspaceId,
                        createdby = analyticsEntity.CreatedBy,
                        dashboardurl = analyticsEntity.DashboardUrl,
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
            return analyticsEntity.Id;
        }

        /// <summary>
        /// getAnalyticsGridDetails
        /// </summary>
        /// <returns></returns>
        public DataSet getAnalyticsGridDetails()  
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsAnalytics = new DataSet(); 
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getanalyticsgriddetails()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsAnalytics);

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
                return dsAnalytics; 
            }

        }

        /// <summary>
        /// UpdateAnalyticConfiguration
        /// </summary>
        /// <param name="analyticsEntity"></param>
        /// <returns></returns>
        public long UpdateAnalyticConfiguration(AnalyticsEntity analyticsEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.updateanalyticconfiguration(@companyid,@userid,@projectid,@dashboardurl)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        userid = analyticsEntity.UserId,
                        companyid = analyticsEntity.CompanyId,
                        projectid = analyticsEntity.ProjectId,
                        dashboardurl = analyticsEntity.DashboardUrl
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
            return analyticsEntity.Id;
        }

        /// <summary>
        /// DeleteAnalyticConfiguration
        /// </summary>
        /// <param name="analyticsEntity"></param>
        /// <returns></returns>
        public long DeleteAnalyticConfiguration(AnalyticsEntity analyticsEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.deleteanalyticconfiguration(@companyid,@userid,@projectid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        companyid = analyticsEntity.CompanyId,
                        userid  = analyticsEntity.UserId,
                        projectid = analyticsEntity.ProjectId 
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
            return analyticsEntity.Id;
        }

    }
}
