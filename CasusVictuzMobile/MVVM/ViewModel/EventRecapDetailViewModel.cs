using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class EventRecapDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private EventRecap eventRecap;

        [ObservableProperty]
        private string comments;

        // Navigation property to handle page navigation
        [ObservableProperty]
        private INavigation navigation;

        public EventRecapDetailViewModel(EventRecap recap)
        {
            EventRecap = recap;
            LoadComments();
        }

        private void LoadComments()
        {
            BaseRepository<Comment> commentRepo = new BaseRepository<Comment>();
            EventRecap.Comments = commentRepo.GetAllEntities().Where(c => c.EventRecapId == EventRecap.Id).ToList();

            if (EventRecap.Comments != null && EventRecap.Comments.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var comment in EventRecap.Comments)
                {
                    sb.AppendLine($"{comment.User?.Username ?? "Anoniem"}: {comment.Content}");
                }
                Comments = sb.ToString();
            }
            else
            {
                Comments = "Geen reacties beschikbaar.";
            }
        }

        [RelayCommand]
        public async Task AddCommentAsync()
        {
            if (Navigation == null)
            {
                // Handle the absence of Navigation
                await Application.Current.MainPage.DisplayAlert("Error", "Navigation not available.", "OK");
                return;
            }

            // Retrieve the current user from the session
            var currentUser = Session.UserSession.Instance.LoggedInUser;
            if (currentUser == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User not logged in.", "OK");
                return;
            }

            // Create an instance of AddCommentPhotoPage with necessary parameters
            var addCommentPage = new AddCommentPhotoPage(EventRecap.Id, currentUser.Id);

            // Navigate to the AddCommentPhotoPage modally
            await Navigation.PushModalAsync(addCommentPage);
        }
    }
}
