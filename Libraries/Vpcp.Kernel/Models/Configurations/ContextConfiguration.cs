using Microsoft.EntityFrameworkCore;

namespace Vpcp.Kernel.Models.Configurations;

public class ContextConfiguration<T> where T : DbContext
{
    public ContextConfiguration()
    {
        StringConnection = string.Empty;
    }

    public string StringConnection { get; set; }
    public DatabaseProvider DatabaseProvider { get; set; }
}