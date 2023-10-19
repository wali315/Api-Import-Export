using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CsvTaskConsole
{
    public class ApiToSql
    {
        private readonly string _connectionString;
        private readonly Logger _logger;
        private readonly string _apiUrl;

        public ApiToSql(string connectionString, Logger logger, string apiUrl)
        {
            _connectionString = connectionString;
            _logger = logger;
            _apiUrl = apiUrl;
        }


        public async Task SendDataFromApiToSqlAsync()
        {
            try
            {
                Console.WriteLine("Data reading starts.");// Printing In Console.
                _logger.LogMessage("Data reading starts.");// Giving Message To LogFile.
                // Send a GET request to the API URL to fetch data
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(_apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var apiData = JsonConvert.DeserializeObject<List<CsvModel>>(content);

                        using (var context = new CsvDbContext())
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var apiRecord in apiData)
                                {
                                    var existingRecord = context.CsvModels.FirstOrDefault(x => x.Id == apiRecord.Id);

                                    if (existingRecord != null)
                                    {
                                        // Update existing record
                                        existingRecord.Name = apiRecord.Name;
                                        existingRecord.Age = apiRecord.Age;
                                        existingRecord.country = apiRecord.country;
                                    }
                                    else
                                    {
                                        // Insert a new record
                                        context.CsvModels.Add(new CsvModel
                                        {
                                            Name = apiRecord.Name,
                                            Age = apiRecord.Age,
                                            country = apiRecord.country
                                        }); // Do not set the Id property explicitly
                                    }
                                }
                                

                                context.SaveChanges();
                                transaction.Commit();
                                Console.WriteLine("API Data Imported to SQL with updates.");
                                EmailSender emailSender = new EmailSender();
                                emailSender.SendEmail("From ApiToSql", " Import Successful");//First Is Subject And Second Is Email Message You Can GEt this Message In You Gmail.
                                Console.WriteLine("Data reading completed.");
                            }
                            catch (Exception ex)
                            {
                                // Handle exceptions and log errors
                                Console.WriteLine($"An error occurred during database operations: {ex.Message}");
                                if (ex.InnerException != null)
                                {
                                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                                }
                                transaction.Rollback();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to fetch data from the API.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }





    }
}