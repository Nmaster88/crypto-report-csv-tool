using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Console.Utils
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BinanceReportEntry, AccountReportResult>()
                .ForMember(dest => dest.Change, opt => opt.MapFrom(src => decimal.Parse(src.Change, NumberStyles.Float | NumberStyles.AllowExponent, CultureInfo.InvariantCulture)));
        }
    }
}
