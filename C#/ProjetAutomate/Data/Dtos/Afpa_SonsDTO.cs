﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetAutomate.Data.Dtos
{
    public class Afpa_SonsDTO
    {

        public class Afpa_SonsDTOIn
        {
            public int? ValeurSon { get; set; }
            public DateTime? DateSon { get; set; }

        }

        public class Afpa_SonsDTOOut
        {
            public int? ValeurSon { get; set; }
            public DateTime? DateSon { get; set; }

        }
    }
}
