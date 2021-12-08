using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
        Task DeleteAsync(int id);

        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> UpdateAsync<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
    }

}
