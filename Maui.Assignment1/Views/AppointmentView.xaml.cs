using Library.Assignment1.Models;
using Library.Assignment1.Services;

namespace Maui.Assignment1.Views
{
    [QueryProperty(nameof(AppointmentDate), "appointmentDate")]
    public partial class AppointmentView : ContentPage
    {
        public string? AppointmentDate { get; set; }

        public AppointmentView()
        {
            InitializeComponent();
            BindingContext = new Appointment();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Appointments");
        }

        private void OkClicked(object sender, EventArgs e)
        {
            ValidationMessage.IsVisible = false;

            var patientName = PatientNameEntry.Text;
            var patient = PatientService.Current.FindPatient(patientName);

            if (patient == null)
            {
                ValidationMessage.Text = "Patient not found. Please enter a valid patient name.";
                ValidationMessage.IsVisible = true;
                return;
            }

            var physicianName = PhysicianNameEntry.Text;
            var physician = PhysicianService.Current.FindPhysician(physicianName);

            if (physician == null)
            {
                ValidationMessage.Text = "Physician not found. Please enter a valid physician name.";
                ValidationMessage.IsVisible = true;
                return;
            }

            // parse date
            var dateInput = DateEntry.Text;
            DateTime date;
            if (!DateTime.TryParse(dateInput, out date))
            {
                ValidationMessage.Text = "Invalid date format. Please use MM/DD/YYYY.";
                ValidationMessage.IsVisible = true;
                return;
            }

            // parse time
            var timeInput = TimeEntry.Text;
            DateTime timeResult;
            if (!DateTime.TryParse(timeInput, out timeResult))
            {
                ValidationMessage.Text = "Invalid time format. Please use format like '9:00 AM' or '14:00'.";
                ValidationMessage.IsVisible = true;
                return;
            }

            // combine date and time
            var appointmentDateTime = new DateTime(
                date.Year, 
                date.Month, 
                date.Day,
                timeResult.Hour,
                timeResult.Minute,
                0
            );

            bool scheduled = AppointmentService.Current.ScheduleAppointment(
                appointmentDateTime, 
                patient, 
                physician
            );

            if (!scheduled)
            {
                ValidationMessage.Text = "Appointment could not be scheduled. " +
                    "Ensure it's a weekday between 8 AM and 5 PM, and the physician is available.";
                ValidationMessage.IsVisible = true;
                return;
            }

            Shell.Current.GoToAsync("//Appointments");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            DateEntry.Text = DateTime.Today.ToString("MM/dd/yyyy");
            TimeEntry.Text = "9:00 AM";
            PatientNameEntry.Text = string.Empty;
            PhysicianNameEntry.Text = string.Empty;
        }
    }
}