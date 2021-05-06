using Grupp4Lms.Core.Entities;
using Grupp4Lms.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp4Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmnesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AmnesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Amnes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amne>>> GetAmne()
        {
            return await _context.Amne.ToListAsync();
        }

        // GET: api/Amnes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amne>> GetAmne(int id)
        {
            var amne = await _context.Amne.FindAsync(id);

            if (amne == null)
            {
                return NotFound();
            }

            return amne;
        }

        // PUT: api/Amnes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmne(int id, Amne amne)
        {
            if (id != amne.AmneId)
            {
                return BadRequest();
            }

            _context.Entry(amne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmneExists(id))
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

        // POST: api/Amnes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Amne>> PostAmne(Amne amne)
        {
            _context.Amne.Add(amne);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmne", new { id = amne.AmneId }, amne);
        }

        // DELETE: api/Amnes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmne(int id)
        {
            var amne = await _context.Amne.FindAsync(id);
            if (amne == null)
            {
                return NotFound();
            }

            _context.Amne.Remove(amne);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmneExists(int id)
        {
            return _context.Amne.Any(e => e.AmneId == id);
        }
    }
}
