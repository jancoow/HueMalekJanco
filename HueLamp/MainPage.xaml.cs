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
        Frame frame;
        public MainPage(Frame frame)
        {
            hh = new HueHandler();
            this.frame = frame;
            this.InitializeComponent();
            DataContext = new MainViewModel(hh);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (frame.CanGoBack)
                {
                    frame.GoBack();
                    e.Handled = true;
                    SetOfLights.IsChecked = true;
                }
            };
            this.MySplitview.Content = frame;

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = !MySplitview.IsPaneOpen;
            ((RadioButton)sender).IsChecked = false;
        }

      
        private void SetOfLights_Checked(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = false;

            if (((RadioButton)sender).IsChecked == true)
            {
                frame.Navigate(typeof(Lights), hh);
            }
            else
            {
                ((RadioButton)sender).IsChecked = false;
            }
             
        }

        private void SettingsButton_Checked(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = false;

            if (((RadioButton)sender).IsChecked == true)
            {
                frame.Navigate(typeof(Settings), hh);
            }
            else
            {
                ((RadioButton)sender).IsChecked = false;
            }
        }

        private void AboutButton_Checked(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = false;

            if (((RadioButton)sender).IsChecked == true)
            {
                frame.Navigate(typeof(AboutPage));
            }
            else
            {
                ((RadioButton)sender).IsChecked = false;
            }
        }

        private void GroupsButton_Checked(object sender, RoutedEventArgs e)
        {
            MySplitview.IsPaneOpen = false;

            if (((RadioButton)sender).IsChecked == true)
            {
                frame.Navigate(typeof(Groups), hh);
            }
            else
            {
                ((RadioButton)sender).IsChecked = false;
            }

        }

        private void MySplitviewPane_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X > 50)
            {
                MySplitview.IsPaneOpen = true;
            }
        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -50)
            {
                MySplitview.IsPaneOpen = false;
            }
        }
    }
}
