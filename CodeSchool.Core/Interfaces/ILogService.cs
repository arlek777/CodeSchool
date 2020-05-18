using System.Threading.Tasks;
using PassJs.DomainModels;

namespace PassJs.Core.Interfaces
{
    public interface ILogService
    {
        Task Log(Log log);
    }
}