using System.Text;

namespace Common.Services.Interfaces
{
    public interface IStreamWriterWrapper
    {
        Encoding GetEncoding();
        void WriteLine(string text);
        void Flush();
        Task FlushAsync();
        void Write(char[] buffer, int index, int count);
        Task WriteAsync(char[] buffer, int index, int count);
        void Dispose();
    }
}
