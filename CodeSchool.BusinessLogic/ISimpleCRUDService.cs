using System;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic
{
    public interface ISimpleCRUDService
    {
        Task<TEntity> CreateOrUpdate<TEntity>(TEntity entity, Action<TEntity, TEntity> updateFunc) 
            where TEntity : class, ISimpleEntity;
        Task<TEntity> GetById<TEntity>(int id) where TEntity : class, ISimpleEntity;
        Task Remove<TEntity>(int id) where TEntity : class, ISimpleEntity;
    }
}