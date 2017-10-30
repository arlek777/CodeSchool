using System;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess.Services
{
    public interface IUserService
    {
        Task<User> CreateNew(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
    }
}