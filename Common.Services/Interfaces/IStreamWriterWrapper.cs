using System.Text;

namespace Common.Services.Interfaces
{
    public interface IStreamWriterWrapper
    {
        Encoding GetEncoding();
        void WriteLine(string text);
    }
}
