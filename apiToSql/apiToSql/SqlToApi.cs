using apiToSql;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;

public class SqlToApi
{
    private readonly string apiUrl;
    private Logger _logger;

    public SqlToApi(string apiUrl, Logger logger)
    {
        this.apiUrl = apiUrl;
        this._logger = logger;
    }

    public void GetDataFromApiAndSaveToCsv(string csvFilePath)
    {
        try
        {

            Console.WriteLine("Data reading starts.");// Printing In Console.
            _logger.LogMessage("Data reading starts.");// Giving Message To LogFile.
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(apiUrl).Result;

                var dataFromApi = JsonConvert.DeserializeObject<CsvModel[]>(response);

                using (var writer = new StreamWriter(csvFilePath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(dataFromApi);
                }
            }
            Console.WriteLine("API Data Exported to CSV.");// Printing In Console.
            _logger.LogMessage("API Data Exported to CSV.");// Giving Message To LogFile.

            // Calling Email By Method
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail("From SqlToApi", " Export Successful");//First Is Subject And Second Is Email Message You Can GEt this Message In You Gmail.
            Console.WriteLine("Data reading Ends.");// Printing In Console.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            _logger.LogMessage($"An error occurred: {ex.Message}");
        }
    }
}
