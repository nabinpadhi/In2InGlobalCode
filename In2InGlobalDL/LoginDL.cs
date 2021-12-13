using kss.ra.dataaccess;
using Npgsql;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In2InGlobal.datalink
{
    public class LoginDL
    {
        /// <summary>
        /// Login check for user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataSet getMyLogin(string emailid) 
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsUser = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getprofiledetails(@email)"; 
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@email", emailid);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsUser);

                    return dsUser;
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

        public long UpdateUserLoginPwd(string usremail,string pwd)
        {
            long _result = 0;
            BaseRepository baseRepo = new BaseRepository();
            var query = @"SELECT * FROM dbo.updateuserloginpwd(@email,@paawrd)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    var result = connection.Query(query, new
                    {
                        email = usremail,
                        paawrd = pwd
                       
                    }, commandType: CommandType.Text
                    );

                    if (result == null || !result.Any())
                    {
                        _result = Convert.ToInt64(result);
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
            return _result;
        }
    }
}
