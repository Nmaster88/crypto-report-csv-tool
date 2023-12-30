using CsvApp.Binance.Application.Dtos;
using CsvApp.Binance.Application.Interfaces;

namespace CsvApp.Binance.Application.Services
{
    public class CsvAppEntryPoint : ICsvApp
    {
        private readonly ICsvAppDownload _csvAppDownload;
        private readonly ICsvAppProcess _csvAppProcess;
        public CsvAppEntryPoint(
            ICsvAppDownload csvAppDownload,
            ICsvAppProcess csvAppProcess)
        {
            _csvAppDownload = csvAppDownload;
            _csvAppProcess = csvAppProcess;
        }


        public async Task<List<IncomeGainsDto>> GetIncomeGains()
        {
            var result = await _csvAppDownload.DownloadIncomeGains();

            var processedResult = _csvAppProcess.Process(result);

            return processedResult;
        }

        public async Task<List<TransactionsDto>> GetTransactions()
        {
            var result = await _csvAppDownload.DownloadTransations();

            var processedResult = _csvAppProcess.Process(result);

            return processedResult;
        }

        public async Task<List<string>> GetRealizedCapitalGains()
        {
            var result = await _csvAppDownload.DownloadRealizedCapitalGains();

            var processedResult = _csvAppProcess.Process(result);

            return processedResult;
        }
    }
}
