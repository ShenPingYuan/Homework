using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Repository
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
