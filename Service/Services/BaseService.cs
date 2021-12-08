using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");
            validator.ValidateAndThrow(obj);
        }
        async Task<TEntity> IBaseService<TEntity>.AddAsync<TValidator>(TEntity obj)
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.InsertAsync(obj);
            return obj;
        }

        async Task IBaseService<TEntity>.DeleteAsync(int id)
       => await _baseRepository.DeleteAsync(id);

        async Task<IList<TEntity>> IBaseService<TEntity>.GetAllAsync()
       => await _baseRepository.GetAllAsync();

        async Task<TEntity> IBaseService<TEntity>.GetByIdAsync(int id)
        => await _baseRepository.GetAsync(id);
          
        async Task<TEntity> IBaseService<TEntity>.UpdateAsync<TValidator>(TEntity obj)
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.UpdateAsync(obj);
            return obj;
        }
    }
}
