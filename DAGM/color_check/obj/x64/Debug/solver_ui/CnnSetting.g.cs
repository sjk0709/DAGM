﻿#pragma checksum "..\..\..\..\solver_ui\CnnSetting.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "661331D00FA1C2BB9F5F023CB23A2D26E25A87B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using DAGM.solver_ui;
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


namespace DAGM.solver_ui {
    
    
    /// <summary>
    /// CnnSetting
    /// </summary>
    public partial class CnnSetting : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox gbSolver;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxSolverSetting;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelInfoSolverSetting;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockSolverSetting;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateNewSetting;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GraphFileName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSaveSetting;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\solver_ui\CnnSetting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/dagm;component/solver_ui/cnnsetting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\solver_ui\CnnSetting.xaml"
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
            this.gbSolver = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.ListBoxSolverSetting = ((System.Windows.Controls.ListBox)(target));
            
            #line 28 "..\..\..\..\solver_ui\CnnSetting.xaml"
            this.ListBoxSolverSetting.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBoxSolverSetting_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LabelInfoSolverSetting = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.TextBlockSolverSetting = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.btnCreateNewSetting = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\solver_ui\CnnSetting.xaml"
            this.btnCreateNewSetting.Click += new System.Windows.RoutedEventHandler(this.btnCreateNewSetting_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GraphFileName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.ButtonSaveSetting = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\..\solver_ui\CnnSetting.xaml"
            this.ButtonSaveSetting.Click += new System.Windows.RoutedEventHandler(this.ButtonSaveSetting_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\..\solver_ui\CnnSetting.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

