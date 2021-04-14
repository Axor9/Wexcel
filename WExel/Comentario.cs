using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WExel
{
    public class Comentario
    {
        public TextBox textBox { get; set; }
        public TextBlock textBlock { get; set; }
        public Rectangle rectangle { get; set; }

        public int altura;
        public int ancho;
        public string texto;

        public int getaltura()
        {
            return altura;
        }
        public void setaltura(int alt)
        {
            altura = alt;
            textBox.Height = altura;
            textBlock.Height = altura;
            rectangle.Height = altura+5;

        }
        public int getancho()
        {
            return ancho;
        }
        public void setancho(int anc)
        {
            ancho = anc;
            textBlock.Width = ancho;
            textBox.Width = ancho;
            rectangle.Width = ancho+5;
        }

        public string gettexto()
        {
            return texto;
        }
        public void settexto(string text)
        {
            texto = text;
            textBlock.Text = texto;
            textBox.Text = texto;
        }


        public Comentario(TextBox h1, TextBlock h2)
        {
            textBox = h1;
            textBlock = h2;

        }

        public Comentario()
        {
            textBox = new TextBox();
            textBlock = new TextBlock();
            rectangle = new Rectangle();

            textBlock.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Collapsed;
            rectangle.Visibility = Visibility.Collapsed;

            rectangle.Fill = Brushes.White;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 4;

            setaltura(30);
            setancho(120);
            settexto("Nuevo Comentario");
        }
    }
}
