﻿#pragma checksum "..\..\DataWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4EA37BD1689BBFC911953BC4CE52035AB17FD20D90B0F1D947A4A6DAF231F807"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
using WExel;


namespace WExel {
    
    
    /// <summary>
    /// DataWindow
    /// </summary>
    public partial class DataWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Box1;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Box2;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lista;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu ClickDerecho;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Añadir;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem InsetarFinal;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem InsertarAntes;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem InsertarDespues;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Eliminar1;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Editar1;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Ordenar;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Cerrar1;
        
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
            System.Uri resourceLocater = new System.Uri("/WExel;component/datawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DataWindow.xaml"
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
            
            #line 8 "..\..\DataWindow.xaml"
            ((WExel.DataWindow)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 8 "..\..\DataWindow.xaml"
            ((WExel.DataWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 8 "..\..\DataWindow.xaml"
            ((WExel.DataWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\DataWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Box1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Box2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 24 "..\..\DataWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lista = ((System.Windows.Controls.ListView)(target));
            
            #line 25 "..\..\DataWindow.xaml"
            this.lista.KeyDown += new System.Windows.Input.KeyEventHandler(this.Eliminar);
            
            #line default
            #line hidden
            
            #line 25 "..\..\DataWindow.xaml"
            this.lista.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Editar);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ClickDerecho = ((System.Windows.Controls.Menu)(target));
            return;
            case 8:
            this.Añadir = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 9:
            this.InsetarFinal = ((System.Windows.Controls.MenuItem)(target));
            
            #line 36 "..\..\DataWindow.xaml"
            this.InsetarFinal.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.InsertarAntes = ((System.Windows.Controls.MenuItem)(target));
            
            #line 37 "..\..\DataWindow.xaml"
            this.InsertarAntes.Click += new System.Windows.RoutedEventHandler(this.InsertarAntes_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.InsertarDespues = ((System.Windows.Controls.MenuItem)(target));
            
            #line 38 "..\..\DataWindow.xaml"
            this.InsertarDespues.Click += new System.Windows.RoutedEventHandler(this.InsertarDespues_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Eliminar1 = ((System.Windows.Controls.MenuItem)(target));
            
            #line 40 "..\..\DataWindow.xaml"
            this.Eliminar1.Click += new System.Windows.RoutedEventHandler(this.Eliminar1_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Editar1 = ((System.Windows.Controls.MenuItem)(target));
            
            #line 41 "..\..\DataWindow.xaml"
            this.Editar1.Click += new System.Windows.RoutedEventHandler(this.Editar1_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.Ordenar = ((System.Windows.Controls.MenuItem)(target));
            
            #line 42 "..\..\DataWindow.xaml"
            this.Ordenar.Click += new System.Windows.RoutedEventHandler(this.Ordenar_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.Cerrar1 = ((System.Windows.Controls.MenuItem)(target));
            
            #line 44 "..\..\DataWindow.xaml"
            this.Cerrar1.Click += new System.Windows.RoutedEventHandler(this.Cerrar1_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

