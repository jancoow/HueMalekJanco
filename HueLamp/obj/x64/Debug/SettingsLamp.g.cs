﻿#pragma checksum "C:\Users\malek\OneDrive\Documenten\SourceTree\Project\HueLamp\SettingsLamp.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1F662B44785C9551DF8C9ADE701E12E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HueLamp
{
    partial class SettingsLamp : 
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
                    global::Windows.UI.Xaml.Controls.ToggleSwitch element1 = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                    #line 42 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.ToggleSwitch)element1).Toggled += this.ToggleSwitch_Toggled;
                    #line default
                }
                break;
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.Slider element2 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 35 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)element2).ValueChanged += this.Slider_ValueChanged;
                    #line 35 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)element2).PointerCaptureLost += this.Slider_PointerCaptureLost;
                    #line default
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Slider element3 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 30 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)element3).ValueChanged += this.Slider_ValueChanged;
                    #line 30 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)element3).PointerCaptureLost += this.Slider_PointerCaptureLost;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Slider element4 = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 25 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)element4).ValueChanged += this.Slider_ValueChanged;
                    #line 25 "..\..\..\SettingsLamp.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)element4).PointerCaptureLost += this.Slider_PointerCaptureLost;
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

