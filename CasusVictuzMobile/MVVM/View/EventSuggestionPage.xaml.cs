using CasusVictuzMobile.MVVM.ViewModel;

namespace CasusVictuzMobile.MVVM.View;

public partial class EventSuggestionPage : ContentPage
{
	public EventSuggestionPage()
	{
		InitializeComponent();
        BindingContext = new CreateSuggestionViewModel();
    }
}