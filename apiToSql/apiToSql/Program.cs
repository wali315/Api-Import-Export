using System;

namespace apiToSql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new MyConfiguration();
            // Get the API URL, CSV file path, and LogPath from your configuration
            string apiUrl = config.GetApiUrl();
            string csvFilePath = config.GetCsvFilePath();
            string logPath = config.GetLogPath();

            // Create a Logger instance
            Logger logger = new Logger(logPath);

            // Create an instance of the SqlToApi class and pass the API URL and logger path
            SqlToApi sqlToApi = new SqlToApi(apiUrl, logger);
            sqlToApi.GetDataFromApiAndSaveToCsv(csvFilePath);

        }
    }
}
