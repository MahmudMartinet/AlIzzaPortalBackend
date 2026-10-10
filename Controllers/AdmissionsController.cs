using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlIzza.API.Data;
using AlIzza.API.Models;
using AlIzza.API.DTOs;

namespace AlIzza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdmissionsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/admissions/register-parent
        [HttpPost("register-parent")]
        public async Task<ActionResult<Parent>> RegisterParent(RegisterParentDto dto)
        {
            var parent = new Parent
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RegisterParent), new { id = parent.Id }, parent);
        }

        // POST: api/admissions/apply-student
        [HttpPost("apply-student")]
        public async Task<ActionResult<StudentResponseDto>> SubmitApplication(SubmitStudentApplicationDto dto)
        {
            // Verify parent and school exist
            var parentExists = await _context.Parents.AnyAsync(p => p.Id == dto.ParentId);
            var schoolExists = await _context.Schools.AnyAsync(s => s.Id == dto.SchoolId);

            if (!parentExists) return BadRequest(new { message = "Parent not found." });
            if (!schoolExists) return BadRequest(new { message = "School not found." });

            var student = new Student
            {
                ParentId = dto.ParentId,
                SchoolId = dto.SchoolId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                ClassApplyingFor = dto.ClassApplyingFor,
                AdmissionStatus = dto.AdmissionStatus, // Default is Pending
                SchoolNumber = null // Will be generated upon approval
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                AdmissionStatus = student.AdmissionStatus,
                SchoolNumber = student.SchoolNumber,
                ClassApplyingFor = student.ClassApplyingFor,
                SchoolId = student.SchoolId,
                ParentId = student.ParentId
            };

            return Ok(response);
        }

        // PUT: api/admissions/approve/{studentId}
        [HttpPut("approve/{studentId}")]
        public async Task<ActionResult<StudentResponseDto>> ApproveAdmission(Guid studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return NotFound(new { message = "Student application not found." });

            if (student.AdmissionStatus == AdmissionStatus.Active)
            {
                return BadRequest(new { message = "Application is already approved." });
            }

            // Generate a unique School Number (e.g., ALIZZA/2026/001)
            int studentCount = await _context.Students.CountAsync(s => s.AdmissionStatus == AdmissionStatus.Active) + 1;
            student.SchoolNumber = $"ALIZZA/{DateTime.UtcNow.Year}/{studentCount:D3}";
            student.AdmissionStatus = AdmissionStatus.Active;

            await _context.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                AdmissionStatus = student.AdmissionStatus,
                SchoolNumber = student.SchoolNumber,
                ClassApplyingFor = student.ClassApplyingFor,
                SchoolId = student.SchoolId,
                ParentId = student.ParentId
            };

            return Ok(response);
        }


        // GET: api/admissions/parents
        [HttpGet("parents")]
        public async Task<ActionResult<IEnumerable<Parent>>> GetParents()
        {
            return await _context.Parents.Include(p => p.Students).ToListAsync();
        }

        // GET: api/admissions/students
        [HttpGet("students")]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            
            var response = students.Select(s => new StudentResponseDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                AdmissionStatus = s.AdmissionStatus,
                SchoolNumber = s.SchoolNumber,
                ClassApplyingFor = s.ClassApplyingFor,
                SchoolId = s.SchoolId,
                ParentId = s.ParentId
            });

            return Ok(response);
        }
    }
}