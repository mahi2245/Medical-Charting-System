using Library.Assignment1.Models;
using Library.Assignment1.Services;

namespace Maui.Assignment1.Views
{
    [QueryProperty(nameof(PatientName), "patientName")]
    public partial class PatientView : ContentPage
    {
        public string? PatientName { get; set; }

        public PatientView()
        {
            InitializeComponent();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Patients");
        }

        private void OkClicked(object sender, EventArgs e)
        {
            var patient = BindingContext as Patient;

            if (patient != null)
            {
                var existingPatient = PatientService.Current.FindPatient(PatientName);
                
                if (existingPatient == null)
                {
                    PatientService.Current.AddPatient(patient);
                }
                else
                {
                    existingPatient.Name = patient.Name;
                    existingPatient.Address = patient.Address;
                    existingPatient.Birthdate = patient.Birthdate;
                    existingPatient.Race = patient.Race;
                    existingPatient.Gender = patient.Gender;
                    existingPatient.MedicalNotes = patient.MedicalNotes;
                }
            }

            Shell.Current.GoToAsync("//Patients");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (string.IsNullOrEmpty(PatientName))
            {
                BindingContext = new Patient();
            }
            else
            {
                var patient = PatientService.Current.FindPatient(PatientName);
                if (patient != null)
                {
                    var patientCopy = new Patient
                    {
                        Name = patient.Name,
                        Address = patient.Address,
                        Birthdate = patient.Birthdate,
                        Race = patient.Race,
                        Gender = patient.Gender,
                        MedicalNotes = new List<MedicalNote>(patient.MedicalNotes)
                    };
                    BindingContext = patientCopy;
                }
                else
                {
                    BindingContext = new Patient();
                }
            }
        }

        private void AddMedicalNoteClicked(object sender, EventArgs e)
        {
            var patient = BindingContext as Patient;
            if (patient != null)
            {
                var newNote = new MedicalNote
                {
                    Date = NoteDatePicker.Date,
                    Diagnosis = DiagnosisEditor.Text,
                    Prescription = PrescriptionEditor.Text
                };

                patient.MedicalNotes.Add(newNote);

                DiagnosisEditor.Text = string.Empty;
                PrescriptionEditor.Text = string.Empty;
                NoteDatePicker.Date = DateTime.Now;

                MedicalNotesList.ItemsSource = null;
                MedicalNotesList.ItemsSource = patient.MedicalNotes;
            }
        }
    }
}