using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Data.Model
{
    class Couleur
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public Couleur(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
