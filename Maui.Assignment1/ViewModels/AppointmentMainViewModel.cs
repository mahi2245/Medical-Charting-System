using Library.Assignment1.Models;
using Library.Assignment1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Assignment1.ViewModels
{
    public class AppointmentMainViewModel : INotifyPropertyChanged
    {
        public AppointmentMainViewModel()
        {
        }

        public ObservableCollection<AppointmentViewModel?> Appointments
        {
            get
            {
                return new ObservableCollection<AppointmentViewModel?>
                    (AppointmentService
                    .Current
                    .Appointments
                    .Where(
                        a => (a?.Patient?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (a?.Physician?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (a?.Date.ToString()?.Contains(Query ?? string.Empty) ?? false)
                    )
                    .Select(a => new AppointmentViewModel(a))
                    );
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Appointments));
        }

        public AppointmentViewModel? SelectedAppointment { get; set; }
        public string? Query { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Delete()
        {
            if (SelectedAppointment == null)
            {
                return;
            }

            AppointmentService.Current.Appointments.Remove(SelectedAppointment?.Model);
            NotifyPropertyChanged(nameof(Appointments));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}