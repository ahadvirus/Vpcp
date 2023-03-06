using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vpcp.Kernel.Models.Entities;

namespace Vpcp.Kernel.Databases.Repositories.Persistence;

public interface IAdminRepository : IClaimRepository<Admin, Guid>
{
    Task<List<Admin>> GetAdminByIdAsync(Guid id, CancellationToken? cancellation = null);

    Task<IEnumerable<IGrouping<Guid, Admin>>> GetAdminsByNameAsync(string name,
        CancellationToken? cancellation = null);
}