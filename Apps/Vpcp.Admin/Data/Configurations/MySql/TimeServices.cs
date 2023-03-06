using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace Vpcp.Admin.Data.Configurations.MySql
{
    public class TimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection service)
        {
            service.AddEntityFrameworkMySQL();
            new EntityFrameworkRelationalDesignServicesBuilder(service)
                .TryAddCoreServices();
        }
    }
}
