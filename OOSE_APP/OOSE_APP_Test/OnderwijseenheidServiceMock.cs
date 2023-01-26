using Logic.Models;
using Logic.Services.Interfaces;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace OOSE_APP.Test
{
    public class OnderwijseenheidServiceMock : IOnderwijseenheidService
    {
        public List<Onderwijseenheid> Onderwijseenheden { get; set; }

        public OnderwijseenheidServiceMock()
        {
            Onderwijseenheden = new List<Onderwijseenheid>();
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 1, Naam = "Onderwijseenheid 1 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 2, Naam = "Onderwijseenheid 2 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 3, Naam = "Onderwijseenheid 3 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
            Onderwijseenheden.Add(new Onderwijseenheid { Id = 4, Naam = "Onderwijseenheid 4 test", Beschrijving = "Dit is een test beschrijving", Coordinator = "Jan Jansen", Studiepunten = 30 });
        }

        public async Task<List<Onderwijseenheid>> GetAllOnderwijseenheden(string jwtToken)
        {
            return Onderwijseenheden;
        }

        public async Task<Onderwijseenheid> GetOnderwijseenheidById(int id, string jwtToken)
        {
            return Onderwijseenheden.FirstOrDefault(o => o.Id == id);
        }

        public async Task<Logic.Models.DocumentExportEnImport.Onderwijseenheid> GetOnderwijseenheidVoorExportById(int id, string jwtToken)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOnderwijseenheid(int id, Onderwijseenheid updatedUnderwijseenheid, string jwtToken)
        {
            var onderwijseenheid = Onderwijseenheden.FirstOrDefault(o => o.Id == id);
            onderwijseenheid.Naam = updatedUnderwijseenheid.Naam;
            onderwijseenheid.Studiepunten = updatedUnderwijseenheid.Studiepunten;
        }
    }
}
