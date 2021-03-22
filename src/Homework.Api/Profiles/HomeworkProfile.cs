using AutoMapper;
using Homework.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.Api.Profiles
{
    public class HomeworkProfile : Profile
    {
        public HomeworkProfile()
        {
            CreateMap<DB.Entities.Homework, HomeworkDto>();
            CreateMap<HomeworkDto, DB.Entities.Homework>();
        }
    }
}
