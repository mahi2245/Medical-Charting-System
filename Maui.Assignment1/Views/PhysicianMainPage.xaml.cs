using Maui.Assignment1.ViewModels;
using System.ComponentModel;

namespace Maui.Assignment1.Views
{
    public partial class PhysicianMainPage : ContentPage
    {
        public PhysicianMainPage()
        {
            InitializeComponent();
            BindingContext = new PhysicianMainViewModel();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("PhysicianDetail?physicianName=");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as PhysicianMainViewModel)?.Refresh();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as PhysicianMainViewModel)?.Delete();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            var selectedName = (BindingContext as PhysicianMainViewModel)?.SelectedPhysician?.Model?.Name ?? string.Empty;
            Shell.Current.GoToAsync($"PhysicianDetail?physicianName={selectedName}");
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            (BindingContext as PhysicianMainViewModel)?.Refresh();
        }

        private void InlineAddClicked(object sender, EventArgs e)
        {
            (BindingContext as PhysicianMainViewModel)?.AddInlinePhysician();
        }

        private void ExpandCardClicked(object sender, EventArgs e)
        {
            (BindingContext as PhysicianMainViewModel)?.ExpandCard();
        }
    }
}