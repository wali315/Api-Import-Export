using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvTask3.Data;
using CsvTask3.Models;

namespace CsvTask3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlToApiController : ControllerBase
    {
        private readonly CsvTask3Context _context;

        public SqlToApiController(CsvTask3Context context)
        {
            _context = context;
        }

        // GET: api/SqlToApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CsvModel>>> GetCsvModel()
        {
          if (_context.CsvModel == null)
          {
              return NotFound();
          }
            return await _context.CsvModel.ToListAsync();
        }

        // GET: api/SqlToApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CsvModel>> GetCsvModel(int id)
        {
          if (_context.CsvModel == null)
          {
              return NotFound();
          }
            var csvModel = await _context.CsvModel.FindAsync(id);

            if (csvModel == null)
            {
                return NotFound();
            }

            return csvModel;
        }

        // PUT: api/SqlToApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCsvModel(int id, CsvModel csvModel)
        {
            if (id != csvModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(csvModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CsvModelExists(id))
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

        // POST: api/SqlToApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CsvModel>> PostCsvModel(CsvModel csvModel)
        {
          if (_context.CsvModel == null)
          {
              return Problem("Entity set 'CsvTask3Context.CsvModel'  is null.");
          }
            _context.CsvModel.Add(csvModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCsvModel", new { id = csvModel.Id }, csvModel);
        }

        // DELETE: api/SqlToApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCsvModel(int id)
        {
            if (_context.CsvModel == null)
            {
                return NotFound();
            }
            var csvModel = await _context.CsvModel.FindAsync(id);
            if (csvModel == null)
            {
                return NotFound();
            }

            _context.CsvModel.Remove(csvModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CsvModelExists(int id)
        {
            return (_context.CsvModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
