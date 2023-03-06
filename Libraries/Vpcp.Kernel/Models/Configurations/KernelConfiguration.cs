using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Databases.Contexts;

namespace Vpcp.Kernel.Models.Configurations;

public class KernelConfiguration
{
    public KernelConfiguration()
    {
        KernelContextOptions = new DbContextOptionsBuilder<KernelContext>();
    }

    public DbContextOptionsBuilder<KernelContext> KernelContextOptions { get; }
}