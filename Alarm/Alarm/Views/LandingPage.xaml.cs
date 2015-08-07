using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Alarm.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        RandomAccessStreamReference mapIconStreamReference;

        public LandingPage()
        {
            this.InitializeComponent();
            myMap.Loaded += MyMap_Loaded;
            myMap.MapTapped += MyMap_MapTapped;
            myMap.MapDoubleTapped += MyMap_MapDoubleTapped;
        }

        private void MyMap_MapDoubleTapped(MapControl sender, MapInputEventArgs args)
        {
            myMap.MapElements.RemoveAt(myMap.MapElements.Count);
        }

        private async void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            Repository.GeoLocationRepository locationAPI = new Repository.GeoLocationRepository();
            var position = await locationAPI.GetCurrentPosition();
            myMap.Center = new Geopoint(position.Position);
            myMap.ZoomLevel = 12;
        }

        private void MyMap_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {
            var tappedGeoPosition = args.Location.Position;
            string status = "MapTapped at \nLatitude:" + tappedGeoPosition.Latitude + "\nLongitude: " + tappedGeoPosition.Longitude;

            AddMapIcon(args.Location, tappedGeoPosition.Latitude.ToString());
        }

        private void TrafficFlowVisible_Checked(object sender, RoutedEventArgs e)
        {
            myMap.TrafficFlowVisible = true;
        }

        private void trafficFlowVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            myMap.TrafficFlowVisible = false;
        }

        private void btnAddMapIcon_Click(object sender, RoutedEventArgs e)
        {
            AddMapIcon(myMap.Center, myMap.Center.GeoshapeType.ToString());
        }

        private void btnClearMapIcons_Click(object sender, RoutedEventArgs e)
        {
            myMap.MapElements.Clear();
        }

        private async void btnCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            Repository.GeoLocationRepository locationAPI = new Repository.GeoLocationRepository();
            var position = await locationAPI.GetCurrentPosition();
            myMap.Center = new Geopoint(position.Position);
            myMap.ZoomLevel = 18;
        }

        private void AddMapIcon(Geopoint position, string title) {
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = position;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = title;
            mapIcon1.Image = mapIconStreamReference;
            mapIcon1.ZIndex = 0;
            myMap.MapElements.Add(mapIcon1);
        }
    }
}
