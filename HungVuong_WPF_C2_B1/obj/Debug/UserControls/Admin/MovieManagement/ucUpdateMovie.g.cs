﻿#pragma checksum "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "615CFB209962A8B13162BA965536935948EE7972"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HungVuong_WPF_C2_B1;
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


namespace HungVuong_WPF_C2_B1 {
    
    
    /// <summary>
    /// ucUpdateMovie
    /// </summary>
    public partial class ucUpdateMovie : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbMain;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtId;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtContent;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRuntime;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUrl;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/HungVuong_WPF_C2_B1;component/usercontrols/admin/moviemanagement/ucupdatemovie.x" +
                    "aml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
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
            this.lbMain = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.txtId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtContent = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtRuntime = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
            this.txtRuntime.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtRuntime_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtUrl = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\..\..\UserControls\Admin\MovieManagement\ucUpdateMovie.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
