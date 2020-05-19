using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassJs.Core.Interfaces;
using PassJs.DataAccess;
using PassJs.DomainModels;

namespace PassJs.Core.Services
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
