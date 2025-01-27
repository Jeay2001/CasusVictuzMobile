using CasusVictuzMobile.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class EventRecapDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private EventRecap eventRecap;

        [ObservableProperty]
        private string comments;

        public EventRecapDetailViewModel(EventRecap recap)
        {
            EventRecap = recap;
            LoadComments();
        }

        private void LoadComments()
        {
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
    }
}
