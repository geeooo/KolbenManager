﻿#pragma checksum "C:\Users\geoff\Documents\Sources\Kolben\Kolben\Views\Products\ProductsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "654B024336A8C6E7E631E7ED44256595"
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
    partial class ProductsPage : 
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
                    this.RootSplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 2:
                {
                    this.ProductsList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 48 "..\..\..\..\Views\Products\ProductsPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.ProductsList).SelectionChanged += this.ProductsList_SelectionChanged;
                    #line default
                }
                break;
            case 3:
                {
                    this.SearchBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                    #line 41 "..\..\..\..\Views\Products\ProductsPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)this.SearchBox).TextChanged += this.SearchBox_TextChanged;
                    #line 41 "..\..\..\..\Views\Products\ProductsPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)this.SearchBox).SuggestionChosen += this.SearchBox_SuggestionChosen;
                    #line default
                }
                break;
            case 4:
                {
                    this.ProductFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
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
