using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using CodeSchool.DataAccess.Services;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lesson = await _lessonService.Get(id);
            return Ok(lesson);
        }

        [Route("[action]/{chapterId}/{id}")]
        public async Task<IActionResult> GetNext(int chapterId, int id)
        {
            var nextLesson = await _lessonService.GetNext(chapterId, id);
            return Ok(nextLesson);
        }
    }
}
