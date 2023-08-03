namespace Common.Services.Interfaces
{
    public interface IStreamReaderWrapper : IDisposable
    {
        string ReadLine();

    }

}
