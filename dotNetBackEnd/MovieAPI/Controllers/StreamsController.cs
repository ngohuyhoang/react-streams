using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class StreamsController : ControllerBase
    {
        private readonly MovieAPIContext _context;

        public StreamsController(MovieAPIContext context)
        {
            _context = context;
        }

        // GET: api/Streams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stream>>> GetStream()
        {
            return await _context.Stream.ToListAsync();
        }

        // GET: api/Streams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stream>> GetStream(int id)
        {
            var stream = await _context.Stream.FindAsync(id);

            if (stream == null)
            {
                return NotFound();
            }

            return stream;
        }

        // PUT: api/Streams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStream(int id, Stream stream)
        {
            if (id != stream.Id)
            {
                return BadRequest();
            }

            _context.Entry(stream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreamExists(id))
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

        // POST: api/Streams
        [HttpPost]
        public async Task<ActionResult<Stream>> PostStream(Stream stream)
        {
            _context.Stream.Add(stream);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStream", new { id = stream.Id }, stream);
        }

        // DELETE: api/Streams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stream>> DeleteStream(int id)
        {
            var stream = await _context.Stream.FindAsync(id);
            if (stream == null)
            {
                return NotFound();
            }

            _context.Stream.Remove(stream);
            await _context.SaveChangesAsync();

            return stream;
        }

        private bool StreamExists(int id)
        {
            return _context.Stream.Any(e => e.Id == id);
        }
    }
}
