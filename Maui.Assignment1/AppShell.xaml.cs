namespace Maui.Assignment1;

using Maui.Assignment1.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("PatientDetail", typeof(PatientView));
		Routing.RegisterRoute("PhysicianDetail", typeof(PhysicianView));
		Routing.RegisterRoute("AppointmentDetail", typeof(AppointmentView));
	}
}
