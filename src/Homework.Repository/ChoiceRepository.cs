using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Repository
{
    public class ChoiceRepository:BaseRepository<Choice>,IChoiceRepository
    {
        public ChoiceRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
