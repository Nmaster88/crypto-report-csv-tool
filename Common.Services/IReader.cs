namespace Common.Services
{
    public interface IReader : IDisposable
    {
        void Open(string filePath);
        List<T> ReadRecords<T>();
        void Close();
    }
}
