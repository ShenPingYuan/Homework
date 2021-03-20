using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Repository
{
    public class StudentCourseRepository : BaseRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
