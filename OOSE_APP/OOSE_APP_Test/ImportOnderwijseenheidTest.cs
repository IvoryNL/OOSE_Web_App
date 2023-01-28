using Logic.DocumentImporter.Onderwijseenheden;
using Logic.Models.DocumentExportEnImport;
using System.Text;

namespace OOSE_APP_Test
{
    public class ImportOnderwijseenheidTest
    {
        private Onderwijseenheid onderwijseenheid;
        public ImportOnderwijseenheidTest()
        {
            onderwijseenheid = new Onderwijseenheid();
            onderwijseenheid.SetImportStrategy(new ImportOnderwijseenheidFromJsonStringStrategy());
        }

        [Fact]
        public void ImportOnderwijseenheidFromJsonTest()
        {
            var jsonBytes = Encoding.Default.GetBytes("{\r\n    \"Naam\":\"Onderwijseenheid 91 test\",\r\n    \"Beschrijving\":\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.\",\r\n    \"Coordinator\":\"Lorem ipsum\",\r\n    \"Studiepunten\":30.00,\r\n    \"Onderwijsmodules\":null,\r\n    \"Leerdoelen\":[\r\n       {\r\n          \"Naam\":\"Leerdoel 911 test\",\r\n          \"Beschrijving\":\"Dit is leerdoel 1\",\r\n          \"Onderwijseenheid\":null,\r\n          \"Leeruitkomsten\":[\r\n             {\r\n                \"Naam\":\"Leeruitkomst 9111 test\",\r\n                \"Beschrijving\":\"Dit is leeruitkomst 1\",\r\n                \"Leerdoel\":null,\r\n                \"Tentamens\":null,\r\n                \"Lessen\":null\r\n             },\r\n             {\r\n                \"Naam\":\"Leeruitkomst 9112 test\",\r\n                \"Beschrijving\":\"Dit is leeruitkomst 2\",\r\n                \"Leerdoel\":null,\r\n                \"Tentamens\":null,\r\n                \"Lessen\":null\r\n             }\r\n          ]\r\n       },\r\n       {\r\n         \"Naam\":\"Leerdoel 912 test\",\r\n         \"Beschrijving\":\"Dit is leerdoel 1\",\r\n         \"Onderwijseenheid\":null,\r\n         \"Leeruitkomsten\":[\r\n            {\r\n               \"Naam\":\"Leeruitkomst 9121 test\",\r\n               \"Beschrijving\":\"Dit is leeruitkomst 1\",\r\n               \"Leerdoel\":null,\r\n               \"Tentamens\":null,\r\n               \"Lessen\":null\r\n            },\r\n            {\r\n               \"Naam\":\"Leeruitkomst 9122 test\",\r\n               \"Beschrijving\":\"Dit is leeruitkomst 2\",\r\n               \"Leerdoel\":null,\r\n               \"Tentamens\":null,\r\n               \"Lessen\":null\r\n            }\r\n         ]\r\n      },\r\n      {\r\n         \"Naam\":\"Leerdoel 913 test\",\r\n         \"Beschrijving\":\"Dit is leerdoel 1\",\r\n         \"Onderwijseenheid\":null,\r\n         \"Leeruitkomsten\":[\r\n            {\r\n               \"Naam\":\"Leeruitkomst 9131 test\",\r\n               \"Beschrijving\":\"Dit is leeruitkomst 1\",\r\n               \"Leerdoel\":null,\r\n               \"Tentamens\":null,\r\n               \"Lessen\":null\r\n            },\r\n            {\r\n               \"Naam\":\"Leeruitkomst 9132 test\",\r\n               \"Beschrijving\":\"Dit is leeruitkomst 2\",\r\n               \"Leerdoel\":null,\r\n               \"Tentamens\":null,\r\n               \"Lessen\":null\r\n            }\r\n         ]\r\n      }\r\n    ],\r\n    \"ExporteerDocument\":{\r\n       \r\n    },\r\n    \"ImporteerDocument\":null\r\n }");
            var inhoud = onderwijseenheid.Importeer(jsonBytes);
            Assert.NotNull(inhoud);
        }
    }
}