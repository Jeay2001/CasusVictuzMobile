using Microsoft.Maui.Controls;
using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.Views
{
    public partial class AddCommentPhotoPage : ContentPage
    {
        public AddCommentPhotoPage(int eventRecapId, int currentUserId)
        {
            InitializeComponent();
            BindingContext = new AddCommentPhotoViewModel(eventRecapId, currentUserId);
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
