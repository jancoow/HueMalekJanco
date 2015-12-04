using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Lights : Page
    {
        HueHandler hh;
        public ProgressRing progress;
        public string currentName { get; private set; }
        public Lights()
        {
            this.InitializeComponent();
            this.currentName = currentName;
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            hh = (HueHandler)e.Parameter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Button b = sender as Button;
            //currentName = ((b.Content as StackPanel).Children[0] as TextBlock).Text;
            this.Frame.Navigate(typeof(SettingsLamp), hh);
            
        }
    }
}
