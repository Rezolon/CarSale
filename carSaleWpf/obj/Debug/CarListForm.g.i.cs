﻿#pragma checksum "..\..\CarListForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0FB9B2DC7269F57484A293046A984E043091AFF1DD509DBABEB3F8E79BDF950F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using carSaleWpf;


namespace carSaleWpf {
    
    
    /// <summary>
    /// CarListForm
    /// </summary>
    public partial class CarListForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\CarListForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonInfo;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\CarListForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonTrackOrder;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\CarListForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonClose;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\CarListForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridViewCarList;
        
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
            System.Uri resourceLocater = new System.Uri("/carSaleWpf;component/carlistform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CarListForm.xaml"
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
            
            #line 1 "..\..\CarListForm.xaml"
            ((carSaleWpf.CarListForm)(target)).Loaded += new System.Windows.RoutedEventHandler(this.CarListForm_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonInfo = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\CarListForm.xaml"
            this.buttonInfo.Click += new System.Windows.RoutedEventHandler(this.buttonInfo_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonTrackOrder = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\CarListForm.xaml"
            this.buttonTrackOrder.Click += new System.Windows.RoutedEventHandler(this.buttonTrackOrder_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonClose = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\CarListForm.xaml"
            this.buttonClose.Click += new System.Windows.RoutedEventHandler(this.buttonClose_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dataGridViewCarList = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
