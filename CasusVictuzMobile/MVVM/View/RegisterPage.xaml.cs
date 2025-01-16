using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.View;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel(Navigation);
	}
}