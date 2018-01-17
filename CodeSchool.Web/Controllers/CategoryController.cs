using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [ResponseCache(CacheProfileName = "StaticContent")]
    [Route("api/[controller]")]
    public class CategoryController: Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public CategoryController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var categories = await _crudService.GetAll<Category>();
            return Ok(categories.Select(c => new { c.Id, c.Title }));
        }
    }
}
