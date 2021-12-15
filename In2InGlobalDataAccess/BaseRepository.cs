using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Configuration;

namespace kss.ra.dataaccess
{
    public class BaseRepository
    {
        public NpgsqlConnection GetDBConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["In2InDBConnection"].ConnectionString;                   
            var databaseConnection = new NpgsqlConnection(connectionString);
            return databaseConnection;

        }
    }
}
