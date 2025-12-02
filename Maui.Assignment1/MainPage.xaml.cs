using Maui.Assignment1.ViewModels;
using System.ComponentModel;

namespace Maui.Assignment1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("PatientDetail?patientName=");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as MainViewModel)?.Refresh();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel)?.Delete();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            var selectedName = (BindingContext as MainViewModel)?.SelectedPatient?.Model?.Name ?? string.Empty;
            Shell.Current.GoToAsync($"PatientDetail?patientName={selectedName}");
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel)?.Refresh();
        }

        private void InlineAddClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel)?.AddInlinePatient();
        }

        private void ExpandCardClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel)?.ExpandCard();
        }
    }
}