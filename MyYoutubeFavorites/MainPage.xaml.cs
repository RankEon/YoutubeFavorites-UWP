using MyYoutubeFavorites.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyYoutubeFavorites
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool isVideoListUpdatedOnStartup = false;
        private YtApplicationSettings AppSettings;
        public string appDataPath;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            appDataPath = ApplicationData.Current.LocalFolder.Path;
            YtFavoritesDataTemplate.OnNavigateParentReady += myControl_OnNavigateParentReady;

            AppSettings = ((App)Application.Current).GetApplicationSettings();

            RefreshAll.Visibility = Visibility.Visible;
            PageTitle.Text = "YouTube Favorites";

            if (isVideoListUpdatedOnStartup == false && AppSettings.autoUpdateVideosOnAppStart == true)
            {
                RefeshAllChannels();
            }
            else
            {
                ContentFrame.Navigate(typeof(FavoritesPage));
            }
        }

        /// <summary>
        /// Invoked when navigating to video viewer -page.
        /// </summary>
        private void myControl_OnNavigateParentReady(object source, EventArgs e)
        {
            YtVideoPlayerParams videoIdParam = new YtVideoPlayerParams();
            videoIdParam.videoId = ((YtVideoId)e).videoId;
            PageTitle.Text = "YouTube Favorites (videoview)";
            ContentFrame.Navigate(typeof(WebVideoViewerPage), videoIdParam);
        }

        /// <summary>
        /// Event handler for the side -menu.
        /// </summary>
        private void SideMenuItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var itemTapped = (ListBoxItem)sender;

            if (itemTapped.Name.Equals("Favorites"))
            {
                RefreshAll.Visibility = Visibility.Visible;
                PageTitle.Text = "YouTube Favorites";  
                ContentFrame.Navigate(typeof(FavoritesPage));
            }
            else if (itemTapped.Name.Equals("RefreshAll"))
            {
                RefeshAllChannels();
            }
            else if (itemTapped.Name.Equals("Settings"))
            {
                RefreshAll.Visibility = Visibility.Collapsed;
                PageTitle.Text = "YouTube Favorites (settings)";
                ContentFrame.Navigate(typeof(SettingsPage));
            }
        }

        /// <summary>
        /// Refreshes all channels in the favorites list.
        /// </summary>
        private async void RefeshAllChannels()
        {
            try
            {
                YtWebInterface webIf = new YtWebInterface();
                ObservableCollection<YtLocalFavChannelList> YtFavoritesList;
                YtFavoritesList = ((App)Application.Current).GetFavoritesList();

                bool updated = false;

                // Null check.
                if(YtFavoritesList == null)
                {
                    throw new Exception("Cannot update empty favorites list, please add channels first.");
                }

                // Start progress ring animation.
                ProgressRing.Visibility = Visibility.Visible;
                ProgressRing.IsActive = true;

                // Update favorites list items and check that the video id is really changed.
                for (int i = 0; i < YtFavoritesList.Count; i++)
                {
                    YtLocalFavChannelList videoInfo = new YtLocalFavChannelList();
                    videoInfo = await webIf.GetYtChannelVideoList(YtFavoritesList[i].channelId);

                    if (YtFavoritesList[i].videoId != videoInfo.videoId)
                    {
                        updated = true;
                        YtFavoritesList[i].imagePath = videoInfo.imagePath;
                        YtFavoritesList[i].publishedAt = videoInfo.publishedAt;
                        YtFavoritesList[i].videoId = videoInfo.videoId;
                        YtFavoritesList[i].videoTitle = videoInfo.videoTitle;
                        YtFavoritesList[i].isNew = true;
                    }
                }

                // Update the list if there has been updates in the list.
                if (updated)
                {
                    ((App)Application.Current).SetFavoritesList(YtFavoritesList);
                    ProgressRing.Visibility = Visibility.Collapsed;
                    ProgressRing.IsActive = false;
                    ContentFrame.Navigate(typeof(FavoritesPage));
                }
                else
                {
                    ProgressRing.Visibility = Visibility.Collapsed;
                    ProgressRing.IsActive = false;

                    if (isVideoListUpdatedOnStartup == false)
                    {
                        ContentFrame.Visibility = Visibility.Visible;
                        ContentFrame.Navigate(typeof(FavoritesPage));
                    }
                }

                // Disable auto-update after the startup.
                isVideoListUpdatedOnStartup = true;
            }
            catch(Exception exp)
            {
                ((App)Application.Current).ShowErrorMessage(exp.Message);
            }
            finally
            {
                isVideoListUpdatedOnStartup = true;
            }
        }

        /// <summary>
        /// Handles side-menu button click.
        /// </summary>
        private void SideMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            SideMenu.IsPaneOpen = !SideMenu.IsPaneOpen;
        }

        /// <summary>
        /// Handles the drop event of URL from the web-browser.
        /// </summary>
        private async void YtFavoritesWindowDataDropped(object sender, DragEventArgs e)
        {
            try
            {
                var items = await e.DataView.GetWebLinkAsync();
                string absUri = items.AbsoluteUri;

                // Refresh (or actually re-load) the content frame.
                ContentFrame.Navigate(typeof(FavoritesPage), absUri);
            }
            catch (Exception exception)
            {
                var dialog = new MessageDialog($"Exception occurred:\n{exception.Message}");
            }
        }

        /// <summary>
        /// Invoked when URL is dragged over the application
        /// </summary>
        private void YtFavoritesWindowDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }

        /// <summary>
        /// Event handler for the channel filter / search autosuggestbox.
        /// </summary>
        private void FavoritesFilter_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var asb = (AutoSuggestBox)sender;
            string queryText = asb.Text;

            if (string.IsNullOrEmpty(queryText))
            {
                return;
            }
            else
            {
                ((App)Application.Current).ClearFilteredResults();
                ((App)Application.Current).ClearYtFavoritesBackup();
                ObservableCollection<YtLocalFavChannelList> YtFavoritesList = ((App)Application.Current).GetFavoritesList();
                var results = YtFavoritesList.Where(i => i.channelTitle.ToLower().Contains(queryText.ToLower())).ToList();

                ObservableCollection<YtLocalFavChannelList> FilteredYtChList = new ObservableCollection<YtLocalFavChannelList>(results);

                ((App)Application.Current).SetFilteredResults(FilteredYtChList);

                ContentFrame.Navigate(typeof(FavoritesPage));
            }
        }

        /// <summary>
        /// Invoked, when the favorites filter (autosuggestbox) is emptied, in order
        /// to load and show the original favorites list instead of the filtered one.
        /// </summary>
        private void FavoritesFilter_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var asb = (AutoSuggestBox)sender;
            string queryText = asb.Text;

            if (string.IsNullOrEmpty(queryText))
            {
                ((App)Application.Current).ClearFilteredResults();
                ((App)Application.Current).ClearYtFavoritesBackup();
                ContentFrame.Navigate(typeof(FavoritesPage));
            }
        }
    }
}
