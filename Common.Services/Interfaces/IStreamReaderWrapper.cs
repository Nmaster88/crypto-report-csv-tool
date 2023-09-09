using System.Text;

namespace Common.Services.Interfaces
{
    public interface IStreamReaderWrapper : IDisposable
    {
        void Open(string filePath); //TODO: delete
        string ReadLine();

        int Read(char[] buffer, int offset, int count);

        Task<int> ReadAsync(char[] buffer, int offset, int count);
    }

}
