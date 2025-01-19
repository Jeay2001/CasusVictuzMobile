// EventDetailPage.xaml.cs
using CasusVictuzMobile.MVVM.ViewModels;
using CasusVictuzMobile.MVVM.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

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
    }
}
