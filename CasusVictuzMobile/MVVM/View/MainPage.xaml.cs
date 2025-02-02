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
            if (Session.UserSession.Instance.LoggedInUser.IsGuest)
            {
                SuggestionButton.IsVisible = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent going back to InstructiePage
            return true;
        }

        // Share Button Click Handlers
        private void OnInstaShareClicked(object sender, EventArgs e)
        {
            // Placeholder for Instagram share functionality
            DisplayAlert("Share", "Share via Instagram clicked!", "OK");
        }

        private void OnXShareClicked(object sender, EventArgs e)
        {
            // Placeholder for X (formerly Twitter) share functionality
            DisplayAlert("Share", "Share via X clicked!", "OK");
        }

        private void OnWhatsAppShareClicked(object sender, EventArgs e)
        {
            // Placeholder for WhatsApp share functionality
            DisplayAlert("Share", "Share via WhatsApp clicked!", "OK");
        }
    }
}
