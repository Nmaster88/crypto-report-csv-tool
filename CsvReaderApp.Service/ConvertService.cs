using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Services
{
    public class ConvertService<TI,TO>
    {
        private readonly ObjectAssignementService<TI,TO> _objectAssignementService;
        public ConvertService(ObjectAssignementService<TI, TO> objectAssignementService) 
        { 
            _objectAssignementService = objectAssignementService ?? throw new ArgumentNullException(nameof(ObjectAssignementService<TI, TO>));
        }


    }
}
