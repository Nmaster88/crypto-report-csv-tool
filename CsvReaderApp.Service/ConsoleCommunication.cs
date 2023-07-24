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
