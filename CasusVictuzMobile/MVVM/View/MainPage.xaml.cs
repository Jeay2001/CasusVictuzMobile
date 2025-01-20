using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.ViewModel;
using CasusVictuzMobile.MVVM.Views;
using CasusVictuzMobile.Session;


namespace CasusVictuzMobile
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent going back to InstructiePage
            return true;
        }


    }

}
