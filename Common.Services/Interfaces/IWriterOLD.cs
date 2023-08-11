namespace Common.Services.Interfaces
{
    public interface IWriterOLD : IDisposable
    {
        void Open(string filePath);
        void WriteRecords<T>(List<T> list);
        void Close();
    }
}
