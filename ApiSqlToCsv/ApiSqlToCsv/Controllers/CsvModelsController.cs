using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSqlToCsv.Data;
using ApiSqlToCsv.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace ApiSqlToCsv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvModelsController : ControllerBase
    {
        private readonly ApiSqlToCsvContext _context;

        public CsvModelsController(ApiSqlToCsvContext context)
        {
            _context = context;
        }

        //[HttpPost("savetocsv")]
        //public IActionResult SaveToCsv([FromBody] List<CsvModel> data)
        //{
        //    try
        //    {
        //        // Verify if data is not null and contains records
        //        if (data == null || data.Count == 0)
        //        {
        //            return BadRequest("No data provided.");
        //        }

        //        // Define the path for the CSV file
        //        string csvFilePath = "C:\\Users\\pc\\Desktop\\data.csv";

        //        // Write the data to the CSV file
        //        using (var writer = new StreamWriter(csvFilePath))
        //        using (var csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
        //        {
        //            csv.WriteRecords(data);
        //        }

        //        return Ok("Data saved to CSV successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }
        //}

        [HttpGet("getdata")]
        public IActionResult GetData()
        {
            try
            {
                var sqlData = _context.CsvModel.ToList();
                return Ok(sqlData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        ///*GET: api/CsvModels
        //[HttpGet]
        // public async Task<ActionResult<IEnumerable<CsvModel>>> GetCsvModel()
        //{
        //    if (_context.CsvModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.CsvModel.ToListAsync();
        //}
    }
}
