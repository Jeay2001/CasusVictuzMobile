using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class AddCommentPhotoViewModel : ObservableObject
    {
        // Changed from IBaseRepository<Comment> to BaseRepository<Comment>
        private readonly BaseRepository<Comment> _commentRepository;
        private readonly int _eventRecapId;
        private readonly int _currentUserId; // Assume you have a way to get the current user ID

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private string photoPath;

        [ObservableProperty]
        private bool isPhotoSelected;

        [ObservableProperty]
        private bool isBusy;

        public AddCommentPhotoViewModel(int eventRecapId, int currentUserId)
        {
            _commentRepository = new BaseRepository<Comment>();
            _eventRecapId = eventRecapId;
            _currentUserId = currentUserId;
        }

        partial void OnPhotoPathChanged(string value)
        {
            IsPhotoSelected = !string.IsNullOrEmpty(value);
        }

        [RelayCommand]
        public async Task PickPhotoAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    var newFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
                    using (var stream = await result.OpenReadAsync())
                    using (var newStream = File.OpenWrite(newFile))
                        await stream.CopyToAsync(newStream);
                    PhotoPath = newFile;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Photo selection failed: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task SubmitComment()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (string.IsNullOrWhiteSpace(Content))
                {
                    await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter a comment.", "OK");
                    return;
                }

                var comment = new Comment
                {
                    Content = Content,
                    UserId = _currentUserId,
                    EventRecapId = _eventRecapId, // Ensure your Comment model has EventRecapId
                    PhotoPath = PhotoPath,
                    CreatedAt = DateTime.UtcNow
                };

                _commentRepository.SafeEntity(comment);

                if (!string.IsNullOrEmpty(_commentRepository.StatusMessage))
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Comment added successfully.", "OK");
                    // Optionally, navigate back or clear fields
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to add comment.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.StackTrace}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
