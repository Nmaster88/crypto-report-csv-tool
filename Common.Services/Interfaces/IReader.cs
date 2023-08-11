namespace Common.Services.Interfaces
{
    public interface IReader : IDisposable
    {
        void Open(string filePath);
        List<T> ReadRecords<T>();
        void Close();
    }
}
