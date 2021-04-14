using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Reflection;

namespace WExel
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {


        ObservableCollection<Hoja> datos;
        ObservableCollection<Valor> purga;
        ObservableCollection<Comentario> comentarios;


        String ruta = null;

        Line ejex = new Line();
        Line ejey = new Line();

        double xeje = 100;
        double xejen = -100;
        double yeje = 100;
        double yejen = -100;

        Hoja hojaseleccionada = new Hoja();
        Comentario comentarioseleccionado = new Comentario();

        private bool mousepresionado = false;
        private bool ctrlpresionado = false;
        private bool mousecomentario = false;
        private bool mousetextbox = false;


        private Point mouseorigen;
        private Point mouseorigenrectangulo;
        private Point posicioncomentario;

        TranslateTransform movimientoraton;



        private double[] tamanos = {1,2,4,6,8,10,14,16,20};

        Rectangle rectangle;
        

        public MainWindow()
        {
            InitializeComponent();
            datos = new ObservableCollection<Hoja>();
            purga = new ObservableCollection<Valor>();
            comentarios = new ObservableCollection<Comentario>();
            

            GraficaColor.ItemsSource = typeof(Colors).GetProperties();
            GraficaColor.SelectedIndex = 7;

            Tamaño.ItemsSource = tamanos;

            movimientoraton = new TranslateTransform();


        }


        //------------------------------------------------Tipos de Graficas-------------------------------//

        private void dibujarGrafica(Hoja hoja)
        {
            double xreal, yreal, xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax, xpant, ypant;

            Polyline polilinea = new Polyline();
            polilinea.Name = hoja.nombre;

            Color color = (Color)ColorConverter.ConvertFromString(hoja.brocha);
            SolidColorBrush brush = new SolidColorBrush(color);

            polilinea.Stroke = brush;
            polilinea.StrokeThickness = hoja.tamano;
            polilinea.Opacity = hoja.opacidad;

            int i = 0;

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;

            Point puntos = new Point();

            foreach (Valor valor in hoja.hoja)
            {
                puntos = new Point();

                xreal = valor.x;
                yreal = valor.y;
                if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3*Math.Exp(12))
                {

                    xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                    ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                    puntos.X = xpant;
                    puntos.Y = ypant;

                    polilinea.Points.Add(puntos);

                }
                i++;
                
            }
            Lienzo.Children.Add(polilinea);


        }

        private void dibujarColumnas(Hoja hoja)
        {
            double xreal, yreal, xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax, xpant, ypant;

            

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;


            foreach (Valor valor in hoja.hoja)
            {
                xreal = valor.x;
                yreal = valor.y;

                if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3 * Math.Exp(12))
                {

                    xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                    ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                    añadirRectangulo(xpant+movimientoraton.X, ypant+movimientoraton.Y, yreal,hoja);
                }
            }
            
        }
        private void añadirRectangulo(double xpant,double ypant,double yreal,Hoja hoja)
        {
            TranslateTransform tt = new TranslateTransform();
            TransformGroup tg = new TransformGroup();

            double ypantmax = Lienzo.ActualHeight;
            double ypantmin = 0;

            double yrealMax = yeje;
            double yrealMin = yejen;

            double ejey = (ypantmin - ypantmax) * (0 - yrealMin) / (yrealMax - yrealMin) + ypantmax;

            Color color = (Color)ColorConverter.ConvertFromString(hoja.brocha);
            SolidColorBrush brush = new SolidColorBrush(color);

            Color color2 = (Color)ColorConverter.ConvertFromString(hoja.brochalinea);
            SolidColorBrush brush2 = new SolidColorBrush(color2);


            Rectangle r = new Rectangle();
            r.Name = hoja.nombre;


            r.Fill = brush;
            r.Stroke = brush2;
            r.Opacity = hoja.opacidad;

            r.Width = hoja.tamano;
            

            if (yreal < 0)
            {
                r.Height = ypant - (ejey + movimientoraton.Y);
                tt.X = xpant - Math.Sqrt(Math.Pow(((xpant - r.Width/2) - xpant), 2) + Math.Pow((ypant - ypant), 2));
                tt.Y = ejey + movimientoraton.Y;
            }
            else
            {
                r.Height = (ejey + movimientoraton.Y) - ypant;
                tt.X = xpant - Math.Sqrt(Math.Pow(((xpant - r.Width/2) - xpant), 2) + Math.Pow((ypant - ypant), 2));
                tt.Y = ypant;
            }

            tg.Children.Add(tt);
            r.RenderTransform = tg;
            Lienzo.Children.Add(r);
        }

        private void dibujarPuntos(Hoja hoja)
        {
            int numpuntos;
            double xreal, yreal, xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax, xpant, ypant;

            

            numpuntos = datos.Count;

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;


            foreach (Valor valor in hoja.hoja)
            {
                xreal = valor.x;
                yreal = valor.y;

                if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3 * Math.Exp(12))
                {

                    xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                    ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                    AñadirCirculo(xpant+movimientoraton.X, ypant+movimientoraton.Y,hoja);
                }
            }
        }
        private void AñadirCirculo(double xpant, double ypant,Hoja hoja)
        {
            TranslateTransform tt = new TranslateTransform();
            TransformGroup tg = new TransformGroup();

            Color color = (Color)ColorConverter.ConvertFromString(hoja.brocha);
            SolidColorBrush brush = new SolidColorBrush(color);

            Color color2 = (Color)ColorConverter.ConvertFromString(hoja.brochalinea);
            SolidColorBrush brush2 = new SolidColorBrush(color2);

            Ellipse p = new Ellipse();
            p.Name = hoja.nombre;


            p.Height = hoja.tamano;
            p.Width = hoja.tamano;
            p.Stroke = brush2;
            p.Fill = brush;
            p.Opacity = hoja.opacidad;

            tt.X = xpant - Math.Sqrt(Math.Pow(((xpant - p.Width / 2) - xpant), 2) + Math.Pow((ypant - ypant), 2));
            tt.Y = ypant - Math.Sqrt(Math.Pow((xpant - xpant), 2) + Math.Pow(((ypant - p.Height / 2) - ypant), 2));

            tg.Children.Add(tt);
            p.RenderTransform = tg;
            Lienzo.Children.Add(p);


        }

        private void dibujarBarras(Hoja hoja)
        {
            double xreal, yreal, xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax, xpant, ypant;

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;


            foreach (Valor valor in hoja.hoja)
            {
                xreal = valor.x;
                yreal = valor.y;

                if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3 * Math.Exp(12))
                {

                    xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                    ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                    añadirRectanguloHorizontal(xpant + movimientoraton.X, ypant + movimientoraton.Y, xreal,hoja);
                }
            }
        }
        private void añadirRectanguloHorizontal(double xpant, double ypant, double xreal,Hoja hoja)
        {
            TranslateTransform tt = new TranslateTransform();
            TransformGroup tg = new TransformGroup();

            double xpantmin = 0;
            double xpantmax = Lienzo.ActualWidth;

            double xrealMax = xeje;
            double xrealMin = xejen;

            double ejex = (xpantmax - xpantmin) * (0 - xrealMin) / (xrealMax - xrealMin) + xpantmin; ;

            Color color = (Color)ColorConverter.ConvertFromString(hoja.brocha);
            SolidColorBrush brush = new SolidColorBrush(color);

            Color color2 = (Color)ColorConverter.ConvertFromString(hoja.brochalinea);
            SolidColorBrush brush2 = new SolidColorBrush(color2);

            Rectangle r = new Rectangle();
            r.Name = hoja.nombre;


            r.Fill = brush;
            r.Stroke = brush2;
            r.Opacity = hoja.opacidad;
            r.Height = hoja.tamano;

            if (xreal < 0)
            {
                
                r.Width = (ejex + movimientoraton.X) - xpant;
                tt.X = xpant;
                tt.Y = ypant - Math.Sqrt(Math.Pow(((ypant - r.Height / 2) - ypant), 2) + Math.Pow((xpant - xpant), 2));
            }
            else
            {
                r.Width = xpant - (ejex + movimientoraton.X);
                tt.X = ejex + movimientoraton.X;
                tt.Y = ypant - Math.Sqrt(Math.Pow(((ypant - r.Height / 2) - ypant), 2) + Math.Pow((xpant - xpant), 2)); ;
            }

            tg.Children.Add(tt);
            r.RenderTransform = tg;
            Lienzo.Children.Add(r);
        }

        private void dibujarAreas(Hoja hoja)
        {
            double xreal, yreal, xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax, xpant, ypant;

            Polyline polilinea = new Polyline();
            polilinea.Name = hoja.nombre;

            Color color = (Color)ColorConverter.ConvertFromString(hoja.brocha);
            SolidColorBrush brush = new SolidColorBrush(color);

            Color color2 = (Color)ColorConverter.ConvertFromString(hoja.brochalinea);
            SolidColorBrush brush2 = new SolidColorBrush(color2);

            Valor valor1 = new Valor();

            double xmax = new double();
            double xmin = new double();

            polilinea.Stroke = brush2;
            polilinea.StrokeThickness = hoja.tamano;
            polilinea.Fill = brush;
            polilinea.Fill.Opacity = hoja.opacidad;

            int i = 0;

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;

            Point puntos = new Point();

            if (hoja.hoja.Count > 0)
            {
                xmax = hoja.hoja.Max(valor => valor.x);
                xmin = hoja.hoja.Min(valor => valor.x);
            }


            xreal = xmin;
            yreal = 0;
            if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3 * Math.Exp(12))
            {

                xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                puntos.X = xpant;
                puntos.Y = ypant;

                polilinea.Points.Add(puntos);

            }

            foreach (Valor valor in hoja.hoja)
            {
                puntos = new Point();

                xreal = valor.x;
                yreal = valor.y;
                if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3 * Math.Exp(12))
                {

                    xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                    ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                    puntos.X = xpant;
                    puntos.Y = ypant;

                    polilinea.Points.Add(puntos);

                }
                i++;

            }

            xreal = xmax;
            yreal = 0;
            if (!double.IsInfinity(yreal) && !double.IsNaN(yreal) && yreal <= 3 * Math.Exp(12))
            {

                xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                puntos.X = xpant;
                puntos.Y = ypant;

                polilinea.Points.Add(puntos);

            }

            Lienzo.Children.Add(polilinea);
        }


        //------------------------------------------Menu------------------------------------------------//

        //.............Archivo..........//

        //Manual//

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if (datos.Count == 0)
            {
                Grafica.Items.Clear();
                datos.Clear();
                comentarios.Clear();
                Hoja hoja = new Hoja();
                hoja.nombre = GraficaNombre();

                datos.Add(hoja);
                CreaMenuItem(hoja);
                CreaVentanaDataWindow(hoja);
            }
            else
            {
                
                string msg = "Se van a perder todos los datos actuales.\n¿Desea salvar los cambios?";
                string titulo = "WExcel";

                Hoja hoja = new Hoja();
                MessageBoxResult resultado;
                MessageBoxButton botones = MessageBoxButton.YesNoCancel;
                MessageBoxImage icono = MessageBoxImage.Question;
                resultado = MessageBox.Show(msg, titulo, botones, icono);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:

                        Grafica.Items.Clear();
                        Guardar_Click(this, null);

                        datos.Clear();
                        comentarios.Clear();

                        hoja.nombre = GraficaNombre();
                        datos.Add(hoja);

                        CreaMenuItem(hoja);


                        CreaVentanaDataWindow(hoja);
                        Modo(hoja);
                        break;
                    case MessageBoxResult.No:

                        Grafica.Items.Clear();
                        datos.Clear();
                        comentarios.Clear();


                        hoja.nombre = GraficaNombre();
                        datos.Add(hoja);

                        CreaMenuItem(hoja);


                        CreaVentanaDataWindow(hoja);
                        Modo(hoja);
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }

        }
        private void CreaVentanaDataWindow(Hoja hoja)
        {
            try
            {
                DataWindow dw = new DataWindow(hoja);
                dw.Owner = this;
                dw.Title = hoja.nombre;
                dw.nuevosDatos += Data_nuevosDatos;
                dw.Show();
            }
            catch(Exception err)
            {
                MostrarError(err.ToString());
            }
        }

        private void Data_nuevosDatos(object sender, DatosEventArgs e)
        {
            int index=0;
            Hoja h = new Hoja();

            foreach(Hoja hoja in datos)
            {
                if (hoja.nombre == e.datos.nombre)
                {
                    index = datos.IndexOf(hoja);
                    h = e.datos;
                }
            }
            datos.RemoveAt(index);
            datos.Insert(index, h);
            dibuja();
            
        }

        //Automatico//

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (datos.Count == 0)
            {
                try
                {
                    Grafica.Items.Clear();
                    datos.Clear();
                    comentarios.Clear();

                    Hoja hoja = new Hoja();
                    hoja.nombre = GraficaNombre();

                    CreaMenuItem(hoja);

                    datos.Add(hoja);
                    CreaVentanaDataAutoWindow(hoja);
                }catch(Exception ex)
                {
                    MostrarError(ex.Message);
                }
            }
            else
            {
                Hoja hoja = new Hoja();
                string msg = "Se van a perder todos los datos actuales.\n¿Desea salvar los cambios?";
                string titulo = "WExcel";

                MessageBoxResult resultado;
                MessageBoxButton botones = MessageBoxButton.YesNoCancel;
                MessageBoxImage icono = MessageBoxImage.Question;
                resultado = MessageBox.Show(msg, titulo, botones, icono);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        Guardar_Click(this, null);
                        Grafica.Items.Clear();

                        datos.Clear();
                        hoja.nombre = GraficaNombre();

                        datos.Add(hoja);
                        comentarios.Clear();


                        CreaMenuItem(hoja);

                        dibuja();
                        CreaVentanaDataAutoWindow(hoja);
                        break;
                    case MessageBoxResult.No:
                        Grafica.Items.Clear();

                        datos.Clear();
                        hoja.nombre = GraficaNombre();

                        datos.Add(hoja);
                        comentarios.Clear();


                        CreaMenuItem(hoja);

                        dibuja();
                        CreaVentanaDataAutoWindow(hoja);
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }


            

        }
        private void CreaVentanaDataAutoWindow(Hoja hoja)
        {
            try
            {
                DataAutoWindow daw = new DataAutoWindow(hoja);
                daw.Owner = this;
                daw.Title = hoja.nombre;
                daw.nuevosDatos += DataAuto_nuevosDatos;
                daw.Show();
                
            }
            catch(Exception err)
            {
                MostrarError(err.ToString());
            }
        }

        private void DataAuto_nuevosDatos(object sender, DatosAutoEventArgs e)
        {
            int index = 0;
            Hoja h = new Hoja();

            foreach (Hoja hoja in datos)
            {
                if (hoja.nombre == e.datos.nombre)
                {
                    index = datos.IndexOf(hoja);
                    h = e.datos;
                }
            }
            datos.RemoveAt(index);
            datos.Insert(index, h);
            dibuja();
        }

        //Abrir//

        private void Abrir_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

            Stream myStream;
            BinaryFormatter datosbinario = new BinaryFormatter();
            Microsoft.Win32.OpenFileDialog dlg;

            string msg = "Se van a perder los datos actuales. ¿Desea salvar los cambios?";
            string titulo = "WExcel";
            MessageBoxResult resultado;
            MessageBoxButton botones = MessageBoxButton.YesNoCancel;
            MessageBoxImage icono = MessageBoxImage.Question;
            resultado = MessageBox.Show(msg, titulo, botones, icono);

            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    GuardarComo_Click(this, null);
                    datos.Clear();
                    Grafica.Items.Clear();

                    dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.FileName = "Documento"; // Nombre por defecto
                    dlg.DefaultExt = ".txt"; // Extensión por defecto
                    dlg.Filter = "Bin de texto(.bin) | *.bin"; // Filtro

                    if (dlg.ShowDialog() == true)
                    {
                        if ((myStream = (FileStream)dlg.OpenFile()) != null)
                        {
                            ruta = System.IO.Path.GetFullPath(dlg.FileName);
                            datos = (ObservableCollection<Hoja>)datosbinario.Deserialize(myStream);
                            dibuja();
                            foreach (Hoja hoja in datos)
                            {
                                CreaMenuItem(hoja);
                            }
                        }
                    }
                    break;
                case MessageBoxResult.No:
                    datos.Clear();
                    Grafica.Items.Clear();

                    dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.FileName = "Documento"; // Nombre por defecto
                    dlg.DefaultExt = ".txt"; // Extensión por defecto
                    dlg.Filter = "Bin de texto(.bin) | *.bin"; // Filtro


                    if (dlg.ShowDialog() == true)
                    {
                        if ((myStream = (FileStream)dlg.OpenFile()) != null)
                        {
                            ruta = System.IO.Path.GetFullPath(dlg.FileName);
                            datos = (ObservableCollection<Hoja>)datosbinario.Deserialize(myStream);
                            dibuja();
                            foreach (Hoja hoja in datos)
                            {
                                CreaMenuItem(hoja);

                            }
                        }
                    }
                    break;
                case MessageBoxResult.Cancel:
                    break;
            } 
        }


        //Guardar//

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;


            Stream myStream;
            BinaryFormatter datosbinario = new BinaryFormatter();


            if (ruta == null)
            {
                GuardarComo_Click(this, null);
            }
            else
            {
                if ((myStream = File.Create(ruta)) != null)
                {
                    datosbinario.Serialize(myStream, datos);
                    byte[] byteArray = new byte[myStream.Length];
                    myStream.Write(byteArray, 0, (int)myStream.Length);
                    myStream.Close();

                }
                else
                {
                    MostrarError("Error en el guardado");
                }
            }
        }

        //GuardarComo//

        private void GuardarComo_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

            Stream myStream;
            BinaryFormatter datosbinario = new BinaryFormatter();
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.Filter = "bin files (*.bin)|*.bin";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;


            if (saveFileDialog1.ShowDialog() == true)
            {              
                ruta = System.IO.Path.GetFullPath(saveFileDialog1.FileName);
                if ((myStream = (FileStream)saveFileDialog1.OpenFile()) != null)
                {
                    datosbinario.Serialize(myStream, datos);
                    byte[] byteArray = new byte[myStream.Length];
                    myStream.Write(byteArray, 0, (int)myStream.Length);

                    myStream.Close();

                }
            }
        }

        //Exportar//

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            

            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.FileName = "Nueva_Imagen";
            saveFileDialog1.Filter = "png files (*.png)|*.png";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                String path = System.IO.Path.GetFullPath(saveFileDialog1.FileName);

                // Create a render bitmap and push the surface to it
                RenderTargetBitmap renderBitmap =
                  new RenderTargetBitmap(
                    (int)Lienzo.ActualWidth,
                    (int)Lienzo.ActualHeight,
                    96d,
                    96d,
                    PixelFormats.Pbgra32);
                renderBitmap.Render(Lienzo);

                // Create a file stream for saving image
                using (FileStream outStream = new FileStream(path, FileMode.Create))
                {
                    // Use png encoder for our data
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    // push the rendered bitmap to it
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    // save the data to the stream
                    encoder.Save(outStream);
                }

            }
        }
        //Cerrar//

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msg = "La aplicación va a cerrar. ¿Desea salvar los cambios?";
            string titulo = "WExcel";

            if (datos.Count != 0)
            {
                MessageBoxResult resultado;
                MessageBoxButton botones = MessageBoxButton.YesNoCancel;
                MessageBoxImage icono = MessageBoxImage.Question;
                resultado = MessageBox.Show(msg, titulo, botones, icono);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        Guardar_Click(this, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        //.............Grafica..........//

        private void Click_Grafica(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;

            foreach(Hoja h in datos)
            {
                if (String.Compare(h.nombre, menu.Name)==0)
                {
                    hojaseleccionada = h;
                    break;
                }
            }
            foreach(MenuItem menuitem in Grafica.Items)
            {
                if(menuitem != menu)
                {
                    menuitem.IsChecked = false;
                }
            }
        }

        //............Añadir.........//
        private void Manual1_Click(object sender, RoutedEventArgs e)
        {
            string nombre = GraficaNombre();
            if (nombre != null)
            {
                Hoja h = new Hoja();
                h.nombre = nombre;
                datos.Add(h);
                CreaMenuItem(h);
                CreaVentanaDataWindow(h);
            }
        }

        private void Auto1_Click(object sender, RoutedEventArgs e)
        {
            string nombre = GraficaNombre();
            if (nombre != null)
            {
                Hoja h = new Hoja();
                h.nombre = nombre;
                datos.Add(h);
                CreaMenuItem(h);
                CreaVentanaDataAutoWindow(h);
            }
        }
        //.............Ver..........//

        private void Ejes_Click(object sender, RoutedEventArgs e)
        {
            dibuja();
        }

        private void ToolBar_Click(object sender, RoutedEventArgs e)
        {
            if (ToolBar.IsChecked)
            {
                BarTool.Visibility = Visibility.Visible;
                Grid.SetRow(Lienzo, 2);
                Grid.SetRowSpan(Lienzo, 1);
                dibuja();
            }
            else
            {
                BarTool.Visibility = Visibility.Collapsed;
                Grid.SetRow(Lienzo, 1);
                Grid.SetRowSpan(Lienzo, 2);
                dibuja();
            }

        }



        //--------------------------------------Click Derecho--------------------------------------------//

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

        private void Propiedades_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuaux = new MenuItem();
            MenuItem menunuevo = new MenuItem();
            int index = 0;
            bool encontrado = false;
            ClickDerecho.Visibility = Visibility.Collapsed;

            if (hojaseleccionada.nombre != null)
            {
                foreach (MenuItem menu in Grafica.Items)
                {
                    if (String.Compare(menu.Name, hojaseleccionada.nombre) == 0)
                    {
                        menuaux = menu;
                    }
                }
                index = Grafica.Items.IndexOf(menuaux);
                Grafica.Items.Remove(menuaux);

                PropertiesWindow pw = new PropertiesWindow(hojaseleccionada);
                pw.Owner = this;
                pw.Title = hojaseleccionada.nombre;
                pw.ShowDialog();

                if(pw.DialogResult == true)
                {

                    Hoja aux = pw.hoja;
                    datos.Remove(hojaseleccionada);
                    foreach (Hoja h in datos)
                    {
                        if (String.Compare(h.nombre, aux.nombre) == 0)
                        {
                            encontrado = true;
                        }
                    }
                    if (encontrado || aux.nombre.Length==0)
                    {
                        datos.Add(hojaseleccionada);
                        MostrarError("No se pudo modificar las propiedades");
                    }
                    else
                    {
                        hojaseleccionada = aux;
                        datos.Add(hojaseleccionada);
                    }

                    menunuevo.Name = aux.nombre;
                    menunuevo.Header = aux.nombre;
                    menunuevo.Click += Click_Grafica;
                    menunuevo.IsCheckable = true;
                    menunuevo.IsChecked = true;
                    Grafica.Items.Insert(index, menunuevo);
                }
                dibuja();
            }
        }

        private void Manual2_Click(object sender, RoutedEventArgs e)
        {
            CreaVentanaDataWindow(hojaseleccionada);
        }

        private void Auto2_Click(object sender, RoutedEventArgs e)
        {
            CreaVentanaDataAutoWindow(hojaseleccionada);
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;
            MenuItem aux = new MenuItem();

            string msg = "¿Está seguro de que desea eliminar la gráfica";
            string titulo = hojaseleccionada.nombre;
            MessageBoxResult resultado;
            MessageBoxButton botones = MessageBoxButton.YesNoCancel;
            MessageBoxImage icono = MessageBoxImage.Question;

            
            resultado = MessageBox.Show(msg, titulo, botones, icono);
            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    foreach(MenuItem menu in Grafica.Items)
                    {
                        if (String.Compare(menu.Name, hojaseleccionada.nombre) == 0)
                        {
                            aux = menu;
                        }
                    }
                    datos.Remove(hojaseleccionada);
                    Grafica.Items.Remove(aux);
                    hojaseleccionada = new Hoja();
                    dibuja();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }


        //---------------------------------------Seleccion----------------------------------------------//


        private void Lienzo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

            FrameworkElement fm = e.OriginalSource as FrameworkElement;
            if (fm is Canvas)
            {
                comentarioseleccionado.textBlock.Visibility = Visibility.Visible;
                comentarioseleccionado.textBox.Visibility = Visibility.Collapsed;
                comentarioseleccionado.rectangle.Visibility = Visibility.Collapsed;
                comentarioseleccionado = new Comentario();

            }


            mousepresionado = true;
            mouseorigen.X = e.GetPosition(Lienzo).X -movimientoraton.X;
            mouseorigen.Y = e.GetPosition(Lienzo).Y - movimientoraton.Y;

            mouseorigenrectangulo = e.GetPosition(Lienzo);

            posicioncomentario = e.GetPosition(Lienzo);
            if (fm is Canvas)
            {
                Lienzo.Children.Clear();
                purga.Clear();
                dibuja();
            }
            Lienzo_Loaded();

        }

        private void Lienzo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mousecomentario = false;
            mousetextbox = false;

            if (mousepresionado) mousepresionado = false;
            if (ctrlpresionado == false)
            {
                Point curMouseDownPoint = new Point();
                curMouseDownPoint.X = e.GetPosition(Lienzo).X - movimientoraton.X;
                curMouseDownPoint.Y = e.GetPosition(Lienzo).Y - movimientoraton.Y;
                Seleccion(mouseorigen, curMouseDownPoint);
                Lienzo.Children.Remove(rectangle);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Lienzo_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (mousepresionado == true && ctrlpresionado == false && mousecomentario == false && mousetextbox == false)
            {
                Point puntoActualRaton = e.GetPosition(Lienzo);
                 ActualizaRectangulo(mouseorigenrectangulo, puntoActualRaton);

            }else if(mousepresionado == true && ctrlpresionado == true && mousecomentario == false && mousetextbox == false)
            {
                this.Cursor = Cursors.Hand;
                Point puntoActualRaton = e.GetPosition(Lienzo);
                muevecanvas(mouseorigen, puntoActualRaton);
            }else if(mousecomentario == true && ctrlpresionado == false)
            {
                Point puntoActualRaton = e.GetPosition(Lienzo);
                muevecomentario(puntoActualRaton);
            }else if(mousetextbox == true && ctrlpresionado == false)
            {
                Point puntoActualRaton = e.GetPosition(Lienzo);
                cambiatamanotextbox(posicioncomentario, puntoActualRaton);
            }
        }

        private void ActualizaRectangulo(Point pt1, Point pt2)
        {
            double x, y, width, height;

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }
            Lienzo.Children.Remove(rectangle);

            rectangle = new Rectangle();
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Fill = Brushes.LightBlue;
            rectangle.Stroke = Brushes.Blue;
            rectangle.StrokeThickness = 1;
            rectangle.Opacity = 0.5;
            rectangle.MouseLeftButtonUp += Lienzo_MouseLeftButtonUp;

            TranslateTransform tt = new TranslateTransform();

            tt.X = x;
            tt.Y = y;

            rectangle.RenderTransform = tt;
            Lienzo.Children.Add(rectangle);
        }

        private void Seleccion(Point pt1, Point pt2)
        {

            double x, y, width, height;

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }
            Rect dragRect = new Rect(x, y, width, height);

            double xreal, yreal, xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax, xpant, ypant;

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;


            foreach (Hoja h in datos)
            {
                foreach (Valor valor in h.hoja)
                {
                    xreal = valor.x;
                    yreal = valor.y;

                    xpant = (xpantmax - xpantmin) * (xreal - xrealMin) / (xrealMax - xrealMin) + xpantmin;
                    ypant = (ypantmin - ypantmax) * (yreal - yrealMin) / (yrealMax - yrealMin) + ypantmax;

                    if (dragRect.Contains(xpant,ypant))
                    {
                        if (h.tipografica == 0 || h.tipografica == 2 || h.tipografica == 4)
                        {
                            Hoja hojaseleccion = new Hoja(null, null, "seleccion12345",
                               h.brochaseleccion, h.brochaseleccion, h.brochaseleccion, h.tamano + 3, 0, 1);
                            AñadirCirculo(xpant+movimientoraton.X, ypant+ movimientoraton.Y, hojaseleccion);
                        }
                        else if (h.tipografica == 1)
                        {
                            Hoja hojaseleccion = new Hoja(null, null, "seleccion12345",
                               h.brochaseleccion, h.brochaseleccion, h.brochaseleccion, h.tamano, 0, 1);
                            añadirRectangulo(xpant + movimientoraton.X, ypant+movimientoraton.Y, yreal, hojaseleccion);
                        }
                        else if(h.tipografica == 3)
                        {
                            Hoja hojaseleccion = new Hoja(null, null, "seleccion12345",
                               h.brochaseleccion, h.brochaseleccion, h.brochaseleccion, h.tamano, 0, 1);
                            añadirRectanguloHorizontal(xpant + movimientoraton.X, ypant + movimientoraton.Y, xreal, hojaseleccion);
                        }
                        purga.Add(valor);
                    }
                }
            }
        }
     
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ObservableCollection<Valor> eliminar = new ObservableCollection<Valor>();
            int index = 0;
            Boolean encontrado = false;

            if(e.Key == Key.Delete)
            {
                if (purga.Count > 0)
                {
                    foreach (Hoja hoja in datos)
                    {
                        hoja.hojactrlz.Clear();
                        foreach(Valor valor in hoja.hoja)
                        {
                            hoja.hojactrlz.Add(valor);
                        }
                    }


                    foreach (Hoja hoja in datos)
                    {
                        foreach (Valor v in hoja.hoja)
                        {
                            if (!purga.Contains(v))
                            {
                                eliminar.Add(v);
                            }
                        }
                    }
                    foreach (Hoja hoja in datos)
                    {
                        foreach (Valor v in eliminar)
                        {
                            hoja.hoja.Remove(v);
                        }
                    }
                }
                else
                {
                    foreach (Comentario c in comentarios)
                    {
                        if (c == comentarioseleccionado)
                        {
                            index = comentarios.IndexOf(c);
                            encontrado = true;
                        }
                    }
                    if (encontrado)
                    {
                        comentarios.RemoveAt(index);
                    }
                }
                dibuja();
            }

            if(e.Key == Key.Enter)
            {
                cambiartamano();
            }
            if(e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                Lienzo.Children.Remove(rectangle);
                ctrlpresionado = true;
            }
            if(ctrlpresionado && e.Key == Key.Z)
            {

                foreach (Hoja hoja in datos)
                {
                    if (hoja.hojactrlz.Count > 0)
                    {
                        hoja.hoja.Clear();
                        foreach (Valor valor in hoja.hojactrlz)
                        {
                            hoja.hoja.Add(valor);
                        }
                    }
                }
                
                dibuja();
            }
        }

        private void SeleccionarPolilinea(object sender, MouseButtonEventArgs e)
        {       
             Polyline poli = (Polyline)sender;

            foreach (Hoja hoja in datos)
                {
                    if (String.Compare(hoja.nombre, poli.Name) == 0)
                    {
                        System.Console.WriteLine("polilinea");
                        hojaseleccionada = hoja;
                        break;
                    }
                }
        }
        private void SeleccionarRectangulo(object sender, MouseButtonEventArgs e)
        {
            
            Rectangle poli = (Rectangle)sender;

            foreach (Hoja hoja in datos)
            {
                if (String.Compare(hoja.nombre, poli.Name) == 0)
                {
                    System.Console.WriteLine("rectangulo");
                    hojaseleccionada = hoja;
                    break;
                }
            }
        }
        private void SeleccionarEllipse(object sender, MouseButtonEventArgs e)
        {
            
            Ellipse poli = (Ellipse)sender;

            foreach (Hoja hoja in datos)
            {
                if (String.Compare(hoja.nombre,poli.Name)== 0)
                {
                    System.Console.WriteLine("circulo");
                    hojaseleccionada = hoja;
                    break;
                }
            }
        }


        //--------------------------------------------ToolBar--------------------------------------//

        private void Color_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(PropertyInfo propertyInfo in typeof(Colors).GetProperties())
            {
                if (propertyInfo.Equals(GraficaColor.SelectedItem))
                {
                    hojaseleccionada.brocha = propertyInfo.Name;
                    hojaseleccionada.brochalinea = propertyInfo.Name;
                }
            }
            dibuja();
        }

        private void zoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            xeje = Zoom.Value;
             xejen = -Zoom.Value;
             yeje = Zoom.Value;
             yejen = -Zoom.Value;

            dibuja();
        }

        private void cambiartamano()
        {

            if (double.Parse(Tamaño.Text) > 0 && double.Parse(Tamaño.Text) < 21)
            {
                /*foreach(Hoja h in datos)
                {
                    h.tamano = double.Parse(Tamaño.Text);
                }*/
                hojaseleccionada.tamano = double.Parse(Tamaño.Text);

                dibuja();
            }
            else
            {
                MostrarError("El valor debe estar entre 1 y 20");
                Tamaño.SelectedIndex = 1;
            }
        } 

        private void BarTool_GotFocus(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;

        }

        private void PosIni_Click(object sender, RoutedEventArgs e)
        {
            movimientoraton = new TranslateTransform();
            dibuja();
        }

        private void Polilinea_Click(object sender, RoutedEventArgs e)
        {
            hojaseleccionada.tipografica = 0;
            dibuja();
        }

        private void Barras_Click(object sender, RoutedEventArgs e)
        {
            hojaseleccionada.tipografica = 3;
            dibuja();
        }

        private void Puntos_Click(object sender, RoutedEventArgs e)
        {
            hojaseleccionada.tipografica = 2;
            dibuja();
        }

        private void Columnas_Click(object sender, RoutedEventArgs e)
        {
            hojaseleccionada.tipografica = 1;
            dibuja();
        }
        private void Area_Click(object sender, RoutedEventArgs e)
        {
            hojaseleccionada.tipografica = 4;
            dibuja();
        }

        //......Manejo de comentarios........///

        private void Comentario_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform tt = new TranslateTransform();
            tt.X = 2.3;
            tt.Y = 2.3;
            Comentario comment = new Comentario();

            comment.textBlock.MouseLeftButtonDown += MouseDownComentario;
            comment.textBlock.MouseLeftButtonUp += MouseUpComentario;

            comment.textBox.TextChanged += ComentarioText;
            comment.textBox.AcceptsReturn = true;
            comment.textBox.RenderTransform = tt;

            comment.rectangle.MouseLeftButtonDown += MouseDownRectangle;
            comment.rectangle.MouseLeftButtonUp += MouseUpRectangle;


            comentarios.Add(comment);
            dibuja();
        }

        //TextBox//

        private void MouseUpRectangle(object sender, MouseButtonEventArgs e)
        {
            mousetextbox = false;
        }

        private void MouseDownRectangle(object sender, MouseButtonEventArgs e)
        {
            posicioncomentario = e.GetPosition(Lienzo);

            mousetextbox = true;
        }

        private void cambiatamanotextbox(Point pt1, Point pt2)
        {
            this.Cursor = Cursors.SizeAll;
            double width, height;
            double x, y;

            TranslateTransform tt = new TranslateTransform();
            TranslateTransform tt2 = new TranslateTransform();

            foreach (Comentario c in comentarios)
            {
                if (comentarioseleccionado == c)
                {
                    if (pt2.X < pt1.X)
                    {
                        x = pt2.X;
                        width = pt1.X - pt2.X;
                    }
                    else
                    {
                        x = pt1.X;
                        width = pt2.X - pt1.X;
                    }

                    if (pt2.Y < pt1.Y)
                    {
                        y = pt2.Y;
                        height = pt1.Y - pt2.Y;
                    }
                    else
                    {
                        y = pt1.Y;
                        height = pt2.Y - pt1.Y;
                    }

                    tt.X = x;
                    tt.Y = y;

                    tt2.X = x + 2.3;
                    tt2.Y = y + 2.3;

                    c.textBlock.RenderTransform = tt;
                    c.textBox.RenderTransform = tt2;
                    c.rectangle.RenderTransform = tt;

                    if ((width > 0) && (height > 0))
                    {
                        c.textBox.Width = width;
                        c.textBox.Height = height;

                        c.textBlock.Width = width;
                        c.textBlock.Height = height;


                        c.rectangle.Width = width + 5;
                        c.rectangle.Height = height + 5;
                    }
                }
            }

            dibuja();
        }

        private void ComentarioText(object sender, TextChangedEventArgs e)
        {
            foreach(Comentario c in comentarios)
            {
                if(c == comentarioseleccionado)
                {
                    c.settexto(c.textBox.Text);

                }
            }
        }

        //TextBlock//

        private void MouseUpComentario(object sender, MouseButtonEventArgs e)
        {
            mousecomentario = false;
        }

        private void MouseDownComentario(object sender, MouseButtonEventArgs e)
        {

            foreach (Comentario c in comentarios)
            {
                if (c.textBlock == sender) comentarioseleccionado = c;
            }


            if (e.ClickCount >= 2)
            {
                foreach(Comentario c in comentarios)
                {
                    if(comentarioseleccionado == c)
                    {
                        c.textBlock.Visibility = Visibility.Collapsed;
                        c.textBox.Visibility = Visibility.Visible;
                        c.rectangle.Visibility = Visibility.Visible;

                    }
                }
            }
            else
            {
                posicioncomentario = e.GetPosition(Lienzo);
                mousecomentario = true;

            }


        }

        private void muevecomentario(Point pt2)
        {
            TranslateTransform movimientocomentario = new TranslateTransform();
            TranslateTransform movimientotextbox = new TranslateTransform();

            movimientocomentario.X = pt2.X;
            movimientocomentario.Y = pt2.Y;

            movimientotextbox.X = pt2.X + 2.3;
            movimientotextbox.Y = pt2.Y + 2.3;

            foreach (Comentario c in comentarios)
            {
                if (comentarioseleccionado == c)
                {
                    
                    c.textBlock.RenderTransform = movimientocomentario;
                    c.textBox.RenderTransform = movimientotextbox;
                    c.rectangle.RenderTransform = movimientocomentario;

                }
            }

            dibuja();
        }


        //---------------------------------Funciones-----------------------------------------//

        private void Modo(Hoja hoja)
        {
            switch (hoja.tipografica)
            {
                case 0:
                    dibujarGrafica(hoja);
                    break;
                case 1:
                    dibujarColumnas(hoja);
                    break;
                case 2:
                    dibujarPuntos(hoja);
                    break;
                case 3:
                    dibujarBarras(hoja);
                    break;
                case 4:
                    dibujarAreas(hoja);
                    break;
            }
        }

        private void dibuja()
        {
            if (datos != null)
            {

                Lienzo.Children.Clear();
                if (Ejes.IsChecked) CrearEjes();
                foreach (Hoja hoja in datos)
                {
                    Modo(hoja);
                }
                foreach (Polyline poli in Lienzo.Children.OfType<Polyline>())
                {
                    poli.RenderTransform = movimientoraton;
                }
                foreach (Line line in Lienzo.Children.OfType<Line>())
                {
                    line.RenderTransform = movimientoraton;
                }
                foreach(Comentario c in comentarios)
                {
                    Lienzo.Children.Add(c.rectangle);
                    Lienzo.Children.Add(c.textBlock);
                    Lienzo.Children.Add(c.textBox);

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

        private void Lienzo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
             dibuja();
        }

        private string GraficaNombre()
        {
            NombreGrafica vn = new NombreGrafica(datos);
            vn.Owner = this;
            vn.ShowDialog();
            if(vn.DialogResult == true)
            {
                return vn.nombre;
            }
            else
            {
                return null;
            }
        }

        private void CrearEjes()
        {
            double xrealMax, yrealMax, xrealMin, yrealMin, xpantmin, xpantmax, ypantmin, ypantmax;

            xpantmin = 0;
            xpantmax = Lienzo.ActualWidth;
            ypantmax = Lienzo.ActualHeight;
            ypantmin = 0;

            xrealMax = xeje;
            xrealMin = xejen;
            yrealMax = yeje;
            yrealMin = yejen;


            ejex.Stroke = Brushes.Gray;
            ejey.Stroke = Brushes.Gray;

            ejex.X1 = -xpantmax * 10;
            ejex.X2 = xpantmax * 10;
            ejex.Y1 = (ypantmin - ypantmax) * (0 - yrealMin) / (yrealMax - yrealMin) + ypantmax;
            ejex.Y2 = (ypantmin - ypantmax) * (0 - yrealMin) / (yrealMax - yrealMin) + ypantmax;

            ejey.X1 = (xpantmax - xpantmin) * (0 - xrealMin) / (xrealMax - xrealMin) + xpantmin;
            ejey.X2 = (xpantmax - xpantmin) * (0 - xrealMin) / (xrealMax - xrealMin) + xpantmin;
            ejey.Y1 = -ypantmax * 10;
            ejey.Y2 = ypantmax*10;

            Lienzo.Children.Add(ejex);
            Lienzo.Children.Add(ejey);
        }

        private void Lienzo_Loaded()
        {
            //Asigna la funcion para seleccionar al evento MouseDown
            foreach (Polyline poli in Lienzo.Children.OfType<Polyline>())
            {
                poli.MouseDown += SeleccionarPolilinea;
            }
            foreach (Rectangle rect in Lienzo.Children.OfType<Rectangle>())
            {
                rect.MouseDown += SeleccionarRectangulo;
            }
            foreach (Ellipse elli in Lienzo.Children.OfType<Ellipse>())
            {
                elli.MouseDown += SeleccionarEllipse;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                ctrlpresionado = false;
            }
        }

        private void muevecanvas(Point pt1, Point pt2)
        {

            movimientoraton.X = pt2.X - pt1.X;
            movimientoraton.Y = pt2.Y - pt1.Y;

            foreach (Polyline poli in Lienzo.Children.OfType<Polyline>())
            {
                poli.RenderTransform = movimientoraton;
            }
            foreach (Line line in Lienzo.Children.OfType<Line>())
            {
                line.RenderTransform = movimientoraton;
            }
            dibuja();

        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (ctrlpresionado)
            {
                Zoom.Value = Zoom.Value + e.Delta/10;
            }
        }

        private void MenuPrincipal_GotFocus(object sender, RoutedEventArgs e)
        {
            ClickDerecho.Visibility = Visibility.Collapsed;
        }
        private void CreaMenuItem(Hoja hoja)
        {
            MenuItem menuItem = new MenuItem();

            menuItem.Name = hoja.nombre;
            menuItem.Header = hoja.nombre;
            menuItem.Click += Click_Grafica;
            menuItem.IsCheckable = true;
            Grafica.Items.Add(menuItem);
        }


    }
}
