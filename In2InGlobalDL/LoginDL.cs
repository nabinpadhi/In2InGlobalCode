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

    }
}
