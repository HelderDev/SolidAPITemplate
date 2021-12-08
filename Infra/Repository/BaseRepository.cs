using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : BaseEntity
    {
        protected readonly MSQLServerContext _mSQLServerContext;
        public BaseRepository(MSQLServerContext mSQLServerContext)
        {
            _mSQLServerContext = mSQLServerContext;
        }
        async Task IBaseRepository<TEntity>.DeleteAsync(int id)
        {
            _mSQLServerContext.Set<TEntity>().Remove(await GetAsync(id));
            await _mSQLServerContext.SaveChangesAsync();
        }


        async Task IBaseRepository<TEntity>.InsertAsync(TEntity obj)
        {
            await _mSQLServerContext.Set<TEntity>().AddAsync(obj);
            await _mSQLServerContext.SaveChangesAsync();
        }

        async Task IBaseRepository<TEntity>.UpdateAsync(TEntity obj)
        {
            _mSQLServerContext.Entry(obj).State = EntityState.Modified;
            await _mSQLServerContext.SaveChangesAsync();
        }
        public async Task<IList<TEntity>> GetAllAsync() =>
          await _mSQLServerContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity> GetAsync(int id) =>
          await _mSQLServerContext.Set<TEntity>().FindAsync(id);
    }
}
