using Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class CsvWriterFactory : ICsvWriterFactory
    {

        public IWriter create(string filePath)
        {
            return new CsvWriterService(filePath);
        }
    }
}
