using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace PassJs.Web.Infrastructure.Services
{
    public class FileLogService: ILogService
    {
        private readonly string _logFilePath;

        public FileLogService(IWebHostEnvironment env)
        {
            _logFilePath = env.ContentRootPath + "/logs.txt";
        }

        public async Task Log(Log log)
        {
            var logs = new List<Log>();
            if (File.Exists(_logFilePath))
            {
                var logsText = File.ReadAllText(_logFilePath);
                logs = JsonConvert.DeserializeObject<List<Log>>(logsText);
            }

            logs.Add(log);
            logs = logs.OrderByDescending(l => l.TimeStamp).ToList();

            var result = JsonConvert.SerializeObject(logs);
            File.WriteAllText(_logFilePath, result);
        }
    }
}
