using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepository<T, TEntity> where T : class where TEntity : class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T vehicle);
        Task<int> UpdateAsync(T vehicle);
        Task<int> RemoveAsync(Guid id);
    }
}
