using AutoMapper;
using ProjetAutomate.Data.Dtos;
using ProjetAutomate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetAutomate.Data.Profiles
{
    public class Afpa_SeuilsProfile : Profile
    {
        public Afpa_SeuilsProfile()
        {
            CreateMap<Afpa_Seuil, Afpa_SeuilsDTOIn>();
            CreateMap<Afpa_SeuilsDTOIn ,Afpa_Seuil>();
            CreateMap<Afpa_Seuil, Afpa_SeuilsDTOOut>();
            CreateMap<Afpa_SeuilsDTOOut, Afpa_Seuil>();
        }
    }
}
