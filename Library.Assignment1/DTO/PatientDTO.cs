using Library.Assignment1.Models;
using Library.Assignment1.Services;

namespace Library.Assignment1.DTO
{
    public class PatientDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Birthdate { get; set; }
        public string? Race { get; set; }
        public string? Gender { get; set; }
        public List<MedicalNoteDTO> MedicalNotes { get; set; } = new List<MedicalNoteDTO>();

        public string Display
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return $"{Id}. {Name} - {Address} - {Birthdate} - {Race} - {Gender}";
        }

        public PatientDTO(Patient patient)
        {
            Id = patient.Id;
            Name = patient.Name;
            Address = patient.Address;
            Birthdate = patient.Birthdate;
            Race = patient.Race;
            Gender = patient.Gender;
            foreach (var note in patient.MedicalNotes)
            {
                MedicalNotes.Add(new MedicalNoteDTO(note));
            }
        }

        public PatientDTO()
        {

        }

        public PatientDTO(int id)
        {
            var patientCopy = PatientService.Current.Patients
                .FirstOrDefault(p => (p?.Id ?? 0) == id);

            if (patientCopy != null)
            {
                Id = patientCopy.Id;
                Name = patientCopy.Name;
                Address = patientCopy.Address;
                Birthdate = patientCopy.Birthdate;
                Race = patientCopy.Race;
                Gender = patientCopy.Gender;
                MedicalNotes = patientCopy.MedicalNotes;
            }
        }
    }
}
