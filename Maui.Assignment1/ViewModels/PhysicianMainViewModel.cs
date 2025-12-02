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
    public class PhysicianMainViewModel : INotifyPropertyChanged
    {
        public PhysicianMainViewModel()
        {
            InlinePhysician = new PhysicianViewModel();
            InlineCardVisibility = false;
        }

        public ObservableCollection<PhysicianViewModel?> Physicians
        {
            get
            {
                return new ObservableCollection<PhysicianViewModel?>
                    (PhysicianService
                    .Current
                    .Physicians
                    .Where(
                        p => (p?.Name?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (p?.LicenseNumber?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (p?.Specialization?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    )
                    .Select(p => new PhysicianViewModel(p))
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
            NotifyPropertyChanged(nameof(Physicians));
        }

        public PhysicianViewModel? SelectedPhysician { get; set; }
        public string? Query { get; set; }

        public PhysicianViewModel? InlinePhysician { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Delete()
        {
            if (SelectedPhysician == null)
            {
                return;
            }

            PhysicianService.Current.Physicians.Remove(SelectedPhysician?.Model);
            NotifyPropertyChanged(nameof(Physicians));
        }

        public void AddInlinePhysician()
        {
            if (InlinePhysician?.Model != null)
            {
                PhysicianService.Current.AddPhysician(InlinePhysician?.Model);
                NotifyPropertyChanged(nameof(Physicians));

                InlinePhysician = new PhysicianViewModel();
                NotifyPropertyChanged(nameof(InlinePhysician));
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