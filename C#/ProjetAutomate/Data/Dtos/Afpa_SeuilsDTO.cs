using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetAutomate.Data.Dtos
{
    public class Afpa_SeuilsDTOIn
    {
        public int? SeuilBas { get; set; }
        public int? SeuilHaut { get; set; }
        public DateTime? DateSeuil { get; set; }
    }
    public class Afpa_SeuilsDTOOut
    {
        public int? SeuilBas { get; set; }
        public int? SeuilHaut { get; set; }
        public DateTime? DateSeuil { get; set; }
    }
}
