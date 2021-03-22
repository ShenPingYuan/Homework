using AutoMapper;
using Homework.Data;
using Homework.DB.Entities;
using Homework.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly ApplicationDbContext _context;

        public CourseController(IMapper mapper, ICourseRepository courseRepository, ApplicationDbContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _courseRepository=courseRepository ?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
        }
        // POST api/<Teacher>
        [HttpGet]
        public async Task<IActionResult> Post()
        {
            var course = new List<Course>
            {
                new Course
                {
                    CourseId="KC1001",
                    CourseName="语文",
                },
                new Course
                {
                    CourseId="KC1002",
                    CourseName="数学",
                }
            };
            await _context.AddRangeAsync(course);
            _context.SaveChanges();
            return Ok();
        }
    }
}
