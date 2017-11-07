using CodeSchool.BusinessLogic.Interfaces;
using CodeSchool.DataAccess;
using CodeSchool.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository _repository;
        IUserLessonService _service;
        private readonly ILessonService _lService;

        public HomeController(IGenericRepository repository, IUserLessonService service, ILessonService lService)
        {
            _repository = repository;
            _service = service;
            _lService = lService;
        }

        public async Task<IActionResult> Index()
        {
            //try
            //{

            //    var user = new User()
            //    {
            //        Email = "test",
            //        UserName = "tesUs",
            //        Password = "123"
            //    };
            //    _repository.Add(user);
            //    await _repository.SaveChanges();

            //    var user2 = new User()
            //    {
            //        Email = "test",
            //        UserName = "tesUs",
            //        Password = "123"
            //    };
            //    _repository.Add(user2);
            //    await _repository.SaveChanges();

            //    var chapter = new Chapter()
            //    {
            //        Order = 0,
            //        Title = "Test Chapter"
            //    };
            //    _repository.Add(chapter);
            //    await _repository.SaveChanges();

            //    var lesson = new Lesson()
            //    {
            //        ChapterId = chapter.Id,
            //        Order = 0,
            //        ReporterCode = "123",
            //        StartCode = "123",
            //        Text = "rtr",
            //        Title = "dfdf",
            //        UnitTestsCode = "dfdf"
            //    };
            //    var lesson2 = new Lesson()
            //    {
            //        ChapterId = chapter.Id,
            //        Order = 0,
            //        ReporterCode = "123",
            //        StartCode = "123",
            //        Text = "rtr",
            //        Title = "dfdf",
            //        UnitTestsCode = "dfdf"
            //    };
            //    _repository.Add(lesson);
            //    _repository.Add(lesson2);
            //    await _repository.SaveChanges();

            //    var userChapter = new UserChapter()
            //    {
            //        ChapterId = chapter.Id,
            //        UserId = user.Id
            //    };
            //    _repository.Add(userChapter);
            //    await _repository.SaveChanges();

            //    var userLesson = new UserLesson()
            //    {
            //        LessonId = lesson.Id,
            //        UserChapterId = userChapter.Id,
            //        Code = "3434",
            //        UserId = user.Id,
            //        UpdatedDt = DateTime.UtcNow
            //    };

            //    var userLesson2 = new UserLesson()
            //    {
            //        LessonId = lesson.Id,
            //        UserChapterId = userChapter.Id,
            //        Code = "3434",
            //        UserId = user2.Id,
            //        UpdatedDt = DateTime.UtcNow
            //    };
            //    _repository.Add(userLesson);
            //    _repository.Add(userLesson2);
            //    await _repository.SaveChanges();

            //    await _service.Remove(lesson.Id);
            //    await _lService.Remove(lesson.Id);

            //    _repository.Remove(userLesson);
            //    await _repository.SaveChanges();
            //    _repository.Remove(lesson);
            //    await _repository.SaveChanges();

            //    _repository.Remove(userLesson2);
            //    await _repository.SaveChanges();
            //    _repository.Remove(lesson2);
            //    await _repository.SaveChanges();

            //    _repository.Remove(userChapter);
            //    await _repository.SaveChanges();
            //    _repository.Remove(chapter);
            //    await _repository.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    var m = e.Message;
            //}

            return View();
        }

        public IActionResult Test()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
