using System.ComponentModel.DataAnnotations;
using AlIzza.API.Models;

namespace AlIzza.API.DTOs
{
    public class CreateStaffDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Qualification is required.")]
        public string Qualification { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public Guid SchoolId { get; set; }
         public StaffRoleType RoleType { get; set; } = StaffRoleType.Teaching;
    }

    public class StaffResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }

        public Guid SchoolId { get; set; }
        public string StaffNumber { get; set; } = string.Empty;
        public StaffRoleType RoleType { get; set; } = StaffRoleType.Teaching;
    }
}