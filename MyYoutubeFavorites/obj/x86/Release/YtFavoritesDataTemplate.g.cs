﻿#pragma checksum "E:\Pasi\Ohjelmointi\CSharp\UniversalWindowsPlatform\MyYoutubeFavorites\MyYoutubeFavorites\MyYoutubeFavorites\YtFavoritesDataTemplate.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7ABDEBE41C842A361D106B6F0814772C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyYoutubeFavorites
{
    partial class YtFavoritesDataTemplate : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_FrameworkElement_Tag(global::Windows.UI.Xaml.FrameworkElement obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.Tag = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Media_Imaging_BitmapImage_UriSource(global::Windows.UI.Xaml.Media.Imaging.BitmapImage obj, global::System.Uri value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Uri) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Uri), targetNullValue);
                }
                obj.UriSource = value;
            }
        };

        private class YtFavoritesDataTemplate_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IYtFavoritesDataTemplate_Bindings
        {
            private global::MyYoutubeFavorites.YtFavoritesDataTemplate dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Grid obj2;
            private global::Windows.UI.Xaml.Controls.TextBlock obj6;
            private global::Windows.UI.Xaml.Controls.Image obj8;
            private global::Windows.UI.Xaml.Controls.TextBlock obj9;
            private global::Windows.UI.Xaml.Controls.TextBlock obj10;
            private global::Windows.UI.Xaml.Controls.Button obj11;
            private global::Windows.UI.Xaml.Controls.Button obj12;
            private global::Windows.UI.Xaml.Controls.Button obj13;
            private global::Windows.UI.Xaml.Controls.Button obj14;
            private global::Windows.UI.Xaml.Media.Imaging.BitmapImage obj15;

            public YtFavoritesDataTemplate_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 2:
                        this.obj2 = (global::Windows.UI.Xaml.Controls.Grid)target;
                        break;
                    case 6:
                        this.obj6 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 8:
                        this.obj8 = (global::Windows.UI.Xaml.Controls.Image)target;
                        break;
                    case 9:
                        this.obj9 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 10:
                        this.obj10 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 11:
                        this.obj11 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    case 12:
                        this.obj12 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    case 13:
                        this.obj13 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    case 14:
                        this.obj14 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    case 15:
                        this.obj15 = (global::Windows.UI.Xaml.Media.Imaging.BitmapImage)target;
                        break;
                    default:
                        break;
                }
            }

            // IYtFavoritesDataTemplate_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // YtFavoritesDataTemplate_obj1_Bindings

            public void SetDataRoot(global::MyYoutubeFavorites.YtFavoritesDataTemplate newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::MyYoutubeFavorites.YtFavoritesDataTemplate obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_YtLocalFavChannelList(obj.YtLocalFavChannelList, phase);
                    }
                }
            }
            private void Update_YtLocalFavChannelList(global::MyYoutubeFavorites.Models.YtLocalFavChannelList obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_YtLocalFavChannelList_channelId(obj.channelId, phase);
                        this.Update_YtLocalFavChannelList_channelTitle(obj.channelTitle, phase);
                        this.Update_YtLocalFavChannelList_isNew(obj.isNew, phase);
                        this.Update_YtLocalFavChannelList_videoTitle(obj.videoTitle, phase);
                        this.Update_YtLocalFavChannelList_publishedAt(obj.publishedAt, phase);
                        this.Update_YtLocalFavChannelList_videoId(obj.videoId, phase);
                        this.Update_YtLocalFavChannelList_imagePath(obj.imagePath, phase);
                    }
                }
            }
            private void Update_YtLocalFavChannelList_channelId(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj2, obj, null);
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj13, obj, null);
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj14, obj, null);
                }
            }
            private void Update_YtLocalFavChannelList_channelTitle(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj6, obj, null);
                }
            }
            private void Update_YtLocalFavChannelList_isNew(global::System.Boolean obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj8, obj, null);
                }
            }
            private void Update_YtLocalFavChannelList_videoTitle(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj9, obj, null);
                }
            }
            private void Update_YtLocalFavChannelList_publishedAt(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj10, obj, null);
                }
            }
            private void Update_YtLocalFavChannelList_videoId(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj11, obj, null);
                    XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj12, obj, null);
                }
            }
            private void Update_YtLocalFavChannelList_imagePath(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Media_Imaging_BitmapImage_UriSource(this.obj15, (global::System.Uri) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Uri), obj), null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.ItemBlock = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 12 "..\..\..\YtFavoritesDataTemplate.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)this.ItemBlock).Tapped += this.ChannelGridTapped;
                    #line default
                }
                break;
            case 3:
                {
                    this.VisualStateGroup = (global::Windows.UI.Xaml.VisualStateGroup)(target);
                }
                break;
            case 4:
                {
                    this.NarrowLayout = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.WideLayout = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 6:
                {
                    this.ChannelTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7:
                {
                    this.GridImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 8:
                {
                    this.ImgNewIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                    #line 55 "..\..\..\YtFavoritesDataTemplate.xaml"
                    ((global::Windows.UI.Xaml.Controls.Image)this.ImgNewIcon).DataContextChanged += this.ImageDataContextChanged;
                    #line default
                }
                break;
            case 9:
                {
                    this.GridHeadline = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10:
                {
                    this.GridDate = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11:
                {
                    this.btnPlayVideo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 59 "..\..\..\YtFavoritesDataTemplate.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnPlayVideo).Click += this.btnPlayVideo_Click;
                    #line default
                }
                break;
            case 12:
                {
                    this.btnPlayOnYoutube = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 62 "..\..\..\YtFavoritesDataTemplate.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnPlayOnYoutube).Click += this.btnPlayOnYoutube_Click;
                    #line default
                }
                break;
            case 13:
                {
                    this.btnUpdateVideo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 65 "..\..\..\YtFavoritesDataTemplate.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnUpdateVideo).Click += this.btnUpdateVideo_Click;
                    #line default
                }
                break;
            case 14:
                {
                    this.btnDeleteVideo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 68 "..\..\..\YtFavoritesDataTemplate.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnDeleteVideo).Click += this.btnDeleteVideo_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    YtFavoritesDataTemplate_obj1_Bindings bindings = new YtFavoritesDataTemplate_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

