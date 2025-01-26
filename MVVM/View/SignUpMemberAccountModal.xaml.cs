using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.View;

public partial class SignUpMemberAccountModal : ContentPage
{    

    public SignUpMemberAccountModal()
	{
		InitializeComponent();
		this.BindingContext = new SignUpMemberAccountModalViewModel();        
    }
}