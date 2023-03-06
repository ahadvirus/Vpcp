using System.Threading.Tasks;
using Vpcp.Kernel.Databases.Repositories.Persistence;

namespace Vpcp.Kernel.Models.Contracts
{
    public interface ISeed<TEntity, out TKey> where TKey : struct where TEntity : class, IEntity<TKey>
    {
        Task Invoke(IRepository<TEntity, TKey> repository);
    }
}
