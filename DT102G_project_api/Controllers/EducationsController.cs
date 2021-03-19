using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT102G_project_API.Models;
using DT102G_project_api.Data;

namespace DT102G_project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly CvContext _context;

        public EducationsController(CvContext context)
        {
            _context = context;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducation()
        {
            return await _context.Education.OrderByDescending(education => education.EndDate).ToListAsync();
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducation(int id)
        {
            var education = await _context.Education.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return education;
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, Education education)
        {
            if (id != education.EducationId)
            {
                return BadRequest();
            }

            _context.Entry(education).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Education>> PostEducation(Education education)
        {
            _context.Education.Add(education);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducation", new { id = education.EducationId }, education);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(int id)
        {
            return _context.Education.Any(e => e.EducationId == id);
        }
    }
}
