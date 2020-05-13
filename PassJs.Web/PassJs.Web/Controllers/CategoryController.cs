using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.BusinessLogic;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace PassJs.Web.Controllers
{
    [ResponseCache(CacheProfileName = "StaticContent")]
    [Route("api/[controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly IMemoryCache _memoryCache;
        private const string CacheCategoriesKey = "Categories";

        public CategoryController(ISimpleCRUDService crudService, IMemoryCache memoryCache)
        {
            _crudService = crudService;
            _memoryCache = memoryCache;
        }

        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            if (!_memoryCache.TryGetValue(CacheCategoriesKey, out IEnumerable<object> categories))
            {
                categories = (await _crudService.GetAll<Category>()).Select(c => new {c.Id, c.Title});
                _memoryCache.Set(CacheCategoriesKey, categories, TimeSpan.FromDays(30));
            }

            return Ok(categories);
        }
    }
}
