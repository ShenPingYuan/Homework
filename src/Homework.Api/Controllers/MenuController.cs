using Homework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Homework.Api.Controllers
{
    [Route("api/menus")]
    [ApiController]
    //[Authorize]
    public class MenuController : ControllerBase
    {
        // GET: api/<MenuController>
        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {

            var menulist = new List<MenuItem>
            {
                new MenuItem
                {
                    Id="125",
                    AuthName= "用户管理",
                    Path="users",
                    Children=new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id="125",
                            AuthName= "用户管理",
                            Path="users",
                            Children=null,
                            Order=null
                        }
                    },
                    Order=1
                },
                new MenuItem
                {
                    Id="103",
                    AuthName= "权限管理",
                    Path="rights",
                    Children=new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id="111",
                            AuthName= "角色列表",
                            Path="roles",
                            Children=null,
                            Order=null
                        },
                        new MenuItem
                        {
                            Id="112",
                            AuthName= "权限列表",
                            Path="rights",
                            Children=null,
                            Order=null
                        }
                    },
                    Order=2
                },
                new MenuItem
                {
                    Id="101",
                    AuthName= "老师作业管理",
                    Path="homeworkManager",
                    Children=new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id="104",
                            AuthName= "作业列表",
                            Path="homeworkManager",
                            Children=null,
                            Order=1
                        }
                    },
                    Order=3
                },
                new MenuItem
                {
                    Id="102",
                    AuthName= "学生做作业",
                    Path="doHomework",
                    Children=new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id="107",
                            AuthName= "作业列表",
                            Path="doHomework",
                            Children=null,
                            Order=null
                        }
                    },
                    Order=4
                },
                new MenuItem
                {
                    Id="145",
                    AuthName= "数据统计",
                    Path="reports",
                    Children=new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id="146",
                            AuthName= "作业完成情况",
                            Path="reports",
                            Children=null,
                            Order=null
                        }
                    },
                    Order=5
                },
            };
            return menulist;
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MenuController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MenuController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MenuController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
