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
            Type typeElementO = null;
            if (typeof(TO).IsGenericType && typeof(TO).GetGenericTypeDefinition() == typeof(List<>))
            {
                Type typeListO = typeof(TO);
                typeElementO = typeListO.GetGenericArguments()[0];
            }

            //TO if its a list we need to get what is the element of the list so that we can use it to create objects to populate the list
            //if it is already the class its easier
            if (typeof(TI).IsGenericType && typeof(TI).GetGenericTypeDefinition() == typeof(List<>))
            {
                var inputList = input as IEnumerable;
                if (inputList != null)
                {
                    foreach (object element in inputList)
                    {
                        dynamic instance = Activator.CreateInstance(typeElementO); //creating a new instance of the output class

                        Type objectElementType = element.GetType();

                        // Get all properties of the object type
                        PropertyInfo[] properties = objectElementType.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            var objPropertyMatch = _objectAssignementService.ObjectPropertiesIOMatch.Find(x => x.PropertyInput.Name == property.Name);
                            if (objPropertyMatch != null)
                            {
                                Console.WriteLine("property found.");
                                

                                // Get all properties of the object type Output
                                PropertyInfo[] propertiesO = typeElementO.GetProperties();

                                PropertyInfo propertyOMatched = propertiesO.FirstOrDefault(x => x.Name == objPropertyMatch.PropertyOutput.Name);

                                if(propertyOMatched != null)
                                {
                                    var value = property.GetValue(element);
                                    //TODO: need to work when the type is different for Input and Output
                                    instance.GetType().GetProperty(propertyOMatched.Name)?.SetValue(instance, value);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"{typeof(TI)} is not a valid type. It should be a list");
            }
        }

        private void AssignToElement()
        {

        }
    }
}
