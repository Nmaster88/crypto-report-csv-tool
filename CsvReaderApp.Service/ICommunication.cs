using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Services
{
    public interface ICommunication
    {
        void SendMessage(string text);
        string ReceiveMessage();
    }
}
