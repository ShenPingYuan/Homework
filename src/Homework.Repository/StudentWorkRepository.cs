using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;

namespace Homework.Repository
{
    public class StudentWorkRepository : BaseRepository<StudentWork>, IStudentWorkRepository
    {
        public StudentWorkRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
