using MyYoutubeFavorites.Models;
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

namespace MyYoutubeFavorites
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private YtApplicationSettings AppSettings;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SettingsPage()
        {
            this.InitializeComponent();
            AppSettings = ((App)Application.Current).GetApplicationSettings();

            toggleVideoRefresh.IsOn = (AppSettings.autoUpdateVideosOnAppStart == true) ? true : false;
            
        }

        /// <summary>
        /// Handles the "auto video refresh on startup" toggle switch and
        /// saves the setting.
        /// </summary>
        private void ToggleVideoRefresh_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = (ToggleSwitch)sender;

            if(toggle.IsOn)
            {
                AppSettings.autoUpdateVideosOnAppStart = true;
            }
            else
            {
                AppSettings.autoUpdateVideosOnAppStart = false;
            }

            ((App)Application.Current).SetApplicationSettings(AppSettings);
        }
    }
}
