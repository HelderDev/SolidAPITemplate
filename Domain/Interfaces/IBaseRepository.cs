using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task InsertAsync(TEntity obj);

        Task UpdateAsync(TEntity obj);

        Task DeleteAsync(int id);

        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);
    }
}
