using AlIzza.API.Data;
using AlIzza.API.DTOs;
using AlIzza.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlIzza.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/staff
        [HttpPost]
        public async Task<ActionResult<StaffResponseDto>> CreateStaff(CreateStaffDto dto)
        {
            var schoolExists = await _context.Schools.AnyAsync(s => s.Id == dto.SchoolId);
            if (!schoolExists)
            {
                return BadRequest(new { message = "Invalid School ID provided." });
            }

            // Auto-generate Staff ID Number (e.g., STF/2026/001)
            int staffCount = await _context.StaffMembers.CountAsync() + 1;
            string generatedStaffId = $"STF/{DateTime.UtcNow.Year}/{staffCount:D3}";

            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                RoleType = dto.RoleType,
                Qualification = dto.Qualification,
                SchoolId = dto.SchoolId,
                StaffNumber = generatedStaffId
            };

            _context.StaffMembers.Add(staff);
            await _context.SaveChangesAsync();

            var response = new StaffResponseDto
            {
                Id = staff.Id,
                FullName = staff.FullName,
                Email = staff.Email,
                Phone = staff.PhoneNumber,
                RoleType = staff.RoleType,
                Qualification = staff.Qualification,
                SchoolId = staff.SchoolId,
                StaffNumber = staff.StaffNumber
            };

            return CreatedAtAction(nameof(GetStaffById), new { id = staff.Id }, response);
        }

        // GET: api/staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffResponseDto>>> GetAllStaff()
        {
            var staffList = await _context.StaffMembers.ToListAsync();
            
            var response = staffList.Select(s => new StaffResponseDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.PhoneNumber,
                RoleType = s.RoleType,
                Qualification = s.Qualification,
                SchoolId = s.SchoolId,
                StaffNumber = s.StaffNumber
            });

            return Ok(response);
        }

        // GET: api/staff/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffResponseDto>> GetStaffById(Guid id)
        {
            var staff = await _context.StaffMembers.FindAsync(id);
            if (staff == null)
            {
                return NotFound(new { message = "Staff record not found." });
            }

            var response = new StaffResponseDto
            {
                Id = staff.Id,
                FullName = staff.FullName,
                Email = staff.Email,
                Phone = staff.PhoneNumber,
                RoleType = staff.RoleType,
                Qualification = staff.Qualification,
                SchoolId = staff.SchoolId,
                StaffNumber = staff.StaffNumber
            };

            return Ok(response);
        }

        // GET: api/staff/school/{schoolId}
        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<IEnumerable<StaffResponseDto>>> GetStaffBySchool(Guid schoolId)
        {
            var staffList = await _context.StaffMembers
                .Where(s => s.SchoolId == schoolId)
                .ToListAsync();

            var response = staffList.Select(s => new StaffResponseDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.PhoneNumber,
                RoleType = s.RoleType,
                Qualification = s.Qualification,
                SchoolId = s.SchoolId,
                StaffNumber = s.StaffNumber
            });

            return Ok(response);
        }
    }
}