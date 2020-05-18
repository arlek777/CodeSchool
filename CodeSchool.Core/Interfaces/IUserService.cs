using System;
using System.Threading.Tasks;
using PassJs.DomainModels;

namespace PassJs.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateNew(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task RemoveUserToken(Token token);
        Task<Token> GetUserToken(Guid token);
    }
}