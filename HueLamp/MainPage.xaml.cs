using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
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
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            PageTitle.Text = "Alle Hue Lampen";
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
               MyFrame.CanGoBack ?
               AppViewBackButtonVisibility.Visible :
               AppViewBackButtonVisibility.Collapsed;
            MyFrame.Navigate(typeof(Lights), hh);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = !MySplitview.IsPaneOpen;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemLights.IsSelected)
            {
                MyFrame.Navigate(typeof(Lights), hh);
                PageTitle.Text = "Alle Hue Lampen";
            }
            else if (ItemGroups.IsSelected)
            {
                MyFrame.Navigate(typeof(Groups), hh);
                PageTitle.Text = "Hue groepen";
            }
            else if (ItemAbout.IsSelected)
            {
                MyFrame.Navigate(typeof(AboutPage), hh);
                PageTitle.Text = "About";
            }

        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X > 20)
            {
                 MySplitview.IsPaneOpen = true;
            }
        }

        private void MySplitviewPane_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -20)
            {
                MySplitview.IsPaneOpen = false;
            }
        }

        private void ListBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = false;
        }
    }
}
