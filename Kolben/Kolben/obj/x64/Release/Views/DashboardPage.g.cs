﻿#pragma checksum "C:\Users\geoff\Documents\Sources\Kolben\Kolben\Views\DashboardPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5954EAFA351BDECE24CE8BB65C92A9A0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kolben.Views
{
    partial class DashboardPage : 
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
                    this.FarmButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 19 "..\..\..\Views\DashboardPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.FarmButton).Click += this.FarmButton_Click;
                    #line default
                }
                break;
            case 2:
                {
                    this.RestaurantButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 20 "..\..\..\Views\DashboardPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.RestaurantButton).Click += this.RestaurantButton_Click;
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
