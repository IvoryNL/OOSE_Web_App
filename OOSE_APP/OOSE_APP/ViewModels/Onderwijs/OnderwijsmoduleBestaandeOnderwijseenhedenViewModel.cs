using Logic.Models;

namespace Presentation.ViewModels.Onderwijs
{
    public class OnderwijsmoduleBestaandeOnderwijseenhedenViewModel
    {
        public Onderwijsmodule Onderwijsmodule { get; set; }

        public string GeselecteerdeOnderwijseenheidId { get; set; }

        public List<Onderwijseenheid> BestaandeOnderwijseenheden { get; set; }
    }
}
