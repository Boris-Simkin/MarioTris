﻿

#pragma checksum "C:\Users\boria\Documents\Visual Studio 2015\Projects\MarioTris\MarioTris\GameView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "91490F737280B08AC32EE9B50B156127"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarioTris
{
    partial class GameView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 51 "..\..\GameView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.refresh_PointerPressed;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 53 "..\..\GameView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.pause_PointerPressed;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 54 "..\..\GameView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.mute_PointerPressed;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 55 "..\..\GameView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.MainMenu_PointerPressed;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


