using Logic.Models;

namespace Presentation.ViewModels.Onderwijs
{
    public class OnderwijsmoduleOverzichtViewModel
    {
        public int OpleidingId { get; set; }

        public List<Onderwijsmodule> Onderwijsmodules { get; set; }
    }
}
