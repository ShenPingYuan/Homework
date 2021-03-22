using AutoMapper;
using Homework.DB.Entities;
using Homework.Dtos;
using Homework.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Homework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        public readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        public TeacherController(IMapper mapper, ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        // GET: api/<Teacher>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Teacher>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Teacher>
        [HttpPost("initteacher")]
        public async Task<IActionResult> Post([FromBody] InitTeacherDto dto)
        {
            dto.CourseName = "语文";
            var teacher = _teacherRepository.LoadEntities(x => x.TeacherId == dto.Sub).FirstOrDefault();
            if (teacher != null)
            {
                return NoContent();
            }
            Course course;
            string teacherId;
            string teacherName;
            if (dto.CourseName == "数学")
            {
                teacherId = "20170601";
                teacherName = "张三";
                course = new Course
                {
                    CourseId = "KC1001",
                    CourseName = "语文",
                    TeacherId=teacherId
                };
            }
            else
            {
                teacherId = "20170602";
                teacherName = "李四";
                course = new Course
                {
                    CourseId = "KC1002",
                    CourseName = "数学",
                    TeacherId=teacherId
                };
            }
            teacher = new Teacher
            {
                Sub = dto.Sub,
                Course = course,
                TeacherId = teacherId,
                CourseId = course.CourseId,
                TeacherName = teacherName,

            };
            await _teacherRepository.AddEntityAsync(teacher);
            return Ok();
        }

        // PUT api/<Teacher>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string sub, string courseName)
        {
            var teacher = _teacherRepository.LoadEntities(x => x.TeacherId == sub.ToString()).FirstOrDefault();
            var course = _courseRepository.LoadEntities(x => x.CourseName == courseName).FirstOrDefault();
            if (teacher != null)
            {
                return NoContent();
            }
            if (course == null)
            {
                return BadRequest();
            }
            string teacherId;
            string teacherName;
            if (course.CourseName == "语文")
            {
                teacherId = "20170601";
                teacherName = "张三";

            }
            else
            {
                teacherId = "20170602";
                teacherName = "李四";
            }
            teacher = new Teacher
            {
                Sub = sub.ToString(),
                Course = course,
                TeacherId = teacherId,
                CourseId = course.CourseId,
                TeacherName = teacherName
            };
            await _teacherRepository.AddEntityAsync(teacher);
            return Ok();
        }

        // DELETE api/<Teacher>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
