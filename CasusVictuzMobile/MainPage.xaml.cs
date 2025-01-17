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

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }

}
