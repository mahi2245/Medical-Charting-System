using Library.Assignment1.Data;
using Library.Assignment1.DTO;
using Library.Assignment1.Utilities;
using Newtonsoft.Json;

namespace Library.Assignment1.Services
{
    public class PatientService
    {
        private List<PatientDTO?> patients = new List<PatientDTO?>();

        private PatientService() { }

        private static PatientService? instance;
        private static readonly object instanceLock = new object();

        public static PatientService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PatientService();
                    }
                }
                return instance;
            }
        }

        public List<PatientDTO?> Patients => patients;

        public async Task LoadAsync()
        {
            var response = await new WebRequestHandler().Get("/Patient");

            if (!string.IsNullOrEmpty(response))
            {
                patients = JsonConvert.DeserializeObject<List<PatientDTO?>>(response)
                           ?? new List<PatientDTO?>();
            }
        }

        public async Task<PatientDTO?> AddOrUpdate(PatientDTO? dto)
        {
            if (dto == null)
                return null;

            var payload = await new WebRequestHandler().Post("/Patient", dto);
            var fromServer = JsonConvert.DeserializeObject<PatientDTO>(payload);

            // new patient
            if (dto.Id <= 0)
            {
                patients.Add(fromServer);
            }
            else
            {
                // update
                var match = patients.FirstOrDefault(p => (p?.Id ?? 0) == dto.Id);
                if (match != null)
                {
                    var idx = patients.IndexOf(match);
                    patients[idx] = fromServer;
                }
            }

            return fromServer;
        }

        public async Task<PatientDTO?> Delete(int id)
        {
            var response = await new WebRequestHandler().Delete($"/Patient/{id}");

            var patient = patients.FirstOrDefault(p => (p?.Id ?? -1) == id);

            if (patient != null)
                patients.Remove(patient);

            return patient;
        }
        public async Task<List<PatientDTO?>> Search(QueryRequest query)
        {
            var payload = await new WebRequestHandler().Post("/Patient/Search", query);
            var results = JsonConvert.DeserializeObject<List<PatientDTO?>>(payload);

            patients = results ?? new List<PatientDTO?>();
            return patients;
        }

        public PatientDTO? FindPatient(string name)
        {
            return patients.FirstOrDefault(p =>
                p?.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false);
        }
    }
}
