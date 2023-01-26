namespace Logic.DocumentImporter.Interfaces
{
    public interface IImportDocument<T>
    {
        T ImportDocument(byte[] fileContent);
    }
}
