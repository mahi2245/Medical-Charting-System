using Library.Assignment1.DTO;
using Library.Assignment1.Services;

namespace Maui.Assignment1.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
    public int PatientId { get; set; }

    public PatientView()
    {
        InitializeComponent();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private async void OkClicked(object sender, EventArgs e)
    {
        await PatientService.Current.AddOrUpdate(BindingContext as PatientDTO);

        await Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PatientId == 0)
        {
            // Creating a new patient
            BindingContext = new PatientDTO();
        }
        else
        {
            // Editing an existing patient
            BindingContext = new PatientDTO(PatientId);
        }
    }

    private void AddMedicalNoteClicked(object sender, EventArgs e)
    {
        var patient = BindingContext as PatientDTO;
        if (patient == null)
        {
            return;
        }

        var newNote = new MedicalNoteDTO
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
