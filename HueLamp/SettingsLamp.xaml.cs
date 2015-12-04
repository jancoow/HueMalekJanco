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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HueLamp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsLamp : Page
    {
        HueHandler hh;

        public object Colorutil { get; private set; }

        public SettingsLamp()
        {
            this.InitializeComponent();
        }

        public void Slider_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush color = new SolidColorBrush(getColor());
            ColorChanger.Fill = color;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SetOn.IsOn)
            {
                hh.hue.SetPower(true);
            }
            else 
            {
                hh.hue.SetPower(false);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hh = (HueHandler)e.Parameter;
        }

        public Color getColor()
        {
            double hue = ((double)Hue.Value * 360.0f) / 65535.0f;
            double sat = (double)Sat.Value / 255.0f;
            double val = (double)Bri.Value / 255.0f;

            int r, g, b;
            ColorUtil.HsvToRgb(hue, sat, val, out r, out g, out b);
            return Color.FromArgb(255, Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
        }
    }
}
