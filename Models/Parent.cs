using System;
using System.Collections.Generic;

namespace AlIzza.API.Models
{
    public class Parent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;       // Enforced unique at database level
        public string PhoneNumber { get; set; } = string.Empty; // Enforced unique for "Forgot Email" recovery

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}