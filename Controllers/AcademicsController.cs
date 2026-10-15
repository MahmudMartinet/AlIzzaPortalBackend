using AlIzza.API.Data;
using AlIzza.API.DTOs;
using AlIzza.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlIzza.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademicsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AcademicsController(AppDbContext context)
        {
            _context = context;
        }

        // --- CLASSES ---

        // POST: api/academics/classes
        [HttpPost("classes")]
        public async Task<ActionResult<AcademicClassResponseDto>> CreateClass(CreateAcademicClassDto dto)
        {
            var schoolExists = await _context.Schools.AnyAsync(s => s.Id == dto.SchoolId);
            if (!schoolExists) return BadRequest(new { message = "Invalid School ID." });

            var newClass = new AcademicClass
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                SchoolId = dto.SchoolId,
                ClassTeacherId = dto.ClassTeacherId
            };

            _context.AcademicClasses.Add(newClass);
            await _context.SaveChangesAsync();

            return Ok(new AcademicClassResponseDto
            {
                Id = newClass.Id,
                Name = newClass.Name,
                SchoolId = newClass.SchoolId,
                ClassTeacherId = newClass.ClassTeacherId
            });
        }

        // GET: api/academics/classes/school/{schoolId}
        [HttpGet("classes/school/{schoolId}")]
        public async Task<ActionResult<IEnumerable<AcademicClassResponseDto>>> GetClassesBySchool(Guid schoolId)
        {
            var classes = await _context.AcademicClasses
                .Include(c => c.ClassTeacher)
                .Include(c => c.Subjects)
                .Where(c => c.SchoolId == schoolId)
                .ToListAsync();

            var response = classes.Select(c => new AcademicClassResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                SchoolId = c.SchoolId,
                ClassTeacherId = c.ClassTeacherId,
                TotalSubjects = c.Subjects.Count
            });

            return Ok(response);
        }

        // --- SUBJECTS ---

        // POST: api/academics/subjects
        [HttpPost("subjects")]
        public async Task<ActionResult<SubjectResponseDto>> CreateSubject(CreateSubjectDto dto)
        {
            var classExists = await _context.AcademicClasses.AnyAsync(c => c.Id == dto.AcademicClassId);
            if (!classExists) return BadRequest(new { message = "Invalid Academic Class ID." });

            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Code = dto.Code,
                AcademicClassId = dto.AcademicClassId,
                TeacherId = dto.TeacherId
            };

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return Ok(new SubjectResponseDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                AcademicClassId = subject.AcademicClassId,
                TeacherId = subject.TeacherId
            });
        }

        // GET: api/academics/subjects/class/{classId}
        [HttpGet("subjects/class/{classId}")]
        public async Task<ActionResult<IEnumerable<SubjectResponseDto>>> GetSubjectsByClass(Guid classId)
        {
            var subjects = await _context.Subjects
                .Include(s => s.AcademicClass)
                .Include(s => s.Teacher)
                .Where(s => s.AcademicClassId == classId)
                .ToListAsync();

            var response = subjects.Select(s => new SubjectResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                AcademicClassId = s.AcademicClassId,
                ClassName = s.AcademicClass?.Name,
                TeacherId = s.TeacherId,
                TeacherName = s.Teacher?.FullName
            });

            return Ok(response);
        }
    }
}