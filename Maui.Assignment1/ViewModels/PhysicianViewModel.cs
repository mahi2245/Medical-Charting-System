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
    public class PhysicianViewModel
    {
        public PhysicianViewModel()
        {
            Model = new Physician();
            SetUpCommands();
        }

        public PhysicianViewModel(Physician? model)
        {
            Model = model;
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PhysicianViewModel));
        }

        private void DoDelete()
        {
            if (Model != null)
            {
                PhysicianService.Current.Physicians.Remove(Model);
                Shell.Current.GoToAsync("//Physicians");
            }
        }

        private void DoEdit(PhysicianViewModel? pv)
        {
            if (pv == null)
            {
                return;
            }
            var selectedPhysician = pv?.Model?.Name ?? string.Empty;
            Shell.Current.GoToAsync($"PhysicianDetail?physicianName={selectedPhysician}");
        }

        public Physician? Model { get; set; }

        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
    }
}