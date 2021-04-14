using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para WindowEditar.xaml
    /// </summary>
    /// 

    public partial class WindowEditar : Window
    {
        public Valor v { get; set; }
        public WindowEditar()
        {
            InitializeComponent();

        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Valor valor = new Valor(int.Parse(Box1.Text), int.Parse(Box2.Text));
                v = valor;
                DialogResult = true;
                Close();
            }
            catch
            {
                string msg = "El valor editado debe ser un ENTERO";
                string titulo = "WExcel";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    Valor valor = new Valor(int.Parse(Box1.Text), int.Parse(Box2.Text));
                    v = valor;
                    DialogResult = true;
                    Close();
                }
                catch
                {
                    string msg = "El valor editado debe ser un ENTERO";
                    string titulo = "WExcel";
                    MessageBoxButton botones = MessageBoxButton.OK;
                    MessageBoxImage icono = MessageBoxImage.Error;
                    MessageBox.Show(msg, titulo, botones, icono);
                }
            }
        }
    }
}
