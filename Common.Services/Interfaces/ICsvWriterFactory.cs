namespace Common.Services.Interfaces
{
    internal interface ICsvWriterFactory
    {
        Interfaces.IWriter Create(string filePath);
    }
}
