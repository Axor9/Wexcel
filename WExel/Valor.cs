using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WExel
{
    [Serializable]
    public class Valor
    {
        //Getter y setter
        public double x { get; set; }

        public double y { get; set; }


        //Constructor
        public Valor(double v1, double v2)
        {
            x = v1;
            y = v2;
        }

        public Valor()
        {
        }
    }
}
