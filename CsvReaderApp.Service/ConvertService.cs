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
            public string Name { get; set; }
            public string Type { get; set; }

        }
        
        public void Execute<T>(T reportOrigin) 
        {
            Type type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                // T is of type List<T> or its derived types
                Console.WriteLine("T is of type List<T> or its derived types.");

                IterateGenericTypeRead(reportOrigin, type);
            }
            else
            {
                // T is not of type List<T> or its derived types
                Console.WriteLine("T is not of type List<T> or its derived types.");
            }
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
            GenericTypeRead<T>(reportOrigin, ObjectPropertiesI);
        }

        private void GenericOutputTypeRead<T>(T reportOrigin)
        {
            ObjectPropertiesO = new List<ObjectProperty>();
            GenericTypeRead<T>(reportOrigin, ObjectPropertiesO);
        }

        /// <summary>
        /// Simplify this method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="report"></param>
        private void GenericTypeRead<T>(T report, List<ObjectProperty> ObjectProperties)
        {
            Type type = typeof(T);

            if (TypeIsList(type))
            {
                // Get the IEnumerable<T> interface
                Type enumerableType = typeof(List<>).MakeGenericType(type.GetGenericArguments());

                // Check if the report object implements IEnumerable<T>
                if (enumerableType.IsAssignableFrom(report.GetType()))
                {
                    // Get the GetEnumerator() method
                    MethodInfo getEnumeratorMethod = enumerableType.GetMethod("GetEnumerator");
                    object enumerator = getEnumeratorMethod.Invoke(report, null);
                    MethodInfo moveNextMethod = enumerator.GetType().GetMethod("MoveNext");
                    PropertyInfo currentProperty = enumerator.GetType().GetProperty("Current");
                    _ = (bool)moveNextMethod.Invoke(enumerator, null);
                    object currentItem = currentProperty.GetValue(enumerator);
                    Type currentItemType = currentItem.GetType();
                    PropertyInfo[] properties = currentItemType.GetProperties();
                    Console.WriteLine($"Properties of the class {currentItemType.Name}:");
                    foreach (PropertyInfo property in properties)
                    {
                        // Process each property value
                        Console.WriteLine($"Name: {property.Name} | Type: {property.PropertyType}");
                        ObjectProperty objProperty = new ObjectProperty();
                        objProperty.Name = property.Name;
                        objProperty.Type = property.PropertyType.Name;
                        ObjectProperties.Add(objProperty);
                    }
                }
                else
                {
                    Console.WriteLine("The provided object is not an IEnumerable<T>.");
                }
            }
            else
            {
                Console.WriteLine($"{type.Name} is not of type List<> or its derived types.");
            }

        }

        private void IterateGenericTypeRead<T>(T report, Type type)
        {
            // Get the IEnumerable<T> interface
            Type enumerableType = typeof(List<>).MakeGenericType(type.GetGenericArguments());

            // Check if the report object implements IEnumerable<T>
            if (enumerableType.IsAssignableFrom(report.GetType()))
            {
                MethodInfo getEnumeratorMethod = enumerableType.GetMethod("GetEnumerator");
                object enumerator = getEnumeratorMethod.Invoke(report, null);
                MethodInfo moveNextMethod = enumerator.GetType().GetMethod("MoveNext");
                PropertyInfo currentProperty = enumerator.GetType().GetProperty("Current");
                _ = (bool)moveNextMethod.Invoke(enumerator, null);
                GetPropertiesOfObject(currentProperty, enumerator);
            }
            else
            {
                Console.WriteLine("The provided object is not an IEnumerable<T>.");
            }
        }

        private void IterateGenericType<T>(T report, Type type)
        {
            // Get the IEnumerable<T> interface
            Type enumerableType = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments());

            // Get the GetEnumerator() method
            MethodInfo getEnumeratorMethod = enumerableType.GetMethod("GetEnumerator");

            // Invoke the GetEnumerator() method to get the enumerator
            object enumerator = getEnumeratorMethod.Invoke(report, null);

            // Get the MoveNext() method and the Current property
            MethodInfo moveNextMethod = enumerator.GetType().GetMethod("MoveNext");
            PropertyInfo currentProperty = enumerator.GetType().GetProperty("Current");

            // Iterate over the elements
            while ((bool)moveNextMethod.Invoke(enumerator, null))
            {
                GetPropertiesOfObject(currentProperty, enumerator);
            }
        }

        private static void GetPropertiesOfObject(PropertyInfo currentProperty, object enumerator)
        {
            object currentItem = currentProperty.GetValue(enumerator);

            // Get the type of the current item
            Type currentItemType = currentItem.GetType();

            // Get all public properties of the current item
            PropertyInfo[] properties = currentItemType.GetProperties();

            Console.WriteLine($"Properties of the class {currentItemType.Name}:");

            // Iterate over each property and print its value
            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(currentItem);

                // Process each property value
                Console.WriteLine($"Name: {property.Name} | Type: {property.PropertyType}");
            }
        }
    }
}
