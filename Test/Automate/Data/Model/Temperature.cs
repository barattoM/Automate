using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Data.Model
{
    class Temperature
    {
        public string ValTemperature { get; set; }

        public Temperature(string temperature)
        {
            this.ValTemperature = temperature;
        }
    }
}
