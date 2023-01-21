using Logic.Models;

namespace Presentation.ViewModels.Opleidingen
{
    public class OpleidingsprofielViewModel
    {
        public Opleiding OpleidingVanStudent { get; set; }

        public Opleidingsprofiel? OpleidingsprofielVanStudent { get; set; }

        public List<Opleidingsprofiel> Opleidingsprofielen { get; set; }

        public string GeselecteerdeOpleidingsprofielId { get; set; }
    }
}
