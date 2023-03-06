using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Databases.Repositories.Presentations;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TKey : struct where TEntity : class, IEntity<TKey> 
{
    protected Func<CancellationToken, Task<int>> SaveChangesAsync { get; }
    protected DbSet<TEntity> Set { get; }

    public Repository(Func<CancellationToken, Task<int>> saveChangesAsync, DbSet<TEntity>? set = null)
    {
        SaveChangesAsync = saveChangesAsync;
            
        if (set == null)
        {
            throw new Exception(string.Empty);
        }

        Set = set;
    }

    public IQueryable<TEntity> Query()
    {
        return Set;
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken? cancellation = null)
    {
        return await Query().FirstOrDefaultAsync(entity => entity.Id.Equals(id), GetCancellationToken(cancellation));
    }

    public async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken? cancellation = null)
    {
        Set.Add(entity);

        if (await SaveChangesAsync(GetCancellationToken(cancellation)) == 0)
        {
            return null;
        }

        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken? cancellation = null)
    {
        Set.Update(entity);

        return await SaveChangesAsync(GetCancellationToken(cancellation)) > 0;
    }

    public async Task<bool> DeleteAsync(TKey id, CancellationToken? cancellation = null)
    {
        bool result = false;

        TEntity? entity = await GetByIdAsync(id);

        if (entity == null)
        {
            return result;
        }

        Set.Remove(entity);

        result = await SaveChangesAsync(GetCancellationToken(cancellation)) > 0;

        return result;
    }

    protected virtual CancellationToken GetCancellationToken(CancellationToken? cancellation)
    {
        CancellationToken result;

        if (cancellation != null)
        {
            result = cancellation.Value;
        }
        else
        {
            CancellationTokenSource token =  new CancellationTokenSource();
            result = token.Token;
        }

        return result;
    }
}