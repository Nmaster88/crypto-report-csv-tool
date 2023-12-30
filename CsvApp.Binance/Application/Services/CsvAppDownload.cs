using CsvApp.Binance.Application.Dtos;
using CsvApp.Binance.Application.Interfaces;
using CsvApp.Binance.Application.Mappers;
using CsvApp.Binance.Domain.Entities;
using CsvApp.Binance.Domain.Interfaces;

namespace CsvApp.Binance.Application.Services
{
    public class CsvAppDownload : ICsvAppDownload
    {
        private readonly IFilesRepository _filesRepository;

        public CsvAppDownload(IFilesRepository filesRepository)
        {
            _filesRepository = filesRepository;
        }

        public async Task<List<IncomeGainsDto>> DownloadIncomeGains()
        {
            List<IncomeGainsEntity>? entities = null;
            try
            {
                entities = await _filesRepository.GetIncomeGains();
            }
            catch (Exception ex)
            {
                //TODO: handle exception
            }
            if (entities == null)
            {
                //throw new DeserializationFailureException("Failure to deserialize data.");
            }

            return await Task.FromResult(entities.ToViews().ToList());
        }
        public async Task<List<TransactionsDto>> DownloadTransations()
        {
            List<TransactionsEntity>? entities = null;
            try
            {
                entities = await _filesRepository.GetTransactions();
            }
            catch (Exception ex)
            {
                //TODO: handle exception
            }
            if (entities == null)
            {
                //throw new DeserializationFailureException("Failure to deserialize data.");
            }

            return await Task.FromResult(entities.ToViews().ToList());
        }

        public async Task<List<RealizedCapitalGainsDto>> DownloadRealizedCapitalGains()
        {
            List<RealizedCapitalGainsEntity>? entities = null;
            try
            {
                entities = await _filesRepository.GetRealizedCapitalGains();
            }
            catch (Exception ex)
            {
                //TODO: handle exception
            }
            if (entities == null)
            {
                //throw new DeserializationFailureException("Failure to deserialize data.");
            }

            return await Task.FromResult(entities.ToViews().ToList());
        }
    }
}
