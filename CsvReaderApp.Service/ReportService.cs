using System.Reflection;

namespace CsvReaderApp.Services
{
    public class ReportService
    {
        public void Execute<T>(T report) 
        {
            Type type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                // T is of type List<T> or its derived types
                Console.WriteLine("T is of type List<T> or its derived types.");

                IterateGenericType(report, type);
            }
            else
            {
                // T is not of type List<T> or its derived types
                Console.WriteLine("T is not of type List<T> or its derived types.");
            }
        }

        private static void IterateGenericType<T>(T report, Type type)
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
                object currentItem = currentProperty.GetValue(enumerator);
                // Process each item
                Console.WriteLine(currentItem);
            }
        }
    }
}
