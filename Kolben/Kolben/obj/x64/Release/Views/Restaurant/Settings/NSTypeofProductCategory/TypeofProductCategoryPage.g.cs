﻿#pragma checksum "C:\Users\geoff\Documents\Sources\Kolben\Kolben\Views\Restaurant\Settings\NSTypeofProductCategory\TypeofProductCategoryPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D5D7C7DB179813C644C9BB3C50B98354"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kolben.Views.Restaurant.Settings.NSTypeofProductCategory
{
    partial class TypeofProductCategoryPage : 
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
                    this.TypeofProductCategoryPageElement = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2:
                {
                    this.RootSplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 3:
                {
                    this.TypeofProductCategoryList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 54 "..\..\..\..\..\..\Views\Restaurant\Settings\NSTypeofProductCategory\TypeofProductCategoryPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.TypeofProductCategoryList).SelectionChanged += this.TypeofProductCategoryList_SelectionChanged;
                    #line default
                }
                break;
            case 4:
                {
                    this.TypeofProductCategoryFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
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

