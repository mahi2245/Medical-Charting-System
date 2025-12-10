using Library.Assignment1.DTO;
using Library.Assignment1.Services;
using System.Windows.Input;

namespace Maui.Assignment1.ViewModels
{
    public class PatientViewModel
    {
        public PatientViewModel()
        {
            Model = new PatientDTO();
            SetUpCommands();
        }

        public PatientViewModel(PatientDTO? dto)
        {
            Model = dto ?? new PatientDTO();
            SetUpCommands();
        }

        public PatientDTO? Model { get; set; }

        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                PatientService.Current.Delete(Model.Id);
                Shell.Current.GoToAsync("//MainPage");
            }
        }

        private void DoEdit(PatientViewModel? pv)
        {
            if (pv == null)
            {
                return;
            }

            var selectedId = pv?.Model?.Id ?? 0;

            Shell.Current.GoToAsync($"//Patient?patientId={selectedId}");
        }
    }
}
