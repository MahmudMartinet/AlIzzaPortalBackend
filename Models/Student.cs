using System;

namespace AlIzza.API.Models
{
    public enum AdmissionStatus
    {
        Pending,
        Active,
        RevertedToPending
    }

    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string ClassApplyingFor { get; set; } = string.Empty;
        // Generated only when Admin approves admission (e.g., 202601)
        public string? SchoolNumber { get; set; } 

// Profile Picture URL
        public string? ProfilePictureUrl { get; set; }
        
        public AdmissionStatus AdmissionStatus { get; set; } = AdmissionStatus.Pending;

        // Foreign Keys
        public Guid SchoolId { get; set; }
        public School? School { get; set; }

        public Guid ParentId { get; set; }
        public Parent? Parent { get; set; }
    }
}