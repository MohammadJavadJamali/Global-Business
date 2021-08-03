using System;
using Serilog;
using System.Data;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateMSSqlLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseSerilog();
                });

        public static void CreateMSSqlLogger()
        {
            var connectionString = "server=.\\sql2019; DataBase=MiniBankdb-dapper; Trusted_Connection=true; MultipleActiveResultSets=true";
            var tableName = "Logs";

            ColumnOptions columnOption = new ColumnOptions();
            columnOption.Store.Remove(StandardColumn.MessageTemplate);
            columnOption.Store.Remove(StandardColumn.TimeStamp);
            columnOption.Store.Remove(StandardColumn.Properties);
            columnOption.Store.Remove(StandardColumn.LogEvent);

            columnOption.AdditionalDataColumns = new Collection<DataColumn>
                            {
                                new DataColumn {DataType = typeof (int), ColumnName = "Count"},
                                new DataColumn {DataType = typeof (decimal), ColumnName = "TotalDeposit"},
                                new DataColumn {DataType = typeof (string), ColumnName = "Status"},
                                new DataColumn {DataType = typeof (DateTime), ColumnName = "Start"},
                                new DataColumn {DataType = typeof (DateTime), ColumnName = "End"},
                                new DataColumn {DataType = typeof (string), ColumnName = "Type"},
                            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("SerilogDemo", LogEventLevel.Information)
                .WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOption)
                .CreateLogger();
        }
    }
}
