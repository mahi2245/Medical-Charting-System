using Library.Assignment1.DTO;

namespace Library.Assignment1.Models
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public PatientDTO? Patient { get; set; }
        public Physician? Physician { get; set; }

        public override string ToString()
        {
            return $"{Date}: {Patient?.Name} with Physician - {Physician?.Name}";
        }
    }
}