using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.Views
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
            BindingContext = new AccountViewModel(Navigation);
        }
    }
}
