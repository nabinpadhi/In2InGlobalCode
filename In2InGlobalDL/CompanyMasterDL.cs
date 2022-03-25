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
    /// <summary>
    /// Company MasterDL
    /// </summary>
    public class CompanyMasterDL
    {
        /// <summary>
        /// This function ios used to fill company information 
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyDetails()
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsCompany = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getcompanydetails()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
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
                    npgsqlDataAdapter.Dispose();
                }
                return dsCompany;
            }

        }

        public DataSet GetUsers(long companyid)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsuser = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getusers(@companyid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@companyid", companyid);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsuser);

                    return dsuser;
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
        /// Save Company Master
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        public long SaveCompanyMaster(CompanyEntity companyEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.savecompanyinfo(@companyname,@lob,@companyphone,@companyaddress,@status,@createdby)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        companyname = companyEntity.CompanyName,
                        lob = companyEntity.LOB,
                        companyphone = companyEntity.CompanyPhone,
                        companyaddress = companyEntity.CompanyAddress,
                        status = companyEntity.Status,
                        createdby = companyEntity.CreatedBy
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
            return companyEntity.CompanyId;
        }

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        public long UpdateCompany(CompanyEntity companyEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.updatecompanyinfo(@companyname,@companylob,@companyid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        companyname = companyEntity.CompanyName,
                        companylob = companyEntity.LOB,                         
                        companyid = companyEntity.CompanyId
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
            return companyEntity.CompanyId;
        }


        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        public long DeleteCompany(CompanyEntity companyEntity)
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.deletecompany(@companyid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        companyid = companyEntity.CompanyId

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
            return companyEntity.CompanyId;
        }

    }
}
