using Vpcp.Kernel.Models.Configurations.MySql;

namespace Vpcp.Kernel.Models.Configurations;

public class MySqlConfiguration
{
    private static MySqlConfiguration? _instance;

    public static MySqlConfiguration Instance()
    {
        if (_instance == null) { _instance = new MySqlConfiguration(); }
        return _instance;
    }

    private MySqlConfiguration()
    {
        Collation = new Collation();
        Charset = new Charset();
    }


    public Collation Collation { get; }
    public Charset Charset { get; }

}