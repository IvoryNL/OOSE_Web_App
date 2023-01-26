using Logic.DocumentImporter.Interfaces;
using Logic.Models.DocumentExportEnImport;
using Newtonsoft.Json;
using System.Text;

namespace Logic.DocumentImporter.Onderwijseenheden
{
    public class ImportOnderwijseenheidFromJsonStringStrategy : IImportDocument<Onderwijseenheid>
    {
        public Onderwijseenheid ImportDocument(byte[] fileContent)
        {
            var onderwijseenheid = ConvertToOnderwijseenheidObject(fileContent);

            return onderwijseenheid;
        }

        private Onderwijseenheid ConvertToOnderwijseenheidObject(byte []fileContent)
        {
            var jsonString = Encoding.Default.GetString(fileContent);
            var onderwijseenheid = JsonConvert.DeserializeObject<Onderwijseenheid>(jsonString);

            return onderwijseenheid;
        }
    }
}
