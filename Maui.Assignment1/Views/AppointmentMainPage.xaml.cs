using Maui.Assignment1.ViewModels;
using System.ComponentModel;

namespace Maui.Assignment1.Views
{
    public partial class AppointmentMainPage : ContentPage
    {
        public AppointmentMainPage()
        {
            InitializeComponent();
            BindingContext = new AppointmentMainViewModel();
        }

        private void AddClicked(object sender, EventArgs e)
		{
            Shell.Current.GoToAsync("AppointmentDetail?appointmentDate=");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as AppointmentMainViewModel)?.Refresh();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as AppointmentMainViewModel)?.Delete();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            var selectedDate = (BindingContext as AppointmentMainViewModel)?.SelectedAppointment?.Model?.Date.ToString("o") ?? string.Empty;
            Shell.Current.GoToAsync($"AppointmentDetail?appointmentDate={selectedDate}");
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            (BindingContext as AppointmentMainViewModel)?.Refresh();
        }
    }
}