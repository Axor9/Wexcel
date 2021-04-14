using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para PropertiesWindow.xaml
    /// </summary>
    /// 

    public partial class PropertiesWindow : Window
    {
        public Hoja hoja { get; set; }
        

        private double[] tamanos = { 1, 2, 4, 6, 8, 10, 14, 16, 20 };
        private double[] opacidad = { 0, 0.25, 0.5, 0.75, 1};
        private String nombre;
        private String brocha;
        private String brochalinea;
        private String brochaseleccion;
        private int tipografica;
        private double tamano;
        private bool guardarcambios;

        public PropertiesWindow(Hoja hojaseleccionada)
        {
            InitializeComponent();

            hoja = hojaseleccionada;

            nombre = hojaseleccionada.nombre;
            brocha = hojaseleccionada.brocha;
            brochalinea = hojaseleccionada.brochalinea;
            brochaseleccion = hojaseleccionada.brochaseleccion;
            tamano = hojaseleccionada.tamano;
            tipografica = hojaseleccionada.tipografica;


            Nombre.Text = hoja.nombre;
            Tamaño.ItemsSource = tamanos;
            Tamaño.SelectedItem = hoja.tamano;
            Tamaño.Text = hoja.tamano.ToString();

            Opacidad.ItemsSource = opacidad;
            Opacidad.SelectedItem = hoja.opacidad;
            Opacidad.Text = hoja.opacidad.ToString();

            ColorRelleno.ItemsSource = typeof(Colors).GetProperties();
            ColorLinea.ItemsSource = typeof(Colors).GetProperties();
            ColorSeleccion.ItemsSource = typeof(Colors).GetProperties();

            ColorRelleno.SelectedItem = typeof(Colors).GetProperty(hoja.brocha);
            ColorLinea.SelectedItem = typeof(Colors).GetProperty(hoja.brochalinea);
            ColorSeleccion.SelectedItem = typeof(Colors).GetProperty(hoja.brochaseleccion);

            MarcaGrafica(hojaseleccionada.tipografica);

        }

        private void Color_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (PropertyInfo propertyInfo in typeof(Colors).GetProperties())
            {
                if (propertyInfo.Equals(ColorRelleno.SelectedItem))
                {
                    hoja.brocha = propertyInfo.Name;
                    break;
                }
            }
        }
        private void Color_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {

            foreach (PropertyInfo propertyInfo in typeof(Colors).GetProperties())
            {
                if (propertyInfo.Equals(ColorLinea.SelectedItem))
                {
                    hoja.brochalinea = propertyInfo.Name;
                    break;
                }
            }
        }
        private void ColorSeleccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (PropertyInfo propertyInfo in typeof(Colors).GetProperties())
            {
                if (propertyInfo.Equals(ColorSeleccion.SelectedItem))
                {
                    hoja.brochaseleccion = propertyInfo.Name;
                    break;
                }
            }
        }

        private void Puntos_Click(object sender, RoutedEventArgs e)
        {
            if (Puntos.IsChecked == true)
            {
                hoja.tipografica = 2;
                Barras.IsChecked = false;
                Linea.IsChecked = false;
                Columnas.IsChecked = false;
                Areas.IsChecked = false;
            }
        }

        private void Barras_Click(object sender, RoutedEventArgs e)
        {
            if (Barras.IsChecked == true)
            {
                hoja.tipografica = 3;
                Puntos.IsChecked = false;
                Linea.IsChecked = false;
                Columnas.IsChecked = false;
                Areas.IsChecked = false;

            }
        }

        private void Linea_Click(object sender, RoutedEventArgs e)
        {
            if (Linea.IsChecked == true)
            {
                hoja.tipografica = 0;
                Puntos.IsChecked = false;
                Barras.IsChecked = false;
                Columnas.IsChecked = false;
                Areas.IsChecked = false;
            }
        }

        private void Columnas_Click(object sender, RoutedEventArgs e)
        {
            if (Columnas.IsChecked == true)
            {
                hoja.tipografica = 1;
                Puntos.IsChecked = false;
                Linea.IsChecked = false;
                Barras.IsChecked = false;
                Areas.IsChecked = false;
            }
        }

        private void Areas_Click(object sender, RoutedEventArgs e)
        {
            if (Areas.IsChecked == true)
            {
                hoja.tipografica = 4;
                Puntos.IsChecked = false;
                Barras.IsChecked = false;
                Columnas.IsChecked = false;
                Linea.IsChecked = false;
            }
        }


        private void MostrarError(string mensaje)
        {
            string titulo = "WExcel";
            MessageBoxButton botones = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Error;
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        private void Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            hoja.nombre = Nombre.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!guardarcambios)HojaDefault();
            DialogResult = true;
        }

        private void Tamaño_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.Parse(Tamaño.Text) > 0 && double.Parse(Tamaño.Text) < 21)
                {
                    hoja.tamano = double.Parse(Tamaño.Text);
                }
                else
                {
                    MostrarError("El valor debe estar entre 1 y 20");
                    Tamaño.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            guardarcambios = true;
            DialogResult = true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            HojaDefault();
            DialogResult = true;
        }

        private void Restablecer_Click(object sender, RoutedEventArgs e)
        {

            HojaDefault();
            Tamaño.SelectedItem = tamano;
            Nombre.Text = nombre;

            ColorRelleno.ItemsSource = typeof(Colors).GetProperties();
            ColorLinea.ItemsSource = typeof(Colors).GetProperties();
            ColorSeleccion.ItemsSource = typeof(Colors).GetProperties();

            ColorRelleno.SelectedItem = typeof(Colors).GetProperty(brocha);
            ColorLinea.SelectedItem = typeof(Colors).GetProperty(brochalinea);
            ColorSeleccion.SelectedItem = typeof(Colors).GetProperty(brochaseleccion);

            MarcaGrafica(tipografica);
        }

        private void HojaDefault()
        {
            hoja.nombre = nombre;
            hoja.brocha = brocha;
            hoja.brochalinea = brochalinea;
            hoja.brochaseleccion = brochaseleccion;
            hoja.tamano = tamano;
            hoja.tipografica = tipografica;
        }

        private void MarcaGrafica(int tipo)
        {
            switch (tipo)
            {
                case 0:
                    Areas.IsChecked = false;
                    Puntos.IsChecked = false;
                    Barras.IsChecked = false;
                    Columnas.IsChecked = false;
                    Linea.IsChecked = true;
                    break;
                case 1:
                    Areas.IsChecked = false;
                    Puntos.IsChecked = false;
                    Barras.IsChecked = false;
                    Columnas.IsChecked = true;
                    Linea.IsChecked = false;
                    break;
                case 2:
                    Areas.IsChecked = false;
                    Puntos.IsChecked = true;
                    Barras.IsChecked = false;
                    Columnas.IsChecked = false;
                    Linea.IsChecked = false;
                    break;
                case 3:
                    Areas.IsChecked = false;
                    Puntos.IsChecked = false;
                    Barras.IsChecked = true;
                    Columnas.IsChecked = false;
                    Linea.IsChecked = false;
                    break;
                case 4:
                    Areas.IsChecked = true;
                    Puntos.IsChecked = false;
                    Barras.IsChecked = false;
                    Columnas.IsChecked = false;
                    Linea.IsChecked = false;
                    break;
            }
        }

        private void Opacidad_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.Parse(Opacidad.Text) >= 0 && double.Parse(Opacidad.Text) <= 1)
                {
                    hoja.opacidad = double.Parse(Opacidad.Text);
                }
                else
                {
                    MostrarError("El valor debe estar entre 0 y 1");
                    Opacidad.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
    }
}
