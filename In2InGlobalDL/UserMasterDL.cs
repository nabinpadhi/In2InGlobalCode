using In2InGlobalBusinessEL;
using kss.ra.dataaccess;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.datalink
{
    /// <summary>
    /// User Master DL
    /// </summary>
    public class UserMasterDL
    {
        /// <summary>
        /// This Function is used to load company name
        /// </summary>
        /// <returns></returns>
        public DataSet getCompanyNameForUser() 
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dscompanyname = new DataSet();
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
                    npgsqlDataAdapter.Fill(dscompanyname);

                    return dscompanyname;  
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
        /// This Function is used to load Activity Name
        /// </summary>
        /// <returns></returns>
        public DataSet getActivityNameForUser() 
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsactivity = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getactivityname()"; 
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);  
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsactivity);

                    return dsactivity;
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
        /// This function is used to load RoleName
        /// </summary>
        /// <returns></returns>
        public DataSet getRoleNameForUser() 
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsrolename = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getuserrolename()"; 
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection); 
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsrolename);

                    return dsrolename;
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

        public DataSet FillUserGridInfo()
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsuser = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.populateusermasterinfo()";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
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
        /// Save User Master
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public long SaveUserMaster(UserEntity userEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.saveuserinfo(@firstname,@lastname,@email,@userphone,@password,@activityid,@companyid,@roleid,@activated,@createdby)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        firstname = userEntity.FirstName,
                        lastname = userEntity.LastName,
                        email = userEntity.Email,
                        userphone = userEntity.PhoneNumber,
                        password = userEntity.Password,
                        activityid = userEntity.ActivityId,
                        companyid = userEntity.CompanyId,
                        roleid = userEntity.RoleId,
                        activated= true,
                        //lastlogin = userEntity.lastlogin,
                        createdby = userEntity.CreatedBy
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }

                  //  userEntity.UserId = Convert.ToInt64(result.First().UserId);

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
            return userEntity.UserId;
        }


        /// <summary>
        /// Save User Master
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public long UpdateUser(UserEntity userEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.updateuserinfo(@firstname,@lastname,@companyname,@useremail,@phonenumber,@roleid)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        firstname = userEntity.FirstName,
                        lastname = userEntity.LastName,
                        useremail = userEntity.Email,
                        phonenumber = userEntity.PhoneNumber,                     
                        //activityid = userEntity.ActivityId,
                        companyname = userEntity.CompanyName,
                        roleid = userEntity.RoleId
                        
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }

                    //  userEntity.UserId = Convert.ToInt64(result.First().UserId);

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
            return userEntity.UserId;
        }

        /// <summary>
        /// Save User Master
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public long DeleteUser(UserEntity userEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();

            var query = @"SELECT * FROM dbo.deleteuser(@email)";

            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        email = userEntity.Email 

                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        //  throw (" failed to create company").ToString();
                    }
                    //  userEntity.UserId = Convert.ToInt64(result.First().UserId);

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
            return userEntity.UserId;
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

        public DataSet GetProjectForUser(UserEntity userEntity) 
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsuser = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();
           
            string query = @"SELECT * FROM dbo.getprojectusers(@email)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@email", userEntity.Email);
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
    }
}
