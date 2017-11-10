using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyYoutubeFavorites.Models
{
    /// <summary>
    /// Constains Youtube API -key and Web API queries.
    /// </summary>
    public static class YtApiWebQueryTemplates
    {
        public static string GetYoutubeApiKey()
        {
            // Remove/comment the following line after you have set the API key.
            #error Obtain your free Youtube API -key from Google and add to YoutubeDataModel.cs
            return "<INSERT YOUR YOUTUBE API KEY HERE>";
        }

        public static string GetChannelInfoQuery(string apikey, string user)
        {
            return $"https://www.googleapis.com/youtube/v3/channels?key={apikey}&forUsername={user}&part=id";
        }

        public static string GetChannelVideoListQuery(string apikey, string channelId)
        {
            return $"https://www.googleapis.com/youtube/v3/search?part=snippet&channelId={channelId}&maxResults=10&order=date&type=video&key={apikey}";
        }

        public static string GetChannelProfilePictureUrl(string apikey, string channelId)
        {
            return $"https://www.googleapis.com/youtube/v3/channels?part=snippet&id={channelId}&fields=items%2Fsnippet%2Fthumbnails&key={apikey}";
        }
    }

    /// <summary>
    /// Channel info structure.
    /// </summary>
    public class YtChannelInfo
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Favorites structure
    /// </summary>
    public class YtLocalFavChannelList
    {
        public string username { get; set; }
        public string channelId { get; set; }
        public string channelTitle { get; set; }
        public string videoId { get; set; }
        public string publishedAt { get; set; }
        public string imagePath { get; set; }
        public string videoTitle { get; set; }
        public bool isChannel { get; set; }
        public bool isNew { get; set; } = false;
        // DateTime d2 = DateTime.Parse("2010-08-20T15:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    /// <summary>
    /// Video player parameters.
    /// </summary>
    public class YtVideoPlayerParams
    {
        public string videoId { get; set; }
    }

    /// <summary>
    /// Video player parameter event argument.
    /// </summary>
    public class YtVideoId : EventArgs
    {
        public string videoId { get; set; }
    }

    /// <summary>
    /// Application settings
    /// </summary>
    public class YtApplicationSettings
    {
        public bool autoUpdateVideosOnAppStart { get; set; } = false;
    }
}
