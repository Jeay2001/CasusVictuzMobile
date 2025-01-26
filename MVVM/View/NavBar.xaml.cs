using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.View;

public partial class NavBar : ContentView
{
	public NavBar()
	{
		InitializeComponent();
		BindingContext = new NavBarViewModel();
	}
}