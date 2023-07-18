using System.Collections;
using System.Reflection;

namespace CsvReaderApp.Services
{
    public class ConvertService<TI, TO>
    {
        private readonly ObjectAssignementService<TI, TO> _objectAssignementService;
        public ConvertService(ObjectAssignementService<TI, TO> objectAssignementService)
        {
            _objectAssignementService = objectAssignementService ?? throw new ArgumentNullException(nameof(ObjectAssignementService<TI, TO>));
        }

        public void Convert(TI input, TO output)
        {
            //TO if its a list we need to get what is the element of the list so that we can use it to create objects to populate the list
            //if it is already the class its easier
            if (typeof(TI).IsGenericType && typeof(TI).GetGenericTypeDefinition() == typeof(List<>))
            {
                var inputList = input as IEnumerable;
                if (inputList != null)
                {
                    foreach (object element in inputList)
                    {
                        Type objectElementType = element.GetType();

                        // Get all properties of the object type
                        PropertyInfo[] properties = objectElementType.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            var objPropertyMatch = _objectAssignementService.ObjectPropertiesIOMatch.Find(x => x.PropertyInput.Name == property.Name);
                            if (objPropertyMatch != null)
                            {
                                Console.WriteLine("property found.");
                            }
                        }
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
