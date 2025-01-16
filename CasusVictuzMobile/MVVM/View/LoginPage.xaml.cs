using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel(Navigation);
	}

   
}