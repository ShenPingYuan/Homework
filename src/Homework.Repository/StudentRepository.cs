using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
