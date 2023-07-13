using System;
using System.Collections;
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

        public void Convert(TI input, TO output) 
        {
            if (typeof(TI).IsGenericType && typeof(TI).GetGenericTypeDefinition() == typeof(List<>))
            {
                var inputList = input as IEnumerable;
                if (inputList != null)
                {
                    foreach (object element in inputList)
                    {
                        //foreach(var property in element)
                        //{

                        //}
                        var objPropertyMatch = _objectAssignementService.ObjectPropertiesIOMatch.Find(x => x.PropertyInput.Name == element);
                    }
                }
            }
            else
            {

            }
        }

        private void AssignToElement() 
        { 
        
        }
    }
}
