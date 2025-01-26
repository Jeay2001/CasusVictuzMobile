using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.Views
{
    public partial class EventRecapDetailPage : ContentPage
    {
        public EventRecapDetailPage(EventRecap recap)
        {
            InitializeComponent();
            BindingContext = new EventRecapDetailViewModel(recap);
        }
    }
}
