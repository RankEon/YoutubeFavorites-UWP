using MyYoutubeFavorites.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MyYoutubeFavorites
{
    enum FileType
    {
        FAVORITES = 0,
        SETTINGS  = 1
    };

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public ObservableCollection<YtLocalFavChannelList> YtFavoritesList;
        public ObservableCollection<YtLocalFavChannelList> YtFavoritesListBackup;
        public YtApplicationSettings AppSettings;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledException;

            YtFavoritesList = new ObservableCollection<YtLocalFavChannelList>();
            YtFavoritesListBackup = new ObservableCollection<YtLocalFavChannelList>();
            AppSettings = new YtApplicationSettings();

            YtOpenFile(FileType.FAVORITES, "favoriteslist.json");
            YtOpenFile(FileType.SETTINGS, "ytsettings.json");
        }

        /// <summary>
        /// Handle undhandled exceptions
        /// </summary>
        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowErrorMessage(e.Message);
        }

        /// <summary>
        /// Shows error message box.
        /// </summary>
        /// <param name="message">Message to show</param>
        public void ShowErrorMessage(string message)
        {
            MessageDialog errMessage = new MessageDialog($"We're sorry, but an exception occurred :(\n\nDetails:\n{message}", "Error Occurred");
            errMessage.Commands.Add(new UICommand("Close") { Id = 0 });
            errMessage.DefaultCommandIndex = 0;
            var result = errMessage.ShowAsync();
        }

        /// <summary>
        /// Gets the Youtube favorites collection
        /// </summary>
        /// <returns>YtFavoritesList observable collection</returns>
        public ObservableCollection<YtLocalFavChannelList> GetFavoritesList()
        {
            return YtFavoritesList;
        }

        /// <summary>
        /// Saves the changes to favorites collection.
        /// </summary>
        /// <param name="favorites">Favorites (observable) collection</param>
        public void SetFavoritesList(ObservableCollection<YtLocalFavChannelList> favorites)
        {
            YtFavoritesList = favorites;
            YtSaveFile(FileType.FAVORITES, "favoriteslist.json");
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <returns>Application settings collection</returns>
        public YtApplicationSettings GetApplicationSettings()
        {
            return AppSettings;
        }

        /// <summary>
        /// Sets the filtered channel list results
        /// </summary>
        /// <param name="channelList">Filtered channel list</param>
        public void SetFilteredResults(ObservableCollection<YtLocalFavChannelList> channelList)
        {
            YtFavoritesListBackup = YtFavoritesList;
            YtFavoritesList = channelList;
        }

        /// <summary>
        /// Clears filtered results and restores original list.
        /// </summary>
        public void ClearFilteredResults()
        {
            if(YtFavoritesListBackup.Count > 0)
            {
                // Deep copy backup contents to original collection by serializing and deserializing
                // backup
                YtFavoritesList = new ObservableCollection<YtLocalFavChannelList>();
                string serialized = JsonConvert.SerializeObject(YtFavoritesListBackup.ToArray(), Formatting.Indented);
                YtFavoritesList = JsonConvert.DeserializeObject<ObservableCollection<YtLocalFavChannelList>>(serialized);

                // Finally, clear the backup collection
                YtFavoritesListBackup.Clear();
            }
        }

        /// <summary>
        /// Clears the favorites list backup (used to preserve original when the list is filtered)
        /// </summary>
        public void ClearYtFavoritesBackup()
        {
            YtFavoritesListBackup.Clear();
        }

        /// <summary>
        /// Saves the application settings.
        /// </summary>
        /// <param name="settings"></param>
        public void SetApplicationSettings(YtApplicationSettings settings)
        {
            AppSettings = settings;
            YtSaveFile(FileType.SETTINGS, "ytsettings.json");
        }

        /// <summary>
        /// Saves a favorites or settings -file.
        /// </summary>
        /// <param name="fileType">File type enum: SETTINGS or FAVORITES</param>
        /// <param name="filename">Filename</param>
        private async void YtSaveFile(FileType fileType, string filename)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(filename, 
                                                                                         Windows.Storage.CreationCollisionOption.ReplaceExisting);
            Windows.Storage.StorageFile jsonFile = await storageFolder.GetFileAsync(filename);
            string serialized = (fileType == FileType.FAVORITES) 
                                  ? JsonConvert.SerializeObject(YtFavoritesList.ToArray(), Formatting.Indented)
                                  : JsonConvert.SerializeObject(AppSettings, Formatting.Indented);

            await Windows.Storage.FileIO.WriteTextAsync(jsonFile, serialized);
        }

        /// <summary>
        /// Opens a saved file (favorites or settings).
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="filename"></param>
        private async void YtOpenFile(FileType fileType, string filename)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile jsonFile = (Windows.Storage.StorageFile) await storageFolder.TryGetItemAsync(filename);

            if (jsonFile != null)
            {
                string jsonContent = await Windows.Storage.FileIO.ReadTextAsync(jsonFile);

                if(fileType == FileType.FAVORITES && !string.IsNullOrEmpty(jsonContent))
                {
                    YtFavoritesList = JsonConvert.DeserializeObject<ObservableCollection<YtLocalFavChannelList>>(jsonContent);
                }
                else if(fileType == FileType.SETTINGS && !string.IsNullOrEmpty(jsonContent))
                {
                    AppSettings = JsonConvert.DeserializeObject<YtApplicationSettings>(jsonContent);
                }
            }
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
