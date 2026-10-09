using System;
using System.Collections.Generic;

namespace AlIzza.API.Models
{
    public class School
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty; // e.g., Conventional or Tahfiz
        public string SchoolCode { get; set; } = string.Empty; // "CONV" or "TAHF"
        public string Address {get; set;} = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}