using Microsoft.AspNetCore.Mvc;
using GradeManagement.Models.Domain;
using GradeManagement.Models.DTO;
using GradeManagement.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;


namespace GradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CenterController : ControllerBase
    {
        private readonly GradeDbContext _context;

        public CenterController(GradeDbContext context)
        {
            _context = context;
        }

        // GET: api/Center
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CenterDTO>>> GetCenters()
        {
            var centers = await _context.Centers.ToListAsync();
            var centerDTOs = centers.ConvertAll(c => new CenterDTO
            {
                CenterId = c.CenterId,
                Name = c.Name
            });

            return centerDTOs;
        }

        // GET: api/Center/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CenterDTO>> GetCenter(Guid id)
        {
            var center = await _context.Centers.FindAsync(id);

            if (center == null)
            {
                return NotFound();
            }

            var centerDTO = new CenterDTO
            {
                CenterId = center.CenterId,
                Name = center.Name
            };

            return centerDTO;
        }

        // POST: api/Center
        [HttpPost]
        public async Task<ActionResult<CenterDTO>> PostCenter([FromBody] CenterDTO centerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var center = new Center
            {
                CenterId = centerDto.CenterId,
                Name = centerDto.Name
            };

            _context.Centers.Add(center);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCenter), new { id = center.CenterId }, centerDto);
        }

        // PUT: api/Center/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenter(Guid id, [FromBody] CenterDTO centerDto)
        {
            if (id != centerDto.CenterId)
            {
                return BadRequest();
            }

            var center = await _context.Centers.FindAsync(id);
            if (center == null)
            {
                return NotFound();
            }

            center.Name = centerDto.Name;
            _context.Entry(center).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Center/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenter(Guid id)
        {
            var center = await _context.Centers.FindAsync(id);

            if (center == null)
            {
                return NotFound();
            }

            _context.Centers.Remove(center);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}