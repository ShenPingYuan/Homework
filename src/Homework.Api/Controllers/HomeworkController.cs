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
    public class HomeworkController : ControllerBase
    {
        public readonly IHomeworkRepository _homeworkRepository;
        private readonly IMapper _mapper;

        public HomeworkController(
            IHomeworkRepository homeworkRepository,
            IMapper mapper)
        {
            _homeworkRepository = homeworkRepository ?? throw new ArgumentNullException(nameof(homeworkRepository));
            _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        // GET: api/<HomeworkController>
        [HttpGet("api/homeworks")]
        public ActionResult<IEnumerable<DB.Entities.Homework>> Get()
        {
            var hws = _homeworkRepository.GetAllEntities().ToList();
            var dto = _mapper.Map<IEnumerable<HomeworkDto>>(hws);
            return Ok(dto);
        }

        // GET api/<HomeworkController>/5
        [HttpGet("api/homeworks/{id}")]
        public ActionResult<IEnumerable<DB.Entities.Homework>> Get(int id)
        {
            var hws = _homeworkRepository.LoadEntities(x=>x.HomeworkId==id.ToString()).ToList();
            var dto = _mapper.Map<IEnumerable<HomeworkDto>>(hws);
            return Ok(dto);
        }
        // POST api/<HomeworkController>
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] HomeworkDto dto)
        {
            if(dto==null)
            {
                return BadRequest();
            }
            var homework = _mapper.Map<DB.Entities.Homework>(dto);
            var homeworkResult = await _homeworkRepository.AddEntityAsync(homework);
            if(homeworkResult!=null)
            {
                return Conflict();
            }
            return CreatedAtAction("Get", new { id = homeworkResult.HomeworkId }, homeworkResult);
        }

        // PUT api/<HomeworkController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeworkController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
