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
    public sealed partial class WebVideoViewerPage : Page
    {
        private string videoId;

        public WebVideoViewerPage()
        {
            this.InitializeComponent();
            this.Loaded += WebVideoViewerPage_Loaded;
        }

        private void WebVideoViewerPage_Loaded(object sender, RoutedEventArgs e)
        {
            double width = ActualWidth;
            double height = ActualHeight;
            WebVideoView.NavigateToString(GetEmbeddedVideoPlayer((int) width - 50, (int) height - 50));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameters = (YtVideoPlayerParams)e.Parameter;
            videoId = parameters.videoId;
            ShowEmbeddedVideo();
        }

        private void ShowEmbeddedVideo()
        {
            WebVideoView.NavigateToString(GetEmbeddedVideoPlayer(640, 480));
        }

        private string GetEmbeddedVideoPlayer(int width, int height)
        {
            string html = "<html>"
                          + "  <iframe title=\"YouTube video player\" class=\"youtube - player\" type=\"text / html\""
                          + "          width=\"" + width + "\" height=\"" + height + "\""
                          + "          src=\"http://www.youtube.com/embed/" + videoId + "\""
                          + "          frameborder=\"0\" allowFullScreen>"
                          + "  </iframe>"
                          + "</html>";

            return html;
        }

        private void WebVideoPlayer_Unloaded(object sender, RoutedEventArgs e)
        {
            WebVideoView.NavigateToString("<html></html>");
        }
    }
}
