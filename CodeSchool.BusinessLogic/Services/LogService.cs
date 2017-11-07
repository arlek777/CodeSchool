using System.Threading.Tasks;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class LogService: ILogService
    {
        private readonly IGenericRepository _repository;

        public LogService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task Log(Log log)
        {
            _repository.Add(log);
            await _repository.SaveChanges();
        }
    }

    public interface ILogService
    {
        Task Log(Log log);
    }
}
