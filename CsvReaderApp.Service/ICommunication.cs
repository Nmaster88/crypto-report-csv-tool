namespace CsvReaderApp.Services
{
    public interface ICommunication
    {
        void SendMessage(string text);
        string ReceiveMessage();
    }
}
