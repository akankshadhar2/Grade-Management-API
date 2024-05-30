using Microsoft.AspNetCore.Mvc;
using GradeManagement.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using GradeManagement.Models.DTO;
using GradeManagement.Models.Domain;

namespace GradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly GradeDbContext _context;

        public GradeController(GradeDbContext context)
        {
            _context = context;
        }

        // GET: api/Grade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDTO>>> GetGrades()
        {
            var grades = await _context.Grades.Include(g => g.Center).ToListAsync();
            var gradeDTOs = grades.ConvertAll(g => new GradeDTO
            {
                GradeName = g.GradeName,
                DisplayName = g.DisplayName,
                CenterName = g.Center.Name,
                GradeId=g.GradeId
            });

            return gradeDTOs;
        }

        // GET: api/Grade/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDTO>> GetGrade(Guid id)
        {
            var grade = await _context.Grades.Include(g => g.Center).FirstOrDefaultAsync(g => g.GradeId == id);

            if (grade == null)
            {
                return NotFound();
            }

            var gradeDTO = new GradeDTO
            {
                GradeName = grade.GradeName,
                DisplayName = grade.DisplayName,
                CenterName = grade.Center.Name,
                GradeId = grade.GradeId
            };

            return gradeDTO;
        }

        // POST: api/Grade
        [HttpPost]
        public async Task<ActionResult<GradeDTO>> PostGrade([FromBody] GradeDTO gradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var center = await _context.Centers.FirstOrDefaultAsync(c => c.Name == gradeDto.CenterName);

            if (center == null)
            {
                return BadRequest("Center not found.");
            }

            var grade = new Grade
            {
                GradeName = gradeDto.GradeName,
                DisplayName = gradeDto.DisplayName,
                Center = center,
                GradeId = gradeDto.GradeId
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrade), new { id = grade.GradeId }, gradeDto);
        }
        // PUT: api/Grade/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(Guid id, [FromBody] GradeDTO gradeDto)
        {
            var grade = await _context.Grades.Include(g => g.Center).FirstOrDefaultAsync(g => g.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            var center = await _context.Centers.FirstOrDefaultAsync(c => c.Name == gradeDto.CenterName);
            if (center == null)
            {
                return BadRequest("Center not found.");
            }

            grade.GradeName = gradeDto.GradeName;
            grade.DisplayName = gradeDto.DisplayName;
            grade.Center = center;
            grade.GradeId = gradeDto.GradeId;

            _context.Entry(grade).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Grade/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}