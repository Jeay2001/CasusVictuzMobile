namespace CasusVictuzMobile.MVVM.View;
using CasusVictuzMobile.MVVM.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
public partial class MapsContentView : ContentView
{
    public static readonly BindableProperty LocationIdProperty =
       BindableProperty.Create(
           nameof(LocationId),        // Property name
           typeof(int),               // Property type
           typeof(MapsContentView),   // Owner type
           default(int),              // Default value
           propertyChanged: OnLocationIdChanged); // Callback when the property changes
    public int LocationId
    {
        get => (int)GetValue(LocationIdProperty);
        set => SetValue(LocationIdProperty, value);
    }


    public MapsContentView()
    {
        InitializeComponent();
    }
    private static void OnLocationIdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (MapsContentView)bindable;
        if (newValue is int locationId && locationId > 0)
        {
            view.UpdateMap(locationId);
        }
    }

    // Method to update the map based on the LocationId
    private void UpdateMap(int locationId)
    {
        // Fetch the location details
        Models.Location locationModel = Models.Location.GetById(locationId);

        // Create a new location and map span
        Location location = new Location(locationModel.Latitude, locationModel.Longitude);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);

        // Create and configure the map
        Map map = new Map(mapSpan);
        map.HeightRequest = 200;
        map.HorizontalOptions = LayoutOptions.Fill;
        // Set the map as the content of the view
        Content = map;
    }
}
