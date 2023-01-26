using Logic.DocumentExporter.Interfaces;
using Logic.Models.DocumentExportEnImport;
using Newtonsoft.Json;
using System.Text;

namespace Logic.DocumentExporter.Onderwijseenheden
{
    public class ExportOnderwijseenheidToJsonStrategy : IExportDocument<Onderwijseenheid>
    {
        public byte[] ExportToDocument(Onderwijseenheid objectModel)
        {
            var jsonContentString = ConvertToJson(objectModel);

            return Encoding.Default.GetBytes(jsonContentString);
        }

        private string ConvertToJson(Onderwijseenheid onderwijsmodule)
        {
            var jsonString = JsonConvert.SerializeObject(onderwijsmodule);

            return jsonString;
        }
    }
}
