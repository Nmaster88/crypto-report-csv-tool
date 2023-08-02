namespace Common.Services
{
    public interface IWriter : IDisposable
    {
        void Open(string filePath);
        void WriteRecords<T>(List<T> list);
        void Close();
    }
}
