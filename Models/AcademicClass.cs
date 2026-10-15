using System.ComponentModel.DataAnnotations;


namespace AlIzza.API.Models
{
    public class AcademicClass
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty; // e.g., Primary 1, JSS 1

        [Required]
        public Guid SchoolId { get; set; }
        public School? School { get; set; }

        // Optional Form Teacher / Class Teacher
        public Guid? ClassTeacherId { get; set; }
        public Staff? ClassTeacher { get; set; }

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}