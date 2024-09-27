using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Capstone.Models;
using TFMS.Data;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffloadingController : ControllerBase
    {
        private readonly TFMSDBContext _context;

        public OffloadingController(TFMSDBContext context)
        {
            _context = context;
        }

        // GET: api/Offloading
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offloading>>> GetOffloading()
        {
            Response.Headers.Append("Refresh", "3");
            return await _context.Offloading.ToListAsync();
        }

        // GET: api/Offloading/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Offloading>> GetOffloading(int id)
        {
            var offloading = await _context.Offloading.FindAsync(id);

            if (offloading == null)
            {
                return NotFound();
            }

            return offloading;
        }

        // PUT: api/Offloading/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffloading(int id, Offloading offloading)
        {
            if (id != offloading.Id)
            {
                return BadRequest();
            }

            _context.Entry(offloading).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffloadingExists(id))
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

        // POST: api/Offloading
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Offloading>> PostOffloading(Offloading offloading)
        {
            _context.Offloading.Add(offloading);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffloading", new { id = offloading.Id }, offloading);
        }

        // DELETE: api/Offloading/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffloading(int id)
        {
            var offloading = await _context.Offloading.FindAsync(id);
            if (offloading == null)
            {
                return NotFound();
            }

            _context.Offloading.Remove(offloading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OffloadingExists(int id)
        {
            return _context.Offloading.Any(e => e.Id == id);
        }
    }
}
