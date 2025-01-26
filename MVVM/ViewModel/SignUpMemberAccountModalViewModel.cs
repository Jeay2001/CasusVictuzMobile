using CasusVictuzMobile.MVVM.Views;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class SignUpMemberAccountModalViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _confirmPassword;
        [ObservableProperty]
        private string _validationMessage;
        [ObservableProperty]
        private bool _hasValidationError;


        public SignUpMemberAccountModalViewModel()
        {

        }

        [RelayCommand]
        private void SubmitData()
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ValidationMessage = "All fields are required.";
                HasValidationError = true;
                return;
            }

            if (Password != ConfirmPassword)
            {
                ValidationMessage = "Passwords do not match.";
                HasValidationError = true;
                return;
            }
            
            ValidationMessage = string.Empty;
            HasValidationError = false;

            UserSession.Instance.LoggedInUser.Username = Username;
            UserSession.Instance.LoggedInUser.Email = Email;
            UserSession.Instance.LoggedInUser.Password = Password;
            UserSession.Instance.LoggedInUser.IsMember = true;
            UserSession.Instance.LoggedInUser.IsAdmin = false;
            UserSession.Instance.LoggedInUser.IsGuest = false;

            UserService userService = new UserService();
            userService.UpdateUser(UserSession.Instance.LoggedInUser);
            UserSession.Instance.Initialize();

            CloseModal();
        }

        [RelayCommand]
        private async Task CloseModal()
        {
            await Application.Current.MainPage.DisplayAlert("Account Registered!","You now have a member account. Refresh your profile page to see the new changes.","Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }


}
