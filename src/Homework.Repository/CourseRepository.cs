using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;

namespace Homework.Repository
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
