using Logic.DocumentImporter.Interfaces;
using Logic.Models.DocumentExportEnImport;
using Newtonsoft.Json;
using System.Text;

namespace Logic.DocumentImporter.Onderwijsmodules
{
    public class ImportOnderwijsmoduleFromJsonStringStrategy : IImportDocument<Onderwijsmodule>
    {
        public Onderwijsmodule ImportDocument(byte[] fileContent)
        {
            var onderwijsmodule = ConvertToOnderwijsmoduleObject(fileContent);

            return onderwijsmodule;
        }

        private Onderwijsmodule ConvertToOnderwijsmoduleObject(byte []fileContent)
        {   var jsonString = Encoding.Default.GetString(fileContent);
            var onderwijsmodule = JsonConvert.DeserializeObject<Onderwijsmodule>(jsonString);

            return onderwijsmodule;
        }
    }
}
