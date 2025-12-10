using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.Assignment1.DTO;
using Library.Assignment1.Data;
using Library.Assignment1.Services;

namespace Maui.Assignment1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InlinePatient = new PatientViewModel(new PatientDTO());

            InlineCardVisibility = false;

            patients = new ObservableCollection<PatientViewModel?>(
                PatientService.Current.Patients
                    .Select(p => new PatientViewModel(p))
            );
        }

        private ObservableCollection<PatientViewModel?> patients;
        public ObservableCollection<PatientViewModel?> Patients => patients;

        public PatientViewModel? SelectedPatient { get; set; }

        private string? query;
        public string? Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged();
            }
        }

        public PatientViewModel? InlinePatient { get; set; }

        private bool inlineCardVisible;
        public bool InlineCardVisibility
        {
            get => inlineCardVisible;
            set
            {
                if (inlineCardVisible != value)
                {
                    inlineCardVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public async void Refresh()
        {
            var result = await PatientService.Current.Search(new QueryRequest { Content = "" });
            patients = new ObservableCollection<PatientViewModel?>(result.Select(r => new PatientViewModel(r)));
            NotifyPropertyChanged(nameof(Patients));
        }



        public async Task Search()
        {
            var result = await PatientService.Current.Search(new QueryRequest { Content = Query });

            patients = new ObservableCollection<PatientViewModel?>(
                result.Select(r => new PatientViewModel(r))
            );

            NotifyPropertyChanged(nameof(Patients));
        }

        public void Delete()
        {
            if (SelectedPatient == null)
                return;

            _ = PatientService.Current.Delete(SelectedPatient.Model!.Id);

            patients.Remove(SelectedPatient);

            NotifyPropertyChanged(nameof(Patients));
        }

        public async Task<bool> AddInlinePatient()
        {
            try
            {
                var saved = await PatientService.Current.AddOrUpdate(InlinePatient?.Model);

                patients.Add(new PatientViewModel(saved));

                InlinePatient = new PatientViewModel(new PatientDTO());
                NotifyPropertyChanged(nameof(InlinePatient));

                NotifyPropertyChanged(nameof(Patients));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void ExpandCard()
        {
            InlineCardVisibility = !InlineCardVisibility;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
