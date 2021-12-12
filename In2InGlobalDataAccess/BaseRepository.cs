using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace kss.ra.dataaccess
{
    public class BaseRepository
    {
        public NpgsqlConnection GetDBConnection()
        {
            var connectionString = "Host=localhost;port=5432;Username=postgres;Password=bhptpl@79;Database=In2InGlobal";           
            var databaseConnection = new NpgsqlConnection(connectionString);
            return databaseConnection;

        }
    }
}
