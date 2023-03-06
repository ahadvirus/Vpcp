using System.Linq;
using Vpcp.Kernel.Models.DataObjects;

namespace Vpcp.Kernel.Services.Contracts;

public interface IAdminService
{
    IQueryable<AdminDTO> Query();


}