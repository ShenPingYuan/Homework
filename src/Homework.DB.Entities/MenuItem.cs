using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Entities
{
    public class MenuItem
    {
        public string Id { get; set; }
        public string AuthName { get; set; }
        public string Path { get; set; }
        public ICollection<MenuItem> Children { get; set; }
        public int? Order { get; set; }
    }
}
