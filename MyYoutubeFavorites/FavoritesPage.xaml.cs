using MyYoutubeFavorites.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyYoutubeFavorites
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoritesPage : Page
    {
        //BackgroundWorker bgThread = new BackgroundWorker();
        public ObservableCollection<YtLocalFavChannelList> YtFavoritesList;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FavoritesPage()
        {
            this.InitializeComponent();
            YtFavoritesList = ((App)Application.Current).GetFavoritesList();

            //YtFavoritesList.
        }

        /// <summary>
        /// Invoked, when the favorites -page is navigated to and updates
        /// the favorites list if invoked with drag'n'drop URL from the
        /// web-browser.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = e.Parameter as string;

            if(!String.IsNullOrEmpty(parameter))
            {
                YtWebInterface webIf = new YtWebInterface();
                webIf.UpdateInfo(parameter);
            }
        }
    }
}
