using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT102G_project_api.Data;
using DT102G_project_API.Models;

namespace DT102G_project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInformationsController : ControllerBase
    {
        private readonly CvContext _context;

        public PersonalInformationsController(CvContext context)
        {
            _context = context;
        }

        // GET: api/PersonalInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalInformation>>> GetPersonalInformation()
        {
            return await _context.PersonalInformation.ToListAsync();
        }

        // GET: api/PersonalInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalInformation>> GetPersonalInformation(int id)
        {
            var personalInformation = await _context.PersonalInformation.FindAsync(id);

            if (personalInformation == null)
            {
                return NotFound();
            }

            return personalInformation;
        }

        // PUT: api/PersonalInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalInformation(int id, PersonalInformation personalInformation)
        {
            if (id != personalInformation.PersonalInformationId)
            {
                return BadRequest();
            }

            _context.Entry(personalInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalInformationExists(id))
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

        // POST: api/PersonalInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonalInformation>> PostPersonalInformation(PersonalInformation personalInformation)
        {
            _context.PersonalInformation.Add(personalInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonalInformation", new { id = personalInformation.PersonalInformationId }, personalInformation);
        }

        // DELETE: api/PersonalInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalInformation(int id)
        {
            var personalInformation = await _context.PersonalInformation.FindAsync(id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            _context.PersonalInformation.Remove(personalInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalInformationExists(int id)
        {
            return _context.PersonalInformation.Any(e => e.PersonalInformationId == id);
        }
    }
}
