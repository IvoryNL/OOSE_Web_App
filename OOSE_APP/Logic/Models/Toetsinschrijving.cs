using Logic.Models.Dto;

namespace Logic.Models
{
    public class Toetsinschrijving
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int TentamenId { get; set; }

        public int PlanningId { get; set; }

        public VolledigeGebruikerModelDto? Student { get; set; }

        public Tentamen? Tentamen { get; set; }

        public Planning? Planning { get; set; }

        public Toetsinschrijving(int studentId, int tentamenId, int planningId)
        {
            StudentId = studentId;
            TentamenId = tentamenId;
            PlanningId = planningId;
        }
    }
}
