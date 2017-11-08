using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using JWT.Serializers;
using Microsoft.AspNetCore.Hosting;

namespace CodeSchool.Web.Infrastructure
{
    public class FileLogService: ILogService
    {
        private readonly string _logFilePath;

        public FileLogService(IHostingEnvironment env)
        {
            _logFilePath = env.ContentRootPath + "/logs.txt";
        }

        public async Task Log(Log log)
        {
            File.AppendAllText(_logFilePath, new JsonNetSerializer().Serialize(log) + ",");
        }
    }
}
