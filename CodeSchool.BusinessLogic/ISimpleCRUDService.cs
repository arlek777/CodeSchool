using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic
{
    public interface ISimpleCRUDService
    {
        Task<ICollection<TEntity>> GetAll<TEntity>()
            where TEntity : class, ISimpleEntity;

        Task<ICollection<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ISimpleEntity;

        Task<TEntity> GetById<TEntity>(int id)
            where TEntity : class, ISimpleEntity;

        Task<TEntity> CreateOrUpdate<TEntity>(TEntity entity, Action<TEntity, TEntity> updateFunc)
            where TEntity : class, ISimpleEntity;

        Task RemoveById<TEntity>(int id)
            where TEntity : class, ISimpleEntity;
    }
}