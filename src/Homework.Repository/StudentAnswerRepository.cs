using Homework.Data;
using Homework.DB.Entities;
using Homework.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.IRepository
{
    public class StudentAnswerRepository : BaseRepository<StudentAnswer>, IStudentAnswerRepository
    {
        public StudentAnswerRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
