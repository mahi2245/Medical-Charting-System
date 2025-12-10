using API.Assignment1.Database;
using Library.Assignment1.DTO;
using Library.Assignment1.Models;

namespace API.Assignment1.Enterprise
{
    public class PatientEC
    {
        public IEnumerable<PatientDTO> GetPatients()
        {
            return PatientFilebase.Current.Patients
                .Select(p => new PatientDTO(p))
                .OrderBy(p => p.Id);
        }

        public PatientDTO? GetById(int id)
        {
            var patient = PatientFilebase.Current.Patients
                            .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return null;
            }

            return new PatientDTO(patient);
        }

        public PatientDTO? Delete(int id)
        {
            var existing = PatientFilebase.Current.Patients.FirstOrDefault(p => p.Id == id);

            if (existing != null)
            {
                PatientFilebase.Current.Delete(id);
                return new PatientDTO(existing);
            }

            return null;
        }

        public PatientDTO? AddOrUpdate(PatientDTO? dto)
        {
            if (dto == null)
            {
                return null;
            }

            var modelNotes = new List<MedicalNote>();
            if (dto.MedicalNotes != null)
            {
                foreach (var noteDto in dto.MedicalNotes)
                {
                    modelNotes.Add(new MedicalNote
                    {
                        Date = noteDto.Date,
                        Diagnosis = noteDto.Diagnosis,
                        Prescription = noteDto.Prescription
                    });
                }
            }

            var patient = new Patient
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Birthdate = dto.Birthdate,
                Race = dto.Race,
                Gender = dto.Gender,
                MedicalNotes = modelNotes
            };

            var saved = PatientFilebase.Current.AddOrUpdate(patient);

            return new PatientDTO(saved);
        }

        public IEnumerable<PatientDTO?> Search(string query)
        {
            query = query?.ToUpper() ?? string.Empty;

            return PatientFilebase.Current.Patients
                .Where(p =>
                    (p?.Name?.ToUpper()?.Contains(query) ?? false)
                    || (p?.Address?.ToUpper()?.Contains(query) ?? false)
                    || (p?.Birthdate?.ToUpper()?.Contains(query) ?? false)
                    || (p?.Race?.ToUpper()?.Contains(query) ?? false)
                    || (p?.Gender?.ToUpper()?.Contains(query) ?? false)
                )
                .Select(p => new PatientDTO(p));
        }
    }
}
