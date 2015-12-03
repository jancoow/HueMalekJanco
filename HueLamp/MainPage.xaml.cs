using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HueLamp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HueHandler hh;
        public MainPage()
        {
            hh = new HueHandler();
            this.InitializeComponent();
            DataContext = new MainViewModel(hh);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = !MySplitview.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Lights.IsSelected)
            {
                MyFrame.Navigate(typeof(Lights), hh);
                BackButton.Visibility = Visibility.Collapsed;
            }
            else if (Settings.IsSelected)
            {
                MyFrame.Navigate(typeof(Settings), hh);
                BackButton.Visibility = Visibility.Visible;
            }
            else if (Groups.IsSelected)
            {
                MyFrame.Navigate(typeof(Groups), hh);
                BackButton.Visibility = Visibility.Visible;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        { 
            if (MyFrame.CanGoBack)
            {
               MyFrame.GoBack();
               Lights.IsSelected = true;
            }
        }
    }
}
