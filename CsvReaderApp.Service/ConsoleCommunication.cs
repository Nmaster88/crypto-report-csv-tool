using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Services
{
    public class ConsoleCommunication : ICommunication
    {
        public string ReceiveMessage()
        {
            return Console.ReadLine();
        }

        public void SendMessage(string text)
        {
            Console.WriteLine(text);
        }
    }
}
