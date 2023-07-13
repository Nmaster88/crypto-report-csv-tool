using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
//using static CsvReaderApp.Services.ObjectAssignementService<TI, TO>;

namespace CsvReaderApp.Services
{
    public class ObjectAssignementService<TI, TO>
    {
        //TODO: console.writeLine or readLine should instead be replaced by DI. So that its generic.
        public List<ObjectProperty> ObjectPropertiesI = new List<ObjectProperty>();
        public List<ObjectProperty> ObjectPropertiesO = new List<ObjectProperty>();
        public List<ObjectPropertiesMatch> ObjectPropertiesIOMatch = new List<ObjectPropertiesMatch>();
        
        public class ObjectProperty
        {
            public required string Name { get; set; }
            public required PropertyInfo Type { get; set; }
            public required string TypeName { get; set; }
        }

        public class ObjectPropertiesMatch
        {
            public ObjectProperty PropertyInput { get; set; }
            public ObjectProperty PropertyOutput { get; set; }
            public ObjectConversion objectConversion { get; set; }
        }

        public enum ObjectConversion
        {
            NoConversion = 0,
            StringToInt = 1,
            StringToDecimal = 2,
            IntToString = 3,
            DecimalToString = 4,
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
                        Type = property,
                        TypeName = property.PropertyType.Name
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
                ObjectProperty propertyI = null;
                do
                {
                    Console.Write($"{propertyO.Name} match with: ");
                    string propName = Console.ReadLine();
                    propertyI = ObjectPropertiesI.Find(x => x.Name == propName);

                    if (propertyI == null)
                    {
                        Console.WriteLine("A property with that name was not found.");
                    }
                    else if (propertyI?.TypeName == propertyO?.TypeName)
                    {
                        Console.WriteLine("The type of the property is the same between them");
                        AddObjectPropertiesMatch(propertyI, propertyO);
                    }
                    else
                    {
                        //TODO: In this case it should be possible to do a conversion?
                        //check both type of Input and Output and work on conversion
                        ObjectConversion objectConversion = CheckTypesAndGetConvertionType(propertyI.Type, propertyO.Type);
                        Console.WriteLine("WARNING: The type of the properties is not the same");
                        AddObjectPropertiesMatch(propertyI, propertyO, objectConversion);
                    }
                }
                while (propertyI == null);
            }
        }

        private void AddObjectPropertiesMatch(ObjectProperty propertyI, ObjectProperty propertyO, ObjectConversion objectConversion = ObjectConversion.NoConversion)
        {
            ObjectPropertiesMatch objectPropertiesMatch = new ObjectPropertiesMatch();
            objectPropertiesMatch.PropertyInput = propertyI;
            objectPropertiesMatch.PropertyOutput = propertyO;
            objectPropertiesMatch.objectConversion = objectConversion;
            ObjectPropertiesIOMatch.Add(objectPropertiesMatch);
        }

        private ObjectConversion CheckTypesAndGetConvertionType(PropertyInfo infoI, PropertyInfo infoO)
        {
            Type typeI = infoI.PropertyType;
            Type typeO = infoO.PropertyType;

            if(typeI == typeof(int) && typeO == typeof(string))
            {
                return ObjectConversion.IntToString;
            }

            if (typeI == typeof(decimal) && typeO == typeof(string))
            {
                return ObjectConversion.DecimalToString;
            }

            if (typeI == typeof(string) && typeO == typeof(decimal))
            {
                return ObjectConversion.StringToDecimal;
            }

            if (typeI == typeof(string) && typeO == typeof(int))
            {
                return ObjectConversion.StringToInt;
            }
            throw new NotImplementedException();
        }

        //TODO: code method that reads the value of a property and assigns it to the right method to convert

        private string IntConvertToString(int value)
        {
            return value.ToString();
        }

        private string DecimalConvertToString(decimal value) {  
            return value.ToString(); 
        }

        private decimal StringConvertToDecimal(string value)
        {
            decimal result;

            if (decimal.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException("Invalid decimal value.");
        }

        private decimal StringConvertToInt(string value)
        {
            int intValue;
            if (int.TryParse(value, out intValue))
            {
                return (decimal)intValue; // Successful conversion to int, cast to decimal
            }

            // Handle conversion failure or invalid input
            // You can throw an exception, return a default value, or handle it based on your requirements
            throw new ArgumentException("Invalid integer value.");
        }
    }
}
