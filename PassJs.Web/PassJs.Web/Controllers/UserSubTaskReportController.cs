using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.DataAccess;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassJs.Web.Models.UserReport;

namespace PassJs.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserSubTaskReportController: ControllerBase
    {
        private readonly IGenericRepository _repository;

        public UserSubTaskReportController(IGenericRepository repository)
        {
            _repository = repository;
        }

        [Route("[action]")]
        public async Task<IActionResult> GetUserReports(Guid companyId)
        {
            var userTaskHeads = await _repository.Where<UserTaskHead>(c => c.User.CompanyId == companyId && !c.User.IsAdmin);
            return Ok(userTaskHeads.OrderByDescending(c => c.CreatedDt).ToList().Select(Mapper.Map<UserTaskHeadReportModel>));
        }

        [Route("[action]")]
        public async Task<IActionResult> GetDetailedUserReport(Guid companyId, string userEmail)
        {
            var userEmailDecoded = WebUtility.UrlDecode(userEmail);
            var userTaskHeads = await _repository.Where<UserTaskHead>(c =>
                c.User.CompanyId == companyId && !c.User.IsAdmin && c.User.Email == userEmailDecoded);

            return Ok(userTaskHeads.ToList().Select(Mapper.Map<UserTaskHeadDetailedReportModel>));
        }
    }
}
