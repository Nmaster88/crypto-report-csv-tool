namespace Common.Services.Interfaces
{
    public interface IWriter : IDisposable
    {
        void WriteRecords<T>(List<T> list);
    }
}
