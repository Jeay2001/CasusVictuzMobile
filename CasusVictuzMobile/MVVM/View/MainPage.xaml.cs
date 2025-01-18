using CasusVictuzMobile.MVVM.Views;

namespace CasusVictuzMobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMeldingenButtonClicked(object sender, EventArgs e)
        {
            // Navigate to NotificationPage
            await Navigation.PushModalAsync(new NotificationPage());
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent going back to InstructiePage
            return true;
        }


    }

}
