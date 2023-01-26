using Logic.Models;

namespace Presentation.ViewModels.Beoordelingsmodellen
{
    public class BeoordelingsmodelViewModel
    {
        public string GeselecteerdeTentamenId { get; set; }

        public Beoordelingsmodel Beoordelingsmodel { get; set; }

        public List<Tentamen> Tentamens { get; set; }
    }
}
