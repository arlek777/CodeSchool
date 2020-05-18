using System;
using System.Threading.Tasks;
using PassJs.Core.Interfaces;
using PassJs.DataAccess;
using PassJs.DomainModels;

namespace PassJs.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository _repository;

        public UserService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> CreateNew(User user)
        {
            _repository.Add(user);
            await _repository.SaveChanges();

            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _repository.Find<User>(u => u.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _repository.Find<User>(u => u.Email == email);
        }

        public async Task RemoveUserToken(Token token)
        {
            _repository.Remove(token);
            await _repository.SaveChanges();
        }

        public async Task<Token> GetUserToken(Guid token)
        {
            var dbToken = await _repository.Find<Token>(t => t.TokenValue == token);
            if (dbToken == null || dbToken.CreatedDt.AddDays(dbToken.LifetimeInDays) < DateTime.UtcNow)
            {
                return null;
            }

            return dbToken;
        }
    }
}