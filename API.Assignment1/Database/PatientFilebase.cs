using Library.Assignment1.Models;
using Newtonsoft.Json;

namespace API.Assignment1.Database
{
    public class PatientFilebase
    {
        private string _root;
        private string _patientRoot;
        private static PatientFilebase _instance;

        public static PatientFilebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PatientFilebase();
                }
                return _instance;
            }
        }

        private PatientFilebase()
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _root = $"{home}/temp";
            _patientRoot = $"{_root}/Patients";

            if (!Directory.Exists(_root))
            {
                Directory.CreateDirectory(_root);
            }
            if (!Directory.Exists(_patientRoot))
            {
                Directory.CreateDirectory(_patientRoot);
            }
        }

        public int LastPatientKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Patient AddOrUpdate(Patient patient)
        {
            if (patient.Id <= 0)
            {
                patient.Id = LastPatientKey + 1;
            }

            string path = $"{_patientRoot}/{patient.Id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(patient));

            return patient;
        }

        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();

                foreach (var file in root.GetFiles("*.json"))
                {
                    var patient = JsonConvert.DeserializeObject<Patient>(
                        File.ReadAllText(file.FullName));

                    if (patient != null)
                    {
                        _patients.Add(patient);
                    }
                }

                return _patients;
            }
        }

        public bool Delete(int id)
        {
            string path = $"{_patientRoot}/{id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
    }
}
