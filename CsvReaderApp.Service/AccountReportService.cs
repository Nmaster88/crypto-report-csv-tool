using CsvReaderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Services
{
    public interface IAccountReportService
    {
        void SplitByProperty(List<AccountReportResult> accountReportResultList, string propertyName);
    }
    public class AccountReportService : IAccountReportService
    {
        public AccountReportService() { }

        public void SplitByProperty(List<AccountReportResult> accountReportResultList, string propertyName) 
        {
            if (accountReportResultList == null || string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            Dictionary<string, List<AccountReportResult>> keyValuePairs = new Dictionary<string, List<AccountReportResult>>();

            foreach (var item in accountReportResultList)
            {
                // Get the value of the specified property using reflection
                var propertyValue = item.GetType().GetProperty(propertyName)?.GetValue(item)?.ToString();

                if (!string.IsNullOrEmpty(propertyValue))
                {
                    // Check if the propertyValue already exists as a key in the Dictionary
                    if (keyValuePairs.TryGetValue(propertyValue, out var list))
                    {
                        // If the key exists, add the item to the existing list
                        list.Add(item);
                    }
                    else
                    {
                        // If the key doesn't exist, create a new list with the item and add it to the Dictionary
                        keyValuePairs[propertyValue] = new List<AccountReportResult> { item };
                    }
                }
            }
        }
    }
}
