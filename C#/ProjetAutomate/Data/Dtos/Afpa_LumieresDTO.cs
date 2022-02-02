using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetAutomate.Data.Dtos
{
   
        public class Afpa_LumieresDTOIn
        {
            public int? ValeurLumiere { get; set; }
            public DateTime? DateLumiere { get; set; }
        }

        public class Afpa_LumieresDTOOut
        {
            public int? ValeurLumiere { get; set; }
            public DateTime? DateLumiere { get; set; }
        }

}
