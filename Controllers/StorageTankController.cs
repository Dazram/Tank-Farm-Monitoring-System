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
    public class StorageTankController : ControllerBase
    {
        private readonly TFMSDBContext _context;

        public StorageTankController(TFMSDBContext context)
        {
            _context = context;
        }

        // GET: api/StorageTank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageTank>>> GetStorageTank()
        {
            Response.Headers.Append("Refresh", "1");
            return await _context.StorageTank.ToListAsync();
        }

        // GET: api/StorageTank/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageTank>> GetStorageTank(int id)
        {
            var storageTank = await _context.StorageTank.FindAsync(id);

            if (storageTank == null)
            {
                return NotFound();
            }

            return storageTank;
        }

        // PUT: api/StorageTank/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageTank(int id, StorageTank storageTank)
        {
            if (id != storageTank.Id)
            {
                return BadRequest();
            }

            _context.Entry(storageTank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageTankExists(id))
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

        // POST: api/StorageTank
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorageTank>> PostStorageTank(StorageTank storageTank)
        {
            _context.StorageTank.Add(storageTank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStorageTank", new { id = storageTank.Id }, storageTank);
        }

        // DELETE: api/StorageTank/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageTank(int id)
        {
            var storageTank = await _context.StorageTank.FindAsync(id);
            if (storageTank == null)
            {
                return NotFound();
            }

            _context.StorageTank.Remove(storageTank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorageTankExists(int id)
        {
            return _context.StorageTank.Any(e => e.Id == id);
        }
    }
}
