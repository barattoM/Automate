using Microsoft.EntityFrameworkCore;
using ProjetAutomate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetAutomate.Data.Services
{
    public class Afpa_AnomaliesServices
    {

        private readonly AutomateContext _context;

        public Afpa_AnomaliesServices(AutomateContext context)
        {
            _context = context;
        }

        public void AddAfpa_Anomalie(Afpa_Anomalie obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _context.Afpa_Anomalies.Add(obj);
            _context.SaveChanges();
        }

        public void DeleteAfpa_Anomalie(Afpa_Anomalie obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _context.Afpa_Anomalies.Remove(obj);
            _context.SaveChanges();
        }


        public IEnumerable<Afpa_Anomalie> GetAllAfpa_Anomalies()
        {
            return _context.Afpa_Anomalies.Include("Erreur").ToList();
        }

        public IEnumerable<Afpa_Anomalie> GetAfpa_AnomaliesByType(string type)
        {
            return _context.Afpa_Anomalies.Where(a => a.TypeAnomalie == type).ToList();
        }

        public IEnumerable<Afpa_Anomalie> GetAfpa_AnomaliesByDate(DateTime date)
        {
            return _context.Afpa_Anomalies.Where(a => a.DateAnomalie.Date == date).ToList();
        }
        
        public IEnumerable<Afpa_Anomalie> GetAfpa_AnomaliesByInterval(DateTime date1, DateTime date2)
        {
            return _context.Afpa_Anomalies.Where(a => a.DateAnomalie.Date >= date1 && a.DateAnomalie.Date <= date2).ToList();
        }

        public Afpa_Anomalie GetAfpa_AnomalieById(int id)
        {
            return _context.Afpa_Anomalies.Include("Erreur").FirstOrDefault(obj => obj.IdAnomalie == id);
        }

        public void UpdateAfpa_Anomalie(Afpa_Anomalie obj)
        {
            _context.SaveChanges();
        }
    }
}
