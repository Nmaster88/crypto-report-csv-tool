using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interfaces
{
    internal interface ICsvWriterFactory
    {
        Interfaces.IWriter create(string filePath);
    }
}
