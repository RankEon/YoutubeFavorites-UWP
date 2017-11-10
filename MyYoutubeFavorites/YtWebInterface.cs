using MyYoutubeFavorites.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace MyYoutubeFavorites
{
    /// <summary>
    /// Handles traffic towards Youtube.
    /// </summary>
    public class YtWebInterface
    {
        //BackgroundWorker bgThread = new BackgroundWorker();
        public ObservableCollection<YtLocalFavChannelList> YtFavoritesList;

        /// <summary>
        /// Constructor
        /// </summary>
        public YtWebInterface()
        {
            YtFavoritesList = ((App)Application.Current).GetFavoritesList();
        }

        /// <summary>
        /// Updates the favorites list with channel information
        /// </summary>
        /// <param name="URL">Channel URL</param>
        /// 
        /// Notes/reference: http://stackoverflow.com/questions/30081301/getting-all-videos-of-a-channel-using-youtube-api
        public async void UpdateInfo(string URL)
        {
            string username = GetUserNameFromUrl(URL);
            bool isChannel = IsUrlFromChannel(URL);

            YtChannelInfo channelInfo = new YtChannelInfo();
            YtLocalFavChannelList videoInfo = new YtLocalFavChannelList();

            // Check whether the URL is by channel or username.
            if (isChannel)
            {
                channelInfo.id = username;
                channelInfo.kind = string.Empty;
                channelInfo.etag = string.Empty;
            }
            else
            {
                channelInfo = await GetYtChannelInfoByUser(username);
            }

            // Get channel information
            videoInfo = await GetYtChannelVideoList(channelInfo.id);

            // Fetch the current favorites list
            YtFavoritesList = ((App)Application.Current).GetFavoritesList();

            // Check whether the channel already exists in the favorites list and throw
            // an exception if so.
            try
            {                
                if (YtFavoritesList != null)
                {
                    YtLocalFavChannelList channelItem = YtFavoritesList.Where(i => i.channelId.Equals(videoInfo.channelId)).SingleOrDefault();

                    if (channelItem != null)
                    {
                        throw new Exception("Item already exists!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageDialog errMessage = new MessageDialog($"Channel (id={videoInfo.channelId}) already exists!");
                errMessage.Commands.Add(new UICommand("OK") { Id = 0 });
                errMessage.DefaultCommandIndex = 0;

                var result = await errMessage.ShowAsync();
                return;
            }

            // Update with username and information whether it's a channel.
            videoInfo.username = username;
            videoInfo.isChannel = isChannel;

            YtFavoritesList.Add(videoInfo);

            // Save to: C:\Users\Pasi\AppData\Local\Packages\ebade9e5-3eb8-4ce8-a6c4-05919d244312_xd9n95tjqv3aw\LocalState
            ((App)Application.Current).SetFavoritesList(YtFavoritesList);
        }

        /// <summary>
        /// Gets the channel information by username or channel ID.
        /// </summary>
        /// <param name="username">Username or channel id.</param>
        /// <returns>Channel information (YtChannelInfo)</returns>
        public async Task<YtChannelInfo> GetYtChannelInfoByUser(string username)
        {
            YtChannelInfo channelInfo = new YtChannelInfo();
            string webResp;

            using (var httpClient = new HttpClient())
            {
                webResp = await httpClient.GetStringAsync(YtApiWebQueryTemplates.GetChannelInfoQuery(YtApiWebQueryTemplates.GetYoutubeApiKey(), username));
            }

            JObject googleSearch = JObject.Parse(webResp);
            IList<JToken> results = googleSearch["items"].Children().ToList();

            channelInfo.etag = results[0]["etag"].ToString();
            channelInfo.kind = results[0]["kind"].ToString();
            channelInfo.id = results[0]["id"].ToString();

            return channelInfo;
        }

        /// <summary>
        /// Gets the newest video information from the channel.
        /// </summary>
        /// <param name="channelId">Username or channel id.</param>
        /// <returns>Newest video information.</returns>
        public async Task<YtLocalFavChannelList> GetYtChannelVideoList(string channelId)
        {
            YtLocalFavChannelList videoList = new YtLocalFavChannelList();
            string webResp;

            using (var httpClient = new HttpClient())
            {
                webResp = await httpClient.GetStringAsync(YtApiWebQueryTemplates.GetChannelVideoListQuery(YtApiWebQueryTemplates.GetYoutubeApiKey(), channelId));
            }

            JObject googleSearch = JObject.Parse(webResp);
            IList<JToken> results = googleSearch["items"].Children().ToList();

            videoList.channelId = channelId;
            videoList.channelTitle = results[0]["snippet"]["channelTitle"].ToString();
            videoList.videoTitle = results[0]["snippet"]["title"].ToString();
            videoList.videoId = results[0]["id"]["videoId"].ToString();
            videoList.imagePath = results[0]["snippet"]["thumbnails"]["default"]["url"].ToString();

            DateTime pubTime = DateTime.Parse(results[0]["snippet"]["publishedAt"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
            videoList.publishedAt = $"{pubTime.Date.DayOfWeek}, {pubTime.Date.Day}.{pubTime.Date.Month}.{pubTime.Date.Year} at {pubTime.TimeOfDay.ToString()}";

            return videoList;
        }

        /// <summary>
        /// Checks whether the URL is from channel (as opposed to username).
        /// </summary>
        /// <param name="URL">Youtube URL.</param>
        /// <returns>True == URL is from channel, False = URL is from username.</returns>
        private bool IsUrlFromChannel(string URL)
        {
            return (URL.Contains("channel")) ? true : false;
        }

        /// <summary>
        /// Extracts the username from URL.
        /// </summary>
        /// <param name="URL">Youtube URL.</param>
        /// <returns>Username</returns>
        private string GetUserNameFromUrl(string URL)
        {
            string[] urlParts = URL.Split('/');
            return (urlParts.Last().Equals("videos")) ? urlParts[urlParts.Length - 2] : urlParts.Last();
        }
    }
}
