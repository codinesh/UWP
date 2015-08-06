using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        public LandingPage()
        {
            this.InitializeComponent();
        }

        public UIElement Marker
        {
            get
            {
                Canvas marker = new Canvas();
                Ellipse outer = new Ellipse() { Width = 25, Height = 25 };
                outer.Fill = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                outer.Margin = new Thickness(-12.5, -12.5, 0, 0);
                Ellipse inner = new Ellipse() { Width = 20, Height = 20 };
                inner.Fill = new SolidColorBrush(Colors.Black);
                inner.Margin = new Thickness(-10, -10, 0, 0);
                Ellipse core = new Ellipse() { Width = 10, Height = 10 };
                core.Fill = new SolidColorBrush(Colors.White);
                core.Margin = new Thickness(-5, -5, 0, 0);
                marker.Children.Add(outer);
                marker.Children.Add(inner);
                marker.Children.Add(core);
                return marker;

            }
        }

        private async void Location_Click(object sender, RoutedEventArgs e)
        {
            Repository.GeoLocationRepository locationAPI = new Repository.GeoLocationRepository();
            var position = await locationAPI.GetCurrentPosition();

            Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(Marker, position);
            Windows.UI.Xaml.Controls.Maps.MapControl.SetNormalizedAnchorPoint(Marker, new Point(0.5, 0.5));
            Map.ZoomLevel = 12;
            Map.Center = position;
            Map.MapServiceToken = "AtmksQlJbWwXX6wWzUNx2KRiYj_2PblvGPo0_RicXiM-HCb2epkj0sDAfwQl7hor";
        }
    }
}
