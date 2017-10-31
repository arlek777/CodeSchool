using System.Linq;
using System.Threading.Tasks;
using CodeSchool.DataAccess;
using CodeSchool.Domain;

namespace CodeSchool.BusinessLogic.Services
{
    public class LessonService : ILessonService
    {
        private readonly IGenericRepository _repository;
        private readonly IChapterService _chapterService;

        public LessonService(IGenericRepository repository, IChapterService chapterService)
        {
            _repository = repository;
            _chapterService = chapterService;
        }

        public async Task<Lesson> Get(int id)
        {
            return await _repository.Find<Lesson>(l => l.Id == id);
        }

        public async Task<Lesson> GetNext(int chapterId, int id)
        {
            var chapter = await _chapterService.Get(chapterId);
            var lessons = chapter.Lessons.OrderBy(l => l.Order).ToList();

            var currentIndex = lessons.FindIndex(l => l.Id == id);
            if (currentIndex == -1) return null;

            var nextIndex = ++currentIndex;

            if (nextIndex == lessons.Count)
            {
                chapter = await _chapterService.GetNext(chapterId);
                var lesson = chapter.Lessons.OrderBy(l => l.Order).FirstOrDefault();
                return lesson;
            }

            return lessons[nextIndex];
        }

        public async Task<Lesson> AddOrUpdate(Lesson model)
        {
            var lesson = await Get(model.Id);
            if (lesson == null)
            {
                model.Order = await GetNextOrder(model.ChapterId);
                _repository.Add(model);
                lesson = model;
            }
            else
            {
                lesson.ReporterCode = model.ReporterCode;
                lesson.UnitTestsCode = model.UnitTestsCode;
                lesson.StartCode = model.StartCode;
                lesson.Text = model.Text;
                lesson.Title = model.Title;
            }
           
            await _repository.SaveChanges();
            return lesson;
        }

        public async Task ChangeOrder(int currentLessonId, int toSwapLessonId)
        {
            var currentLesson = await Get(currentLessonId);
            var toSwapLesson = await Get(toSwapLessonId);

            var toSwapOrder = toSwapLesson.Order;
            toSwapLesson.Order = currentLesson.Order;
            currentLesson.Order = toSwapOrder;

            await _repository.SaveChanges();
        }

        public async Task Remove(int id)
        {
            var lesson = await Get(id);
            _repository.Remove(lesson);
            await _repository.SaveChanges();
        }

        private async Task<int> GetNextOrder(int chapterId)
        {
            var chapter = await _chapterService.Get(chapterId);
            var lastLesson = chapter.Lessons.OrderBy(l => l.Order).LastOrDefault();

            return lastLesson == null ? 0 : ++lastLesson.Order;
        }
    }
}