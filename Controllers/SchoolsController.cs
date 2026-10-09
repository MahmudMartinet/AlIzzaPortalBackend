using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlIzza.API.Data;
using AlIzza.API.Models;
using AlIzza.API.DTOs;

namespace AlIzza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SchoolsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchools()
        {
            var schools = await _context.Schools
                .Select(s => new SchoolDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    SchoolCode = s.SchoolCode
                    
                })
                .ToListAsync();

            return Ok(schools);
        }

        // GET: api/schools/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(Guid id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound(new { message = "School not found." });
            }

            var schoolDto = new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                SchoolCode = school.SchoolCode
            
            };

            return Ok(schoolDto);
        }

        // POST: api/schools
        [HttpPost]
        public async Task<ActionResult<SchoolDto>> CreateSchool(CreateSchoolDto createDto)
        {
            var school = new School
            {
                Name = createDto.Name,
                Address = createDto.Address,
                
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            var schoolDto = new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                SchoolCode = school.SchoolCode,
            };

            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, schoolDto);
        }
    }
}