﻿#pragma checksum "E:\Pasi\Ohjelmointi\CSharp\UniversalWindowsPlatform\MyYoutubeFavorites\MyYoutubeFavorites\MyYoutubeFavorites\SettingsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3D2BF20BE42AB3F261C1670491988118"
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
    partial class SettingsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.toggleVideoRefresh = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                    #line 13 "..\..\..\SettingsPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ToggleSwitch)this.toggleVideoRefresh).Toggled += this.ToggleVideoRefresh_Toggled;
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
            return returnValue;
        }
    }
}

