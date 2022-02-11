using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetAutomate.Data.Dtos;
using ProjetAutomate.Data.Models;
using ProjetAutomate.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetAutomate.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class Afpa_CadencesController : ControllerBase
    {

        private readonly Afpa_CadencesServices _service;
        private readonly IMapper _mapper;

        public Afpa_CadencesController(Afpa_CadencesServices service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/Afpa_Cadences
        [HttpGet]
        public ActionResult<IEnumerable<Afpa_CadencesDTOOut>> GetAllAfpa_Cadences()
        {
            IEnumerable<Afpa_Cadence> listeAfpa_Cadences = _service.GetAllAfpa_Cadences();
            return Ok(_mapper.Map<IEnumerable<Afpa_CadencesDTOOut>>(listeAfpa_Cadences));
        }

        //GET api/Afpa_Cadences/ByDate/{date}
        [HttpGet("ByDate/{date}", Name = "GetAfpa_CadencesByDate")]
        public ActionResult<IEnumerable<Afpa_CadencesDTOOut>> GetAfpa_CadencesByDate(DateTime date)
        {
            IEnumerable<Afpa_Cadence> listeAfpa_Cadences = _service.GetAfpa_CadencesByDate(date);
            return Ok(_mapper.Map<IEnumerable<Afpa_CadencesDTOOut>>(listeAfpa_Cadences));
        }

        //GET api/Afpa_Cadences/ByDate/AVG/{date}
        [HttpGet("ByDate/AVG/{date}", Name = "GetAVGAfpa_CadencesByDate")]
        public ActionResult<float> GetAVGAfpa_CadencesByDate(DateTime date)
        {
            IEnumerable<Afpa_Cadence> listeAfpa_Cadences = _service.GetAfpa_CadencesByDate(date);
            int somme = 0;
            foreach (Afpa_Cadence afpa_Cadence in listeAfpa_Cadences)
            {
                somme += afpa_Cadence.NbProduit;
            }
            return Ok(somme / listeAfpa_Cadences.Count());
        }

        [HttpGet("ByDate/Arret/{date}", Name = "GetArretsByDate")]
        public ActionResult<int> GetArretsByDate(DateTime date)
        {
            IEnumerable<Afpa_Cadence> listeAfpa_Cadences = _service.GetAfpa_CadencesByDate(date);
            int arret = 0;
            bool enArret = false;
            foreach (Afpa_Cadence afpa_Cadence in listeAfpa_Cadences)
            {
                // Si on a pas de production et qu'on est pas déjà en arret
                // ça évite de compter plusieurs fois le même arrêt
                if (afpa_Cadence.NbProduit == 0 && !enArret)
                {
                    arret++;
                    enArret = true;
                }
                // Si on était en arret que la cadence est supérieur a 0, on est donc plus en arrêt.
                if (afpa_Cadence.NbProduit != 0 && enArret)
                {
                    enArret = false;
                }
            }
            return Ok(arret);
        }


        //GET api/Afpa_Cadences/{i}
        [HttpGet("{id}", Name = "GetAfpa_CadenceById")]
        public ActionResult<Afpa_CadencesDTOOut> GetAfpa_CadenceById(int id)
        {
            Afpa_Cadence commandItem = _service.GetAfpa_CadenceById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<Afpa_CadencesDTOOut>(commandItem));
            }
            return NotFound();
        }

        //POST api/Afpa_Cadences
        [HttpPost]
        public ActionResult<Afpa_CadencesDTOOut> CreateAfpa_Cadence(Afpa_CadencesDTOIn objIn)
        {
            Afpa_Cadence obj = _mapper.Map<Afpa_Cadence>(objIn);
            _service.AddAfpa_Cadence(obj);
            return CreatedAtRoute(nameof(GetAfpa_CadenceById), new { Id = obj.IdCadence }, obj);
        }

        //POST api/Afpa_Cadences/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateAfpa_Cadence(int id, Afpa_CadencesDTOIn obj)
        {
            Afpa_Cadence objFromRepo = _service.GetAfpa_CadenceById(id);
            if (objFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(obj, objFromRepo);
            _service.UpdateAfpa_Cadence(objFromRepo);
            return NoContent();
        }

        //DELETE api/Afpa_Cadences/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAfpa_Cadence(int id)
        {
            Afpa_Cadence obj = _service.GetAfpa_CadenceById(id);
            if (obj == null)
            {
                return NotFound();
            }
            _service.DeleteAfpa_Cadence(obj);
            return NoContent();
        }
    }

}
