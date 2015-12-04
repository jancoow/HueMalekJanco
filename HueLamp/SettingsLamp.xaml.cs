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
            hh.hue.setRGBValue((float)Bri.Value,(float)Hue.Value, (float)Sat.Value);
            int hue;
            int sat;
            int bri;
            SolidColorBrush color = new SolidColorBrush(hh.hue.getRGBValue(out bri, out hue,out sat));
            Hue.Value = hue;
            Sat.Value = sat;
            Bri.Value = bri;
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
            NameLight.Text = hh.hue.name;
        }
    }
}
