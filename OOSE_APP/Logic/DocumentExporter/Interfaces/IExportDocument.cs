namespace Logic.DocumentExporter.Interfaces
{
    public interface IExportDocument<T>
    {
        byte[] ExportToDocument(T objectModel);
    }
}
