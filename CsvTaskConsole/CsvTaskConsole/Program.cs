using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvTaskConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            MyConfiguration config = new MyConfiguration();
            string connectionString = config.GetConnectionString();
            string apiUrl = config.GetApiUrl();
            string LogPath = config.GetLogPath();
            Logger logger = new Logger(LogPath);


            ApiToSql apiToSql = new ApiToSql(connectionString, logger, apiUrl);
            await apiToSql.SendDataFromApiToSqlAsync();
        }
    }
}
