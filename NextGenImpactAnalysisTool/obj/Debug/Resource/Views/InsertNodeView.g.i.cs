﻿#pragma checksum "..\..\..\..\Resource\Views\InsertNodeView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DAB93D5702605590B0457D89A7DAAC20"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using NextGenImpactAnalysisTool.Model;
using NextGenImpactAnalysisTool.Views;
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


namespace NextGenImpactAnalysisTool.Views {
    
    
    /// <summary>
    /// InsertNodeView
    /// </summary>
    public partial class InsertNodeView : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNodeName;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Sampledatagrid;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridDatabaseDetails;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridOtherDetails;
        
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
            System.Uri resourceLocater = new System.Uri("/NextGenImpactAnalysisTool;component/resource/views/insertnodeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
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
            this.txtNodeName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Sampledatagrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            
            #line 52 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GridDatabaseDetails = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            
            #line 70 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GridOtherDetails = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            
            #line 85 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click_2);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 97 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 98 "..\..\..\..\Resource\Views\InsertNodeView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

