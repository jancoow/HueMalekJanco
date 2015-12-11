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
        HueLamp huelamp;
        public SettingsLamp()
        {
            this.InitializeComponent();
        }

        public void Slider_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            huelamp.setRGBValue((float)Red.Value,(float)Green.Value, (float)Blue.Value);
            SolidColorBrush color = new SolidColorBrush(huelamp.getColor());
            ColorChanger.Fill = color;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SetOn.IsOn)
            {
                huelamp.SetPower(true);
            }
            else 
            {
               huelamp.SetPower(false);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            huelamp = (HueLamp)e.Parameter;
            NameLight.Text = huelamp.name;
            int r, g, b;
            huelamp.getRGBValue(out r, out g, out b);
            Red.Value = r;
            Green.Value = g;
            Blue.Value = b;
            SetOn.IsOn = huelamp.OnLamp;
            SolidColorBrush color = new SolidColorBrush(huelamp.getColor());
            ColorChanger.Fill = color;
        }
    }
}
