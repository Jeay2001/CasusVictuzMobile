using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.ViewModel;
using CasusVictuzMobile.Session;
using System.Collections.ObjectModel;

namespace CasusVictuzMobile.MVVM.View;

public partial class RegistrationsPage : ContentPage
{
	public RegistrationsPageViewModel ViewModel { get; set; }
	public RegistrationsPage()
	{
        InitializeComponent();
        ViewModel = new RegistrationsPageViewModel();
        BindingContext = ViewModel;
    }
}