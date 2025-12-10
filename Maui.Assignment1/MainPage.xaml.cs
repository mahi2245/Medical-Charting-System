using Library.Assignment1.Services;
using Maui.Assignment1.ViewModels;

namespace Maui.Assignment1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("PatientDetail?patientId=0");

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
        var id = (BindingContext as MainViewModel)?.SelectedPatient?.Model?.Id ?? 0;
        Shell.Current.GoToAsync($"PatientDetail?patientId={id}");
    }

    private async void SearchClicked(object sender, EventArgs e)
    {
        await (BindingContext as MainViewModel)!.Search();
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