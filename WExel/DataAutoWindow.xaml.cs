using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WExel
{
    /// <summary>
    /// Lógica de interacción para DataAutoWindow.xaml
    /// </summary>
    /// 

    public class DatosAutoEventArgs : EventArgs
    {
        public Hoja datos { get; set; }

        public DatosAutoEventArgs(Hoja c) { datos = c; }
    }

    public delegate void NuevaDatosAutoEventHandler(Object sender, DatosAutoEventArgs e);

    public partial class DataAutoWindow : Window
    {
        Hoja ndatos;
        public event NuevaDatosAutoEventHandler nuevosDatos;


        public DataAutoWindow(Hoja datos)
        {
            InitializeComponent();
            ndatos = datos;
            lista.ItemsSource = ndatos.hoja;
        }

        void OnnuevoDatos(Hoja data)
        { if (nuevosDatos != null) nuevosDatos(this, new DatosAutoEventArgs(data)); }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;
            Valor valor;
            ObservableCollection<Valor> valores = new ObservableCollection<Valor>();
            double  yvalor=0;
            char[] separadores = new char[] {','};

            String [] rango = Box_1.Text.Split(separadores);
            String [] expresion= Box_2.Text.Split(separadores);

            if (Expresion.SelectedIndex == -1)
            {
                MostrarError("Seleccione una expresion");
            }
            else
            {
                switch (Expresion.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            for (int i = int.Parse(rango[0]); i <= int.Parse(rango[1]); i++)
                            {
                                yvalor = 0;
                                valor = new Valor();
                                valor.x = i;
                                for (int j = 0; j < expresion.Length; j++)
                                {
                                    yvalor = yvalor + double.Parse(expresion[j]) * Math.Pow(valor.x, expresion.Length - 1 - j);
                                }
                                valor.y = yvalor;
                                valores.Add(valor);
                            }
                            ndatos.hoja.Clear();
                            ndatos.hoja = valores;
                            OnnuevoDatos(ndatos);
                        }
                        catch
                        {
                            MostrarError("Error en el formato");
                        }
                        break;
                    case 1:
                        try
                        {
                            for (int i = int.Parse(rango[0]); i <= int.Parse(rango[1]); i++)
                            {
                                valor = new Valor();
                                valor.x = i;
                                valor.y = int.Parse(expresion[0]) * Math.Sin(valor.x);
                                valores.Add(valor);
                            }
                            ndatos.hoja.Clear();
                            ndatos.hoja = valores;
                            OnnuevoDatos(ndatos);
                        }
                        catch
                        {
                            MostrarError("Error en el formato");
                        }
                        break;
                    case 2:
                        try
                        {
                            for (int i = int.Parse(rango[0]); i <= int.Parse(rango[1]); i++)
                            {
                                valor = new Valor();
                                valor.x = i;
                                valor.y = int.Parse(expresion[0]) * Math.Cos(valor.x);
                                valores.Add(valor);
                            }
                            ndatos.hoja.Clear();
                            ndatos.hoja = valores;
                            OnnuevoDatos(ndatos);
                        }
                        catch
                        {
                            MostrarError("Error en el formato");
                        }
                        break;
                    case 3:
                        try
                        {
                            for (int i = int.Parse(rango[0]); i <= int.Parse(rango[1]); i++)
                            {
                                valor = new Valor();
                                valor.x = i;
                                valor.y = int.Parse(expresion[0]) * Math.Tan(valor.x);
                                valores.Add(valor);
                            }
                            ndatos.hoja.Clear();
                            ndatos.hoja = valores;
                            OnnuevoDatos(ndatos);
                        }
                        catch
                        {
                            MostrarError("Error en el formato");
                        }
                        break;
                    case 4:
                        try
                        {
                            for (int i = int.Parse(rango[0]); i <= int.Parse(rango[1]); i++)
                            {
                                valor = new Valor();
                                valor.x = i;
                                valor.y = int.Parse(expresion[0]) * Math.Log(valor.x);
                                valores.Add(valor);
                            }
                            ndatos.hoja.Clear();
                            ndatos.hoja = valores;
                            OnnuevoDatos(ndatos);
                        }
                        catch
                        {
                            MostrarError("Error en el formato");
                        }
                        break;
                    case 5:
                        try
                        {
                            for (int i = int.Parse(rango[0]); i <= int.Parse(rango[1]); i++)
                            {
                                valor = new Valor();
                                valor.x = i;
                                valor.y = int.Parse(expresion[0]) * Math.Exp(valor.x);
                                valores.Add(valor);
                            }
                            ndatos.hoja.Clear();
                            ndatos.hoja = valores;
                            OnnuevoDatos(ndatos);
                        }
                        catch
                        {
                            MostrarError("Error en el formato");
                        }
                        break;
                }
                lista.ItemsSource = ndatos.hoja;
            }
        }

        private void Eliminar(object sender, KeyEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

            Valor valor = new Valor();
            if (e.Key == Key.Delete)
            {
                valor = (Valor)lista.SelectedItem;
                ndatos.hoja.Remove(valor);
                OnnuevoDatos(ndatos);
            }
        }

        private void Editar(object sender, MouseButtonEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;


            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != lista)
            {
                if (obj.GetType() == typeof(ListViewItem))
                {
                    WindowEditar we = new WindowEditar();
                    we.Owner = this;
                    we.ShowDialog();
                    if (we.DialogResult == true)
                    {
                            Valor v = new Valor();
                            v = (Valor)lista.SelectedItem;
                            int indice = ndatos.hoja.IndexOf(v);
                            ndatos.hoja.RemoveAt(indice);
                            ndatos.hoja.Insert(indice, we.v);
                            OnnuevoDatos(ndatos);

                    }
                    break;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }


        //---------------------------------- Click derecho---------------------------//

        //........Menu........//

        private void Eliminar1_Click(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedItem != null)
            {
                Valor valor = new Valor();
                valor = (Valor)lista.SelectedItem;
                ndatos.hoja.Remove(valor);
                OnnuevoDatos(ndatos);
            }
            else
            {
                MostrarError("Seleccione un valor para eliminar");
            }
        }

        private void Editar1_Click(object sender, RoutedEventArgs e)
        {
            if (lista.SelectedItem != null)
            {
                WindowEditar we = new WindowEditar();
                we.Owner = this;
                we.ShowDialog();
                if (we.DialogResult == true)
                {
                    Valor v = new Valor();
                    v = (Valor)lista.SelectedItem;
                    int indice = ndatos.hoja.IndexOf(v);
                    ndatos.hoja.RemoveAt(indice);
                    ndatos.hoja.Insert(indice, we.v);
                    OnnuevoDatos(ndatos);

                }
            }
            else
            {
                MostrarError("Seleccione un valor para editar");
            }
        }

        private void Ordenar_Click(object sender, RoutedEventArgs e)
        {
            List<Valor> list = new List<Valor>();
            ObservableCollection<Valor> aux = new ObservableCollection<Valor>();

            foreach (Valor v in ndatos.hoja)
            {
                list.Add(v);
            }
            List<Valor> ordenada = list.OrderBy(Valor => Valor.x).ToList<Valor>();
            ndatos.hoja.Clear();
            foreach (Valor v in ordenada)
            {
                ndatos.hoja.Add(v);
            }
            OnnuevoDatos(ndatos);
        }

        private void Cerrar1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //......Movimiento.....//

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TranslateTransform tt = new TranslateTransform();

            Point punto = new Point();
            punto = e.GetPosition(this);

            tt.X = punto.X;
            tt.Y = punto.Y;

            ClickDerecho.RenderTransform = tt;

            ClickDerecho.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

        }

        //---------------------------------FuncionesExtra-----------------------------------------//

        private void MostrarError(string mensaje)
        {
            string titulo = "WExcel";
            MessageBoxButton botones = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Error;
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string titulo = "Ayuda";
            string mensaje = "Rango de 1 a 10 -> 1,10 \nExpresion 1x2 + 2x + 1 -> 1,2,1\n(Para mas dudas consulte el manual)";
            MessageBoxButton botones = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Question;
            MessageBox.Show(mensaje, titulo, botones, icono);

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click(this, null);
            }
        }


    }
}
