using Microsoft.EntityFrameworkCore;

namespace Vpcp.Panel.Api.Kernel.Databases.Contexts;

public class MemoryContext : DbContext
{
    public MemoryContext(DbContextOptions<MemoryContext> options) : base(options)
    {
    }
}