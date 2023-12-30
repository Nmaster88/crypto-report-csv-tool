using Common.Services.Interfaces;
using CsvApp.Binance.Domain.Entities;
using CsvApp.Binance.Domain.Interfaces;

namespace CsvApp.Binance.Infrastructure
{
    public class FilesRepository : IFilesRepository
    {
        public string FilePath { get; set; } = string.Empty;

        private readonly IReader _reader;

        public FilesRepository(IReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public async Task<List<IncomeGainsEntity>?> GetIncomeGains()
        {
            _ = FilePath ?? throw new ArgumentNullException(FilePath);

            _reader.Open(FilePath);
            List<IncomeGainsEntity>? records = _reader.ReadRecords<IncomeGainsEntity>();

            return await Task.FromResult(records);
        }

        public async Task<List<TransactionsEntity>?> GetTransactions()
        {
            _ = FilePath ?? throw new ArgumentNullException(FilePath);

            _reader.Open(FilePath);
            List<TransactionsEntity>? records = _reader.ReadRecords<TransactionsEntity>();

            return await Task.FromResult(records);
        }

        public async Task<List<RealizedCapitalGainsEntity>?> GetRealizedCapitalGains()
        {
            _ = FilePath ?? throw new ArgumentNullException(FilePath);

            _reader.Open(FilePath);
            List<RealizedCapitalGainsEntity>? records = _reader.ReadRecords<RealizedCapitalGainsEntity>();

            return await Task.FromResult(records);
        }
    }
}
