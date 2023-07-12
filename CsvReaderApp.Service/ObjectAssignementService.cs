using System;
using System.Reflection;

namespace CsvReaderApp.Services
{
    public class ObjectAssignementService<TI, TO>
    {
        //TODO: console.writeLine or readLine should instead be replaced by DI. So that its generic.
        private List<ObjectProperty> ObjectPropertiesI = new List<ObjectProperty>();
        private List<ObjectProperty> ObjectPropertiesO = new List<ObjectProperty>();
        
        private class ObjectProperty
        {
            public required string Name { get; set; }
            public required string Type { get; set; }
        }
        
        public void Setup()
        {
            GenericInputTypeRead<TI>();
            GenericOutputTypeRead<TO>();
        }

        private bool TypeIsList(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        private void GenericInputTypeRead<T>()
        {
            ObjectPropertiesI = new List<ObjectProperty>();
            GenericListTypeRead<T>(ObjectPropertiesI);
        }

        private void GenericOutputTypeRead<T>()
        {
            ObjectPropertiesO = new List<ObjectProperty>();
            GenericListTypeRead<T>(ObjectPropertiesO);
        }

        /// <summary>
        /// From a generic type read its properties and assign it to an object that will contain its metadata
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ObjectProperties"></param>
        private void GenericListTypeRead<T>(List<ObjectProperty> ObjectProperties)
        {
            Type type = typeof(T);

            if (TypeIsList(type))
            {
                Type typeOfList = type.GetGenericArguments()[0];
                string typeName = typeOfList.Name;
                Console.WriteLine($"Class name: {typeName}",typeName);
                PropertyInfo[] properties = typeOfList.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    Console.WriteLine($"Name: {property.Name} | Type: {property.PropertyType}");
                    ObjectProperty objProperty = new ObjectProperty()
                    { 
                        Name = property.Name,
                        Type = property.PropertyType.Name
                    };
                    ObjectProperties.Add(objProperty);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"{type.Name} is not of type List<> or its derived types.");
            }

        }

        public void Mapping()
        {
            if(ObjectPropertiesI?.Count == 0 || ObjectPropertiesO?.Count == 0)
            {
                Console.WriteLine("Please run setup first.");
                return;
            }
            Console.WriteLine("Proceed with the mapping between output object and input object");
            Type typeI = typeof(TI);
            Type typeO = typeof(TO);

            Console.WriteLine($"from the class {typeI} properties map each one of them to the class {typeO}");

            foreach(ObjectProperty propertyO in ObjectPropertiesO) {
                Console.Write($"{propertyO.Name} match with: ");
                ObjectProperty propertyI = null;
                do
                {
                    string propName = Console.ReadLine();
                    propertyI = ObjectPropertiesI.Find(x => x.Name == propName);
                    if(propertyI == null)
                    {
                        Console.WriteLine("A property with that name was not found.");
                    }
                    else if (propertyI?.Type == propertyO?.Type)
                    {
                        Console.WriteLine("The type of the property is the same between them");
                    }
                    else
                    {
                        //TODO: In this case it should be possible to do a conversion?
                        //check both type of Input and Output and work on conversion
                        CheckTypesAndApplyConvertion(propertyI?.Type, propertyO?.Type);
                        Console.WriteLine("The type of the property is not the same");
                    }
                }
                while (propertyI == null);
            }
        }

        private void CheckTypesAndApplyConvertion(string type1, string type2)
        {
            throw new NotImplementedException();
        }
    }
}
