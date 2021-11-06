using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace In2InGlobal.presentation
{
    public partial class TestPostgres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TestButton_Click(object sender, EventArgs e)
        {

        }
        public static DbConnection GetDatabaseConnection()
        {

            NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
            npgsqlConnectionStringBuilder.Host = "localhost";
            npgsqlConnectionStringBuilder.Port = 5432;
            npgsqlConnectionStringBuilder.Database = "database";
            npgsqlConnectionStringBuilder.Username = "postgres";
            npgsqlConnectionStringBuilder.Password = "postgres";

            var conn = new NpgsqlConnectionFactory().CreateConnection(npgsqlConnectionStringBuilder.ConnectionString.ToString());
            return conn;
        }
    }
}