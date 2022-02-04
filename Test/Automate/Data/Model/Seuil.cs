using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Data.Model
{
    class Seuil
    {
        public float SeuilBas { get; set; }
        public float SeuilHaut { get; set; }
        public int Temps { get; set; }

        public Seuil(float seuilBas, float seuilHaut, int temps)
        {
            this.SeuilBas = seuilBas;
            this.SeuilHaut = seuilHaut;
            this.Temps = temps;
        }
        public Seuil()
        {

        }
    }
}
