using System;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateNew(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
    }
}