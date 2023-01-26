using Logic.DocumentExporter.Interfaces;
using Logic.Models.DocumentExportEnImport;
using Newtonsoft.Json;
using System.Text;

namespace Logic.DocumentExporter.Onderwijsmodules
{
    public class ExportOnderwijsmoduleToJsonStrategy : IExportDocument<Onderwijsmodule>
    {
        public byte[] ExportToDocument(Onderwijsmodule objectModel)
        {
            var jsonContentString = ConvertToJson(objectModel);

            return Encoding.Default.GetBytes(jsonContentString);
        }

        private string ConvertToJson(Onderwijsmodule onderwijsmodule)
        {
            var jsonString = JsonConvert.SerializeObject(onderwijsmodule);

            return jsonString;
        }
    }
}
