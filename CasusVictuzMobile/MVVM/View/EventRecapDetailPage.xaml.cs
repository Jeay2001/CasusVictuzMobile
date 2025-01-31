using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.ViewModel;
using Microsoft.Maui.Controls;

namespace CasusVictuzMobile.MVVM.Views
{
    public partial class EventRecapDetailPage : ContentPage
    {
        public EventRecapDetailPage(EventRecap recap)
        {
            InitializeComponent();
            var viewModel = new EventRecapDetailViewModel(recap);
            BindingContext = viewModel;

            // Assign the Navigation property
            viewModel.Navigation = this.Navigation;
        }
    }
}
