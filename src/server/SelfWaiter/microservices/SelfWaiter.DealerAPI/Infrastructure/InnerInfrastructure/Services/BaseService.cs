using System.Linq.Expressions;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.Shared.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Services;
using SelfWaiter.Shared.Core.Domain.Dtos;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Services
{
    public abstract class BaseService<D, T>(IBaseRepository<D> _baseRepository) : IBaseService<D, T> where D : class, IEntity where T : class, IDto
    {
        public async Task<bool> AnyAsync(Expression<Func<D, bool>> exp)
        {
            return await _baseRepository.AnyAsync(exp);
        }

        public async Task CreateAsync(T dto)
        {
            var entity = ObjectMapper.Mapper.Map<D>(dto);
            await _baseRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(T dto)
        {
            var entity = ObjectMapper.Mapper.Map<D>(dto);
            await _baseRepository.DeleteAsync(entity);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<D, bool>> exp, bool tracking = true)
        {
            var entity = await _baseRepository.FirstOrDefaultAsync(exp, tracking);

            if (entity == null) return null;

            var dto = ObjectMapper.Mapper.Map<T>(entity);

            return dto;
        }

        public async Task<List<T>> GetAllAsync(bool tracking = true)
        {
            var entities = await _baseRepository.GetAllAsync(tracking); 

            var dtos = ObjectMapper.Mapper.Map<List<T>>(entities);

            return dtos;
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            var entity = await _baseRepository.GetByIdAsync(id, tracking);

            if (entity == null) return null;

            var dto = ObjectMapper.Mapper.Map<T>(entity);

            return dto;
        }

        public async Task RemoveAsync(T dto)
        {
            var entity = ObjectMapper.Mapper.Map<D>(dto);
            await _baseRepository.DeleteAsync(entity);
        }

        public async Task UpdateAsync(T dto)
        {
            var entity = ObjectMapper.Mapper.Map<D>(dto);
            await _baseRepository.UpdateAsync(entity);
        }

        public IQueryable<T> Where(Expression<Func<D, bool>> exp)
        {
            var queryableEntity = _baseRepository.Where(exp);

            var queryableDto = ObjectMapper.Mapper.ProjectTo<T>(queryableEntity);

            return queryableDto;
        }
    }
}
