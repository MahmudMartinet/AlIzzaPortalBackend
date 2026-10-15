using System.ComponentModel.DataAnnotations;

namespace AlIzza.API.DTOs
{
    // Class DTOs
    public class CreateAcademicClassDto
    {
        [Required(ErrorMessage = "Class name is required.")]
        [StringLength(50, ErrorMessage = "Class name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "School ID is required.")]
        public Guid SchoolId { get; set; }

        public Guid? ClassTeacherId { get; set; }
    }

    public class AcademicClassResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid SchoolId { get; set; }
        public Guid? ClassTeacherId { get; set; }
        public string? Class { get; set; }
        public int TotalSubjects { get; set; }
    }

    // Subject DTOs
    public class CreateSubjectDto
    {
        [Required(ErrorMessage = "Subject name is required.")]
        [StringLength(100, ErrorMessage = "Subject name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject code is required.")]
        [StringLength(20, ErrorMessage = "Subject code cannot exceed 20 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Class ID is required.")]
        public Guid AcademicClassId { get; set; }

        public Guid? TeacherId { get; set; }
    }

    public class SubjectResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid AcademicClassId { get; set; }
        public string? ClassName { get; set; }
        public Guid? TeacherId { get; set; }
        public string? TeacherName { get; set; }
    }
}