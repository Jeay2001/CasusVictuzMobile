using CasusVictuzMobile.MVVM.ViewModels;
using CasusVictuzMobile.MVVM.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : ContentPage
    {
        public EventDetailPage(Event selectedEvent)
        {
            InitializeComponent();
            BindingContext = new EventDetailViewModel(selectedEvent);
        }

        // Share Button Click Handlers
        private async void OnInstaShareClicked(object sender, EventArgs e)
        {
            await ShareEventAsync("Instagram");
        }

        private async void OnXShareClicked(object sender, EventArgs e)
        {
            await ShareEventAsync("X (Twitter)");
        }

        private async void OnWhatsAppShareClicked(object sender, EventArgs e)
        {
            await ShareEventAsync("WhatsApp");
        }

        private async Task ShareEventAsync(string platform)
        {
            var viewModel = BindingContext as EventDetailViewModel;
            if (viewModel != null && viewModel.CurrentEvent != null)
            {
                string eventName = viewModel.CurrentEvent.Name;
                string message = $"Hey! Check out this event: {eventName}. It's going to be awesome!";

                try
                {
                    await Share.RequestAsync(new ShareTextRequest
                    {
                        Text = message,
                        Title = $"Deel via {platform}"
                    });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Fout", $"Er is een fout opgetreden bij het delen: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fout", "Het evenement kan niet worden gedeeld op dit moment.", "OK");
            }
        }
    }
}
