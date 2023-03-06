using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Models.DataObjects;
using Vpcp.Kernel.Models.Entities;

namespace Vpcp.Kernel.Databases.Repositories.Presentations;

public class AdminRepository : ClaimRepository<Admin, Guid>, IAdminRepository
{
    public AdminRepository(Func<CancellationToken, Task<int>> saveChangesAsync, DbSet<Admin> set) : base(saveChangesAsync, set)
    {
    }

    public async Task<List<Admin>> GetAdminByIdAsync(Guid id, CancellationToken? cancellation = null)
    {
        return await Query()
            .Where(admin => admin.UserId == id)
            .ToListAsync(GetCancellationToken(cancellation));
    }

    public async Task<IEnumerable<IGrouping<Guid, Admin>>> GetAdminsByNameAsync(string name,
        CancellationToken? cancellation = null)
    {
        List<Guid> adminIds = await Query()
            .Where(admin => admin.Key.Equals(nameof(AdminDTO.Name)))
            .Where(admin => admin.Value.Contains(name))
            .Select(admin => admin.UserId)
            .ToListAsync(GetCancellationToken(cancellation));

            
        return (await Query()
            .Where(admin => adminIds.Contains(admin.UserId))
            .ToListAsync(GetCancellationToken(cancellation)))
            .GroupBy(admin => admin.UserId);
    }
}