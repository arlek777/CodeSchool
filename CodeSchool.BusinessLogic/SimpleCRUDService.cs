using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic
{
    public class SimpleCRUDService : ISimpleCRUDService
    {
        private readonly IGenericRepository _repository;

        public SimpleCRUDService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<TEntity>> GetAll<TEntity>()
            where TEntity : class, ISimpleEntity
        {
            return await _repository.GetAll<TEntity>();
        }

        public async Task<ICollection<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ISimpleEntity
        {
            return await _repository.Where<TEntity>(predicate);
        }

        public async Task<TEntity> GetById<TEntity>(int id) 
            where TEntity : class, ISimpleEntity
        {
            return await _repository.Find<TEntity>(l => l.Id == id);
        }

        public async Task<TEntity> CreateOrUpdate<TEntity>(TEntity entity, Action<TEntity, TEntity> updateFunc)
            where TEntity : class, ISimpleEntity
        {
            var dbEntity = await GetById<TEntity>(entity.Id);
            if (dbEntity == null)
            {
                _repository.Add(entity);
            }
            else
            {
                updateFunc(dbEntity, entity);
            }

            await _repository.SaveChanges();
            return dbEntity;
        }

        public async Task RemoveById<TEntity>(int id)
            where TEntity : class, ISimpleEntity
        {
            var entity = await GetById<TEntity>(id);
            _repository.Remove(entity);
            await _repository.SaveChanges();
        }
    }
}
