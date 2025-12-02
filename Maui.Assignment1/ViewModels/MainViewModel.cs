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
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InlinePatient = new PatientViewModel();
            InlineCardVisibility = false;
        }

        public ObservableCollection<PatientViewModel?> Patients
        {
            get
            {
                return new ObservableCollection<PatientViewModel?>
                    (PatientService
                    .Current
                    .Patients
                    .Where(
                        p => (p?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (p?.Address?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (p?.Race?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (p?.Gender?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    )
                    .Select(p => new PatientViewModel(p))
                    );
            }
        }

        private bool inlineCardVisibility;
        public bool InlineCardVisibility
        {
            get
            {
                return inlineCardVisibility;
            }

            set
            {
                if (inlineCardVisibility != value)
                {
                    inlineCardVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
        }

        public PatientViewModel? SelectedPatient { get; set; }
        public string? Query { get; set; }

        public PatientViewModel? InlinePatient { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Delete()
        {
            if (SelectedPatient == null)
            {
                return;
            }

            PatientService.Current.Patients.Remove(SelectedPatient?.Model);
            NotifyPropertyChanged(nameof(Patients));
        }

        public void AddInlinePatient()
        {
            if (InlinePatient?.Model != null)
            {
                PatientService.Current.AddPatient(InlinePatient?.Model);
                NotifyPropertyChanged(nameof(Patients));

                InlinePatient = new PatientViewModel();
                NotifyPropertyChanged(nameof(InlinePatient));
            }
        }

        public void ExpandCard()
        {
            InlineCardVisibility = !InlineCardVisibility;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}