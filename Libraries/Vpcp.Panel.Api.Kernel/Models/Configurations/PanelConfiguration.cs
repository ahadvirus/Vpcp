using Microsoft.EntityFrameworkCore;
using Vpcp.Panel.Api.Kernel.Databases.Contexts;

namespace Vpcp.Panel.Api.Kernel.Models.Configurations
{
    public class PanelConfiguration
    {
        public PanelConfiguration()
        {
            IdentityOptions = new DbContextOptionsBuilder<IdentityContext>();

            MemoryOptions = new DbContextOptionsBuilder<MemoryContext>();

            //MemoryOptions.UseMemoryCache()
        }
        
        public DbContextOptionsBuilder<IdentityContext> IdentityOptions { get; }

        public DbContextOptionsBuilder<MemoryContext> MemoryOptions { get; }
    }
}
