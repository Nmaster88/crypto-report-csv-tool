using System;
using System.Reflection;

namespace CsvReaderApp.Services
{
    public class ConvertService
    {
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
            Type typeI = typeof(TI);
            Type typeO = typeof(TO);
            if (TypeIsList(typeI) && TypeIsList(typeO))
            {
                IterateGenericTypeRead(reportOrigin, typeI);
                IterateGenericTypeRead(reportOrigin, typeO);
            }
            else
            {
                Console.WriteLine($"{typeI.Name} or {typeO.Name} is not of type List<> or its derived types.");
            }
        }


        private bool TypeIsList(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        private void IterateGenericTypeRead<T>(T report, Type type)
        {
            // Get the IEnumerable<T> interface
            Type enumerableType = typeof(List<>).MakeGenericType(type.GetGenericArguments());

            // Check if the report object implements IEnumerable<T>
            if (enumerableType.IsAssignableFrom(report.GetType()))
            {
                // Get the GetEnumerator() method
                MethodInfo getEnumeratorMethod = enumerableType.GetMethod("GetEnumerator");

                // Invoke the GetEnumerator() method to get the enumerator
                object enumerator = getEnumeratorMethod.Invoke(report, null);

                // Get the MoveNext() method and the Current property
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
