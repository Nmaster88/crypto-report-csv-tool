namespace Common.Services.Interfaces
{
    public interface IStreamReaderWrapper : IDisposable
    {
        void Open(string filePath);
        string ReadLine();
    }

}
