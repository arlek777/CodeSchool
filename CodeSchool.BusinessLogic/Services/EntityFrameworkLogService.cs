using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class EntityFrameworkLogService: ILogService
    {
        private readonly IGenericRepository _repository;

        public EntityFrameworkLogService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task Log(Log log)
        {
            _repository.Add(log);
            await _repository.SaveChanges();
        }

        public async Task<IEnumerable<Log>> Get()
        {
            return (await _repository.GetAll<Log>()).ToList();
        }
    }
}
