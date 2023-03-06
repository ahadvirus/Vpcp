using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Databases.Repositories.Persistence;

public interface IRepository<TEntity, in TKey> where TKey : struct where TEntity : IEntity<TKey>
{
    IQueryable<TEntity> Query();

    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken? cancellation = null);

    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken? cancellation = null);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken? cancellation = null);

    Task<bool> DeleteAsync(TKey id, CancellationToken? cancellation = null);
}