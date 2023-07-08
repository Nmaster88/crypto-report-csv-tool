using System;
using System.Reflection;

namespace CsvReaderApp.Services
{
    public class ConvertService
    {
        private List<ObjectProperty> ObjectPropertiesI = new List<ObjectProperty>();
        private List<ObjectProperty> ObjectPropertiesO = new List<ObjectProperty>();
        
        private class ObjectProperty
        {
            public required string Name { get; set; }
            public required string Type { get; set; }
        }
        
        public void ExecuteAssignment<TI,TO>(TI reportOrigin, TO reportDestiny)
        {
            GenericInputTypeRead(reportOrigin);
            GenericOutputTypeRead(reportDestiny);
        }


        private bool TypeIsList(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        private void GenericInputTypeRead<T>(T reportOrigin)
        {
            ObjectPropertiesI = new List<ObjectProperty>();
            GenericListTypeRead<T>(ObjectPropertiesI);
        }

        private void GenericOutputTypeRead<T>(T reportOrigin)
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
    }
}
