using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvTask3.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCsvData()
    {
        var csvRecords = ReadCsvFile(@"D:\downloads\CsvTask3\CsvTask3\DataOFCsv\data.csv");

        return Ok(csvRecords);
    }

    private List<CsvModel> ReadCsvFile(string filePath)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<CsvModel>().ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
            return null;
        }
    }
}
