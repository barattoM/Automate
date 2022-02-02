using System;
using System.Collections.Generic;

#nullable disable

namespace ProjetAutomate.Data.Models
{
    public partial class Afpa_Seuil
    {
        public int IdSeuil { get; set; }
        public int? SeuilBas { get; set; }
        public int? SeuilHaut { get; set; }
        public DateTime? DateSeuil { get; set; }
        public int? Temps { get; set; }
    }
}
