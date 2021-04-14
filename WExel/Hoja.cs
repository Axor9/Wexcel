using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WExel
{
    [Serializable]
    public class Hoja
    {

        //getter setter

        public ObservableCollection<Valor> hoja { get; set; }
        public ObservableCollection<Valor> hojactrlz { get; set; }
        public string nombre { get; set; }
        public string brocha { get; set; }
        public string brochalinea { get; set; }
        public string brochaseleccion { get; set; }
        public double tamano { get; set; }
        public double opacidad { get; set; }
        public int tipografica { get; set;}

        //Constructores
        public Hoja(ObservableCollection<Valor> h1, ObservableCollection<Valor> h8, string h6,string h2, string h3,string h7,double h4,int h5,double h9)
        {
            hoja = h1;
            hojactrlz = h8;
            nombre = h6;
            brocha = h2;
            brochalinea = h3;
            brochaseleccion = h7;
            tamano = h4;
            tipografica = h5;
            opacidad = h9;
        }

        public Hoja()
        {
            hoja = new ObservableCollection<Valor>();
            hojactrlz = new ObservableCollection<Valor>();
            nombre = null;
            brocha = "Black";
            brochalinea = "Black";
            brochaseleccion = "CadetBlue";
            tamano = 2;
            tipografica = 0;
            opacidad = 1;
        }
    }
}
