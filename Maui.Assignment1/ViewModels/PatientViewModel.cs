using Library.Assignment1.Models;
using Library.Assignment1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.Assignment1.ViewModels
{
    public class PatientViewModel
    {
        public PatientViewModel()
        {
            Model = new Patient();
            SetUpCommands();
        }

        public PatientViewModel(Patient? model)
        {
            Model = model;
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            if (Model != null)
            {
                PatientService.Current.Patients.Remove(Model);
                Shell.Current.GoToAsync("//Patients");
            }
        }

        private void DoEdit(PatientViewModel? pv)
        {
            if (pv == null)
            {
                return;
            }
            var selectedPatient = pv?.Model?.Name ?? string.Empty;
            Shell.Current.GoToAsync($"PatientDetail?patientName={selectedPatient}");
        }

        public Patient? Model { get; set; }

        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
    }
}