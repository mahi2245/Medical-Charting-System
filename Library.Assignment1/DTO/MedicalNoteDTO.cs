using Library.Assignment1.Models;

namespace Library.Assignment1.DTO
{
    public class MedicalNoteDTO
    {
        public DateTime Date { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }

        public string Display
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return $"{Date:d}: {Diagnosis} - {Prescription}";
        }

        public MedicalNoteDTO()
        {
        }

        public MedicalNoteDTO(MedicalNote note)
        {
            Date = note.Date;
            Diagnosis = note.Diagnosis;
            Prescription = note.Prescription;
        }
    }
}
