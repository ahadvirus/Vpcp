using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Databases.Repositories.Presentations;

public class ClaimRepository<TEntity, TKey> : Repository<TEntity, TKey>, IClaimRepository<TEntity, TKey> where TKey : struct where TEntity : class, IClaim<TKey>
{
    public ClaimRepository(Func<CancellationToken, Task<int>> saveChangesAsync, DbSet<TEntity>? set = null) : base(saveChangesAsync, set)
    {
    }

    public async Task<List<TEntity>> GetClaimByKeyAsync(string key, CancellationToken? cancellation = null)
    {
        return await Query()
            .Where(claim => string.Compare(claim.Key, key, StringComparison.CurrentCultureIgnoreCase) == 0)
            .ToListAsync(GetCancellationToken(cancellation));
    } 
}