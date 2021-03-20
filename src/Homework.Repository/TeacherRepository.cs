using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;

namespace Homework.Repository
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
