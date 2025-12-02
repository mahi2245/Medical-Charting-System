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
    public class AppointmentViewModel
    {
        public AppointmentViewModel()
        {
            Model = new Appointment();
            SetUpCommands();
        }

        public AppointmentViewModel(Appointment? model)
        {
            Model = model;
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((a) => DoEdit(a as AppointmentViewModel));
        }

        private void DoDelete()
        {
            if (Model != null)
            {
                AppointmentService.Current.Appointments.Remove(Model);
                Shell.Current.GoToAsync("//Appointments");
            }
        }

        private void DoEdit(AppointmentViewModel? av)
        {
            if (av == null)
            {
                return;
            }
            var appointmentDate = av?.Model?.Date.ToString("o") ?? string.Empty;
            Shell.Current.GoToAsync($"AppointmentDetail?appointmentDate={appointmentDate}");
        }

        public Appointment? Model { get; set; }

        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
    }
}