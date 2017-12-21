using System;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.LessonsServices
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
    }
}