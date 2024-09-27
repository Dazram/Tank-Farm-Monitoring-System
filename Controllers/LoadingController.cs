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
    public class LoadingController : ControllerBase
    {
        private readonly TFMSDBContext _context;

        public LoadingController(TFMSDBContext context)
        {
            _context = context;
        }

        // GET: api/Loading
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loading>>> GetLoading()
        {
            Response.Headers.Append("Refresh", "3");
            return await _context.Loading.ToListAsync();
        }

        // GET: api/Loading/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loading>> GetLoading(int id)
        {
            var loading = await _context.Loading.FindAsync(id);

            if (loading == null)
            {
                return NotFound();
            }

            return loading;
        }

        // PUT: api/Loading/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoading(int id, Loading loading)
        {
            if (id != loading.Id)
            {
                return BadRequest();
            }

            _context.Entry(loading).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoadingExists(id))
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

        // POST: api/Loading
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Loading>> PostLoading(Loading loading)
        {
            _context.Loading.Add(loading);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoading", new { id = loading.Id }, loading);
        }

        // DELETE: api/Loading/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoading(int id)
        {
            var loading = await _context.Loading.FindAsync(id);
            if (loading == null)
            {
                return NotFound();
            }

            _context.Loading.Remove(loading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoadingExists(int id)
        {
            return _context.Loading.Any(e => e.Id == id);
        }
    }
}
