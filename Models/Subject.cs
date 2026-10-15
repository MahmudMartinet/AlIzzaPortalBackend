using System.ComponentModel.DataAnnotations;
using AlIzza.API.Models;

namespace AlIzza.API.Models
{
    public class Subject
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // e.g., Mathematics, Arabic

        [StringLength(20)]
        public string Code { get; set; } = string.Empty; // e.g., MATH101, ARB101

        [Required]
        public Guid AcademicClassId { get; set; }
        public AcademicClass? AcademicClass { get; set; }

        // Assigned Teacher for the Subject
        public Guid? TeacherId { get; set; }
        public Staff? Teacher { get; set; }
    }
}