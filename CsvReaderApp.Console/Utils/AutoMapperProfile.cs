using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using System.Globalization;

namespace CsvReaderApp.Console.Utils
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ReportEntry, AccountReportResult>()
                .ForMember(dest => dest.Change, opt => opt.MapFrom(src => decimal.Parse(src.Change, NumberStyles.Float | NumberStyles.AllowExponent, CultureInfo.InvariantCulture)));
        }
    }
}
