using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using Dapper;
using kss.ra.dataaccess;
using System.Threading.Tasks;
using System.Data;

namespace In2InGlobal.datalink
{
    public class MyProfileDL
    {
        /// <summary>
        /// This method is used to get the MyProfile Information
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataSet getMyProfile(string email)
        {
            BaseRepository baseRepo = new BaseRepository();
            DataSet dsMyProfile = new DataSet();
            NpgsqlDataAdapter npgsqlDataAdapter = new NpgsqlDataAdapter();

            string query = @"SELECT * FROM dbo.getprofiledetails(@email)";
            using (var connection = baseRepo.GetDBConnection())
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    npgsqlCommand.Parameters.AddWithValue("@email", email);
                    npgsqlCommand.CommandType = CommandType.Text;
                    npgsqlDataAdapter.SelectCommand = npgsqlCommand;
                    npgsqlDataAdapter.Fill(dsMyProfile);

                    return dsMyProfile;
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
