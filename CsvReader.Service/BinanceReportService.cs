using CsvHelper;
using CsvReaderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Services
{
    public class BinanceReportService
    {
        public List<string> Operations { get; set; }

        public List<string> Coins { get; set; }

        public List<string> Accounts { get; set; }



        public BinanceReportService() { }

        public void Execute(IEnumerable<BinanceReport> binanceReports)
        {
            foreach (BinanceReport binanceReport in binanceReports)
            {

            }
        }
    }
}
