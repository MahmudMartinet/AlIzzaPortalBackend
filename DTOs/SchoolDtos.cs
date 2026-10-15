using System.ComponentModel.DataAnnotations;

namespace AlIzza.API.DTOs
{
    // What the client sends when creating a school
    public class CreateSchoolDto
    {
        [Required(ErrorMessage = "School name is required.")]
        [StringLength(100, ErrorMessage = "School name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? SchoolCode { get; set; }
      
    }

    // What the API returns back to the client
    public class SchoolDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? SchoolCode { get; set; }
    }
}