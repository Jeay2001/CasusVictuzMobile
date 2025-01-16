using CasusVictuzMobile.MVVM.View;

namespace CasusVictuzMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }
    }
}
