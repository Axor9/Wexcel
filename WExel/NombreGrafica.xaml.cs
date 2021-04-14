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
    /// Lógica de interacción para NombreGrafica.xaml
    /// </summary>
    public partial class NombreGrafica : Window
    {
        public string nombre { get; set; }
        ObservableCollection<Hoja> ndatos;
        private bool coincide = false;

        public NombreGrafica(System.Collections.ObjectModel.ObservableCollection<Hoja> datos)
        {
            InitializeComponent();
            nombre = null;
            ndatos = datos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            coincide = false;
            if (Nombre.Text.Length != 0)
            {
                nombre = Nombre.Text;

                foreach(Hoja h in ndatos)
                {
                    if (String.Compare(h.nombre, nombre) == 0)
                    {
                        coincide = true;
                    }
                }
                if (coincide == false)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MostrarError("El nombre ya existe");
                }
            }
        }
        private void MostrarError(string mensaje)
        {
            string titulo = "WExcel";
            MessageBoxButton botones = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Error;
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                coincide = false;
                if (Nombre.Text.Length != 0)
                {
                    nombre = Nombre.Text;

                    foreach (Hoja h in ndatos)
                    {
                        if (String.Compare(h.nombre, nombre) == 0)
                        {
                            coincide = true;
                        }
                    }
                    if (coincide == false)
                    {
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        MostrarError("El nombre ya existe");
                    }
                }
            }
        }
    }
}
