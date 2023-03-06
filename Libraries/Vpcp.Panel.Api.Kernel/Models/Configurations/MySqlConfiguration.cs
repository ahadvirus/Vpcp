using MySql.Data.MySqlClient;

namespace Vpcp.Panel.Api.Kernel.Models.Configurations;

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
            Port = 3307,
            Database = "vpcp",
            UserID = "root",
            Password = "123@host.local",
            CharacterSet = Vpcp.Kernel.Models.Configurations.MySqlConfiguration.Instance().Charset.Utf8Mb4
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