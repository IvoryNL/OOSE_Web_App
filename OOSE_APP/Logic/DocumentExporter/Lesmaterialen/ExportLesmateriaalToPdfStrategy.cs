using HtmlAgilityPack;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Logic.Constants;
using Logic.DocumentExporter.Interfaces;
using Logic.Models.DocumentExportEnImport;
using Newtonsoft.Json;
using System.Text;

namespace Logic.DocumentExporter.Lesmaterialen
{
    public class ExportLesmateriaalToPdfStrategy : IExportDocument<string>
    {
        public byte[] ExportToDocument(string jsonString)
        {
            return GenereerDocument(jsonString);
        }

        private byte[] GenereerDocument(string jsonString)
        {
            var jsonDocument = ConverteerNaarJsonObject(jsonString);
            return CreateDocument(jsonDocument);
        }

        private JsonDocument ConverteerNaarJsonObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<JsonDocument>(jsonString);
        }

        private byte[] CreateDocument(JsonDocument jsonDocument)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new Document())
                {
                    using (var pdfWriter = PdfWriter.GetInstance(document, memoryStream))
                    {
                        document.Open();

                        foreach (var pageHtml in jsonDocument.Paginas)
                        {
                            AddPage(document, pdfWriter, pageHtml);
                        }

                        document.Close();
                    }
                }
                return memoryStream.ToArray();
            }
        }

        private void AddPage(Document document, PdfWriter pdfWriter, Pagina pagina)
        {
            var htmlString = CreateHtmlPaginaString(pagina);
            document.NewPage();
            using (var stringReader = new StringReader(htmlString))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, document, stringReader);
            }
        }

        private string CreateHtmlPaginaString(Pagina pagina)
        {
            var htmlDocment = new HtmlDocument();
            var htmlNode = HtmlNode.CreateNode("<html><head></head><body></body></html>");
            htmlDocment.DocumentNode.AppendChild(htmlNode);

            var body = htmlDocment.DocumentNode.SelectSingleNode("/html/body");

            foreach (var element in pagina.Inhoud)
            {
                MaakElementEnVoegToe(element, body);
            }

            return htmlDocment.DocumentNode.OuterHtml;
        }

        private void MaakElementEnVoegToe(Content content, HtmlNode body)
        {
            if (content.Type == LesmateriaalElementTypes.TITEL)
            {
                body.AppendChild(MaakTitelNode(content));
            }
            if (content.Type == LesmateriaalElementTypes.HEADER)
            {
                body.AppendChild(MaakHeaderNode(content));
            }
            if (content.Type == LesmateriaalElementTypes.PARAGRAAF)
            {
                body.AppendChild(MaakParagraafNode(content));
            }
        }

        private HtmlNode MaakTitelNode(Content content)
        {
            return HtmlNode.CreateNode($"<h1>{content.Waarde}</h1>");
        }

        private HtmlNode MaakHeaderNode(Content content)
        {
            return HtmlNode.CreateNode($"<h3>{content.Waarde}</h3>");
        }

        private HtmlNode MaakParagraafNode(Content content)
        {
            return HtmlNode.CreateNode($"<p>{content.Waarde}</p>");
        }
    }
}
