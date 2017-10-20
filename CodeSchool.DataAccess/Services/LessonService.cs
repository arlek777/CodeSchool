using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CodeSchool.Domain;

namespace CodeSchool.DataAccess.Services
{
    public class LessonService : ILessonService
    {
        private readonly DbContext _dbContext;

        public LessonService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Lesson> Get(int id)
        {
            return await _dbContext.Set<Lesson>().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Lesson> GetNext(int chapterId, int id)
        {
            var chapter = await _dbContext.Set<Chapter>().FirstOrDefaultAsync(c => c.Id == chapterId);
            var lessons = chapter.Lessons.ToList();

            var currentIndex = lessons.FindIndex(l => l.Id == id);
            if (currentIndex == -1) return null;

            var nextIndex = currentIndex++;
            return nextIndex == lessons.Count 
                ? lessons[currentIndex] 
                : lessons[nextIndex];
        }

        public async Task<Lesson> AddOrUpdate(Lesson model)
        {
            var lesson = await Get(model.Id);
            if (lesson == null)
            {
                lesson = _dbContext.Set<Lesson>().Add(model);
            }
            else
            {
                lesson.ReporterCode = model.ReporterCode;
                lesson.UnitTestsCode = model.UnitTestsCode;
                lesson.StartCode = model.StartCode;
                lesson.Text = model.Text;
                lesson.Title = model.Title;
            }
           
            await _dbContext.SaveChangesAsync();
            return lesson;
        }

        public async Task Remove(int id)
        {
            var lesson = await Get(id);
            _dbContext.Set<Lesson>().Remove(lesson);
            await _dbContext.SaveChangesAsync();
        }
    }
}