namespace CsvReaderApp.Services.Utils
{
    public class DateTimeWithMarginComparer : IEqualityComparer<DateTime>
    {
         public bool Equals(DateTime x, DateTime y)
        {
            // Compare DateTime values within a 1-second margin of error
            return Math.Abs((x - y).TotalSeconds) <= 1;
        }

        public int GetHashCode(DateTime obj)
        {
            // Generate hash code based on the DateTime values up to the minute
            int hash = obj.Year.GetHashCode() ^
                       obj.Month.GetHashCode() ^
                       obj.Day.GetHashCode() ^
                       obj.Hour.GetHashCode() ^
                       obj.Minute.GetHashCode();

            return hash;
        }
    }
}
