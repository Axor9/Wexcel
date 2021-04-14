using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WExel
{
    /// <summary>
    /// Lógica de interacción para DataWindow.xaml
    /// </summary>
    /// 
    public class DatosEventArgs : EventArgs
    {
        public Hoja datos { get; set; }

        public DatosEventArgs(Hoja c) { datos = c; }
    }

    public delegate void NuevaDatosEventHandler(Object sender, DatosEventArgs e);


    public partial class DataWindow : Window
    {

        public event NuevaDatosEventHandler nuevosDatos;
        Hoja ndatos;

        public DataWindow(Hoja datos)
        {
            InitializeComponent();
            ndatos = datos;
            lista.ItemsSource = ndatos.hoja;
        }

        void OnnuevoDatos(Hoja data)
        { if (nuevosDatos != null) nuevosDatos(this, new DatosEventArgs(data)); }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

            try
            {
                Valor valor = new Valor(int.Parse(Box1.Text), int.Parse(Box2.Text));
                ndatos.hoja.Add(valor);
                OnnuevoDatos(ndatos);
            }
            catch
            {
                MostrarError("El valor a añadir debe ser un ENTERO");
            }
            
        }


        private void Eliminar(object sender, KeyEventArgs e)
        {

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
            string mensaje = "Añada los valores de x en el primer cuadro , los y en el segundo\nPulse añadir para representar el punto\n(Para mas dudas consulte el manual)";
            MessageBoxButton botones = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Question;
            MessageBox.Show(mensaje, titulo, botones, icono);
        }




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


        //---------------------------------- Click derecho---------------------------//
        private void Eliminar1_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

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
            ClickDerecho.Visibility = Visibility.Collapsed;

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

            ClickDerecho.Visibility = Visibility.Collapsed;

            foreach (Valor v in ndatos.hoja)
            {
                list.Add(v);
            }
            List <Valor> ordenada = list.OrderBy(Valor => Valor.x).ToList<Valor>();
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

        private void InsertarAntes_Click(object sender, RoutedEventArgs e)
        {
            int indice;
            ClickDerecho.Visibility = Visibility.Collapsed;

            if (lista.SelectedItem != null)
            {
                indice = ndatos.hoja.IndexOf((Valor)lista.SelectedItem);

                try
                {
                    Valor valor = new Valor(int.Parse(Box1.Text), int.Parse(Box2.Text));
                    ndatos.hoja.Insert(indice, valor);
                    OnnuevoDatos(ndatos);
                }
                catch
                {
                    MostrarError("El valor a añadir debe ser un ENTERO");
                }

            }
            else
            {
                MostrarError("Seleccione un valor para eliminar");
            }
        }

        private void InsertarDespues_Click(object sender, RoutedEventArgs e)
        {
            int indice;
            ClickDerecho.Visibility = Visibility.Collapsed;

            if (lista.SelectedItem != null)
            {
                indice = ndatos.hoja.IndexOf((Valor)lista.SelectedItem);

                try
                {
                    Valor valor = new Valor(int.Parse(Box1.Text), int.Parse(Box2.Text));
                    ndatos.hoja.Insert(indice+1, valor);
                    OnnuevoDatos(ndatos);
                }
                catch
                {
                    MostrarError("El valor a añadir debe ser un ENTERO");
                }

            }
            else
            {
                MostrarError("Seleccione un valor para eliminar");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Button_Click(this, null);
        }
        //--------------------------------------------------------------------------//
    }
}
