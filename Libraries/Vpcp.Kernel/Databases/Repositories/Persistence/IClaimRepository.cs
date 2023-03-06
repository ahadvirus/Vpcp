using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Databases.Repositories.Persistence;

public interface IClaimRepository<TEntity, in TKey> : IRepository<TEntity, TKey> where TKey : struct where TEntity : IClaim<TKey>
{
    Task<List<TEntity>> GetClaimByKeyAsync(string key, CancellationToken? cancellation = null);
}