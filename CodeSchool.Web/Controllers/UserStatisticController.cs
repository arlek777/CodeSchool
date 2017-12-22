using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeSchool.DataAccess;
using CodeSchool.Domain;
using CodeSchool.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserStatisticController: Controller
    {
        private readonly IGenericRepository _repository;

        public UserStatisticController(IGenericRepository repository)
        {
            _repository = repository;
        }

        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var users = await _repository.GetAll<User>();
            return Ok(users.Select(Mapper.Map<UserStatisticModel>));
        }
    }
}
