﻿#pragma checksum "..\..\EmailPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C2312659161856CB4F6EFCF8842D666519D9A36A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SeniorProjectPrototype;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SeniorProjectPrototype {
    
    
    /// <summary>
    /// EmailPage
    /// </summary>
    public partial class EmailPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView customer_ListView;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox search_Textbox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showAll_Button;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailSubjectTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailBodyTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendEmail_Button;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailTargetTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\EmailPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SeniorProjectPrototype;component/emailpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EmailPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.customer_ListView = ((System.Windows.Controls.ListView)(target));
            
            #line 12 "..\..\EmailPage.xaml"
            this.customer_ListView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Customer_ListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.search_Textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\EmailPage.xaml"
            this.search_Textbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Search_Textbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.showAll_Button = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\EmailPage.xaml"
            this.showAll_Button.Click += new System.Windows.RoutedEventHandler(this.ShowAll_Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.emailSubjectTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.emailBodyTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.sendEmail_Button = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\EmailPage.xaml"
            this.sendEmail_Button.Click += new System.Windows.RoutedEventHandler(this.SendEmail_Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.emailTargetTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

