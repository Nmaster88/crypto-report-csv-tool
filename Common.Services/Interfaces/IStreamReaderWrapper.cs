using System.Text;

namespace Common.Services.Interfaces
{
    public interface IStreamReaderWrapper : IDisposable
    {
        void Open(string filePath); //TODO: delete
        string ReadLine();
    }

}
