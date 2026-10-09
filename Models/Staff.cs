using System;

namespace AlIzza.API.Models
{
    public enum StaffRoleType
    {
        Teaching,
        NonTeaching,
        HostelMaster,
        HostelMistress,
        Admin
    }

    public class Staff
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string StaffNumber { get; set; } = string.Empty; // Unique employee ID
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        
        // Profile Picture URL
        public string? ProfilePictureUrl { get; set; }

        public StaffRoleType RoleType { get; set; } = StaffRoleType.Teaching;

        // Foreign Key
        public Guid SchoolId { get; set; }
        public School? School { get; set; }
    }
}