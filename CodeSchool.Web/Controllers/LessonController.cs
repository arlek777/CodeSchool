using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CodeSchool.Web.Controllers
{
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public LessonController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [Route("get")]
        [HttpGet]
        public IActionResult GetLesson()
        {
            var text = @"Программы на языке JavaScript можно вставить в любое место HTML при помощи тега SCRIPT. Этот пример использует следующие элементы:
                       < script > ... </ script >
                Тег script содержит исполняемый код. Предыдущие стандарты HTML требовали обязательного указания атрибута type, но сейчас он уже не нужен.Достаточно просто<script>.
            Браузер, когда видит < script >:
            Начинает отображать страницу, показывает часть документа до script
            Встретив тег script, переключается в JavaScript-режим и не показывает, а исполняет его содержимое.
                Закончив выполнение, возвращается обратно в HTML - режим и только тогда отображает оставшуюся часть документа.
                Попробуйте этот пример в действии, и вы сами всё увидите.
                alert(сообщение)
            Отображает окно с сообщением и ждёт, пока посетитель не нажмёт «Ок».";

            var code = " alert( 'Привет, Мир!' );";

            var unitTestCode = System.IO.File.ReadAllText(_environment.ContentRootPath + "/LessonUnitTests/1/1.js");

            return Ok(new LessonModel()
            {
                Id = 1,
                ChapterId = 1,
                Text = text,
                Code = code,
                UnitTestCode = unitTestCode
            });
        }
    }

    public class LessonModel
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public string UnitTestCode { get; set; }
    }
}
