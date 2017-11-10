using MyYoutubeFavorites.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyYoutubeFavorites
{
    /// <summary>
    /// Data template for the Favorites items.
    /// </summary>
    public sealed partial class YtFavoritesDataTemplate : UserControl
    {
        // Event handling for the video -page in order to submit the video URL to the video page.
        public delegate void VideoPageNavigationEventHandler(object source, EventArgs e);
        public static event VideoPageNavigationEventHandler OnNavigateParentReady;

        // Data model.
        public Models.YtLocalFavChannelList YtLocalFavChannelList { get { return this.DataContext as Models.YtLocalFavChannelList; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public YtFavoritesDataTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        /// <summary>
        /// Handles video player click event.
        /// </summary>
        private void btnPlayVideo_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            YtVideoId videoIdParam = new YtVideoId();
            videoIdParam.videoId = tag.ToString();
            
            OnNavigateParentReady(this, videoIdParam);
        }

        /// <summary>
        /// Plays the video on Youtube with the default web-browser.
        /// </summary>
        private async void btnPlayOnYoutube_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            await Windows.System.Launcher.LaunchUriAsync(new Uri($"http://www.youtube.com/watch?v={tag}"));
        }

        /// <summary>
        /// Deletes a favorite from the list.
        /// </summary>
        private async void btnDeleteVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tag = ((Button)sender).Tag;

                // Build a confirmation dialog.
                ContentDialog deleteDialog = new ContentDialog()
                {
                    Title = "Delete Channel?",
                    Content = "Are You sure You wan't to delete this channel?",
                    PrimaryButtonText = "Yes",
                    SecondaryButtonText = "Cancel"
                };

                ContentDialogResult result = await deleteDialog.ShowAsync();

                // If the user accepts the delete, remove channel from the collection.
                if (result == ContentDialogResult.Primary)
                {
                    ObservableCollection<YtLocalFavChannelList> YtFavoritesList = ((App)Application.Current).GetFavoritesList();
                    YtFavoritesList.Remove(YtFavoritesList.Where(i => i.channelId.Equals(tag.ToString())).Single());
                    ((App)Application.Current).SetFavoritesList(YtFavoritesList);
                }
            }
            catch(Exception exp)
            {
                exp.ToString();
            }
        }

        /// <summary>
        /// Updates individual channel.
        /// </summary>
        private async void btnUpdateVideo_Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;

            ObservableCollection<YtLocalFavChannelList> YtFavoritesList = ((App)Application.Current).GetFavoritesList();

            YtLocalFavChannelList videoInfo = new YtLocalFavChannelList();
            YtWebInterface webIf = new YtWebInterface();
            videoInfo = await webIf.GetYtChannelVideoList(tag.ToString());

            YtLocalFavChannelList channelItem = YtFavoritesList.Where(i => i.channelId.Equals(tag.ToString())).Single();
            int videoIndex = YtFavoritesList.IndexOf(channelItem);

            if (YtFavoritesList[videoIndex].videoId != videoInfo.videoId)
            {
                YtFavoritesList[videoIndex].imagePath = videoInfo.imagePath;
                YtFavoritesList[videoIndex].publishedAt = videoInfo.publishedAt;
                YtFavoritesList[videoIndex].videoId = videoInfo.videoId;
                YtFavoritesList[videoIndex].videoTitle = videoInfo.videoTitle;
                ((App)Application.Current).SetFavoritesList(YtFavoritesList);
                ImgNewIcon.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Handles channel grid tap event, basically removes the "new" -indication from the top-left corner
        /// (if any).
        /// </summary>
        private void ChannelGridTapped(object sender, TappedRoutedEventArgs e)
        {
            if (ImgNewIcon.Visibility == Visibility.Visible)
            {
                var tag = ((Grid)sender).Tag;
                ObservableCollection<YtLocalFavChannelList> YtFavoritesList = ((App)Application.Current).GetFavoritesList();
                YtLocalFavChannelList channelItem = YtFavoritesList.Where(i => i.channelId.Equals(tag.ToString())).Single();

                int videoIndex = YtFavoritesList.IndexOf(channelItem);
                YtFavoritesList[videoIndex].isNew = false;

                ((App)Application.Current).SetFavoritesList(YtFavoritesList);

                ImgNewIcon.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Handles the visibility of the "new" -indication.
        /// </summary>
        private void ImageDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var value = sender.Tag;

            if(value != null && (bool) value == true)
            {
                sender.Visibility = Visibility.Visible;
            }
        }
    }
}
