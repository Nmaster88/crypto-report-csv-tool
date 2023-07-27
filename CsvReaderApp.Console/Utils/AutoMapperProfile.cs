using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using System.Globalization;

namespace CsvReaderApp.Console.Utils
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            int idCounter = 0;

            CreateMap<ReportEntry, AccountReportResult>()
                .AfterMap((src, dest) => dest.Id = ++idCounter)
                .ForMember(dest => dest.Change, opt => opt.MapFrom(src => decimal.Parse(src.Change, NumberStyles.Float | NumberStyles.AllowExponent, CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.UTC_Time));
        }
    }
}
