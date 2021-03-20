using Homework.Data;
using Homework.IRepository;

namespace Homework.Repository
{
    public class HomeworkRepository : BaseRepository<DB.Entities.Homework>, IHomeworkRepository
    {
        public HomeworkRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
