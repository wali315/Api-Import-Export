using CsvHelper.Configuration.Attributes;

namespace CsvTask3.Models
{
    public class CsvModel
    {

            public int Id { get; set; }

            public string? Name { get; set; }

            public int Age { get; set; }

            public string? Country { get; set; }
        

    }
}
