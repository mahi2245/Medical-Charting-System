using Library.Assignment1.Models;
using Library.Assignment1.Services;

namespace Maui.Assignment1.Views
{
    [QueryProperty(nameof(PhysicianName), "physicianName")]
    public partial class PhysicianView : ContentPage
    {
        public string? PhysicianName { get; set; }

        public PhysicianView()
        {
            InitializeComponent();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Physicians");
        }

        private void OkClicked(object sender, EventArgs e)
        {
            var physician = BindingContext as Physician;

            if (physician != null)
            {
                var existingPhysician = PhysicianService.Current.FindPhysician(PhysicianName);
                
                if (existingPhysician == null)
                {
                    PhysicianService.Current.AddPhysician(physician);
                }
                else
                {
                    existingPhysician.Name = physician.Name;
                    existingPhysician.LicenseNumber = physician.LicenseNumber;
                    existingPhysician.GraduationDate = physician.GraduationDate;
                    existingPhysician.Specialization = physician.Specialization;
                }
            }

            Shell.Current.GoToAsync("//Physicians");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (string.IsNullOrEmpty(PhysicianName))
            {
                BindingContext = new Physician();
            }
            else
            {
                var physician = PhysicianService.Current.FindPhysician(PhysicianName);
                if (physician != null)
                {
                    var physicianCopy = new Physician
                    {
                        Name = physician.Name,
                        LicenseNumber = physician.LicenseNumber,
                        GraduationDate = physician.GraduationDate,
                        Specialization = physician.Specialization
                    };
                    BindingContext = physicianCopy;
                }
                else
                {
                    BindingContext = new Physician();
                }
            }
        }
    }
}