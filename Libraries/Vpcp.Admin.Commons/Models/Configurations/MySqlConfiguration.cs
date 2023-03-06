using MySql.Data.MySqlClient;

namespace Vpcp.Admin.Commons.Models.Configurations
{
    public class MySqlConfiguration
    {

        private static MySqlConfiguration? _instance;

        public static MySqlConfiguration Instance()
        {
            if (_instance == null)
            {
                _instance = new MySqlConfiguration();
            }

            return _instance;
        }

        private MySqlConfiguration()
        {
            ConnectionStringBuilder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "vpcp",
                UserID = "root",
                Password = ""
            };
        }

        private MySqlConnectionStringBuilder ConnectionStringBuilder { get; }

        public string ConnectionString
        {
            get
            {
                return ConnectionStringBuilder.ConnectionString;
            }
        }
    }
}
