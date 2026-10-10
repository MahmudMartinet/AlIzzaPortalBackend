using AlIzza.API.Models;

namespace AlIzza.API.DTOs
{
    public class RegisterParentDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class SubmitStudentApplicationDto
    {
        public Guid ParentId { get; set; }
        public Guid SchoolId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
         public AdmissionStatus AdmissionStatus { get; set; } = AdmissionStatus.Pending; // Pending, Approved, etc.
        public string ClassApplyingFor { get; set; } = string.Empty;
    }

    public class StudentResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public AdmissionStatus AdmissionStatus { get; set; } = AdmissionStatus.Pending; // Pending, Approved, etc.
        public string? SchoolNumber { get; set; }
        public string ClassApplyingFor { get; set; } = string.Empty;
        public Guid SchoolId { get; set; }
        public Guid ParentId { get; set; }
    }
}