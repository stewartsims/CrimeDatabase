using CrimeDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace CrimeDatabase.Data
{
    public class CrimeEventRepository : ICrimeEventRepository
    {
        private readonly CrimeDatabaseContext _context;

        public CrimeEventRepository(CrimeDatabaseContext context)
        {
            _context = context;
        }

        List<CrimeEvent> ICrimeEventRepository.GetAll()
        {
            return _context.CrimeEvent.ToList();
        }

        CrimeEvent? ICrimeEventRepository.GetById(int id)
        {
            var crimeEvent = _context.CrimeEvent
                .FirstOrDefault(m => m.Id == id);
            return crimeEvent;
        }

        CrimeEvent ICrimeEventRepository.Create(CrimeEvent crimeEvent)
        {
            _context.Add(crimeEvent);
            _context.SaveChanges();
            AuditLog auditLog = new AuditLog { ActionType = "Crime event with ID: " + crimeEvent.Id + " created", ActionDateTime = DateTime.Now, CrimeEventID = crimeEvent.Id };
            _context.Add(auditLog);
            _context.SaveChanges();
            return crimeEvent;
        }

        CrimeEvent ICrimeEventRepository.Update(CrimeEvent crimeEvent)
        {
            _context.Update(crimeEvent);
            _context.SaveChanges();
            AuditLog auditLog = new AuditLog { ActionType = "Crime event with ID: " + crimeEvent.Id + " updated", ActionDateTime = DateTime.Now, CrimeEventID = crimeEvent.Id };
            _context.Add(auditLog);
            _context.SaveChanges();
            return crimeEvent;
        }

        CrimeEvent? ICrimeEventRepository.DeleteById(int id)
        {
            var crimeEvent = _context.CrimeEvent
                .FirstOrDefault(m => m.Id == id);
            if (crimeEvent == null)
            {
                return null;
            }
            AuditLog auditLog = new AuditLog { ActionType = "Crime event with ID: " + crimeEvent.Id + " deleted", ActionDateTime = DateTime.Now };
            _context.Add(auditLog);
            // before we delete the crime event we must remove the explicit id reference from all previous audit log entries
            _context.AuditLog
                .Where(existingAuditLog => existingAuditLog.CrimeEventID == crimeEvent.Id)
                .ForEachAsync(existingAuditLog => existingAuditLog.CrimeEventID = null).Wait();
            _context.CrimeEvent.Remove(crimeEvent);
            _context.SaveChanges();
            return crimeEvent;
        }

        List<CrimeEvent> ICrimeEventRepository.Search(string queryString)
        {
            var crimes = from crime in _context.CrimeEvent
                         select crime;

            if (!String.IsNullOrEmpty(queryString))
            {
                crimes = crimes.Where(crime =>
                    crime.LocationArea.Contains(queryString) ||
                    crime.LocationTown.Contains(queryString) ||
                    crime.VictimName.Contains(queryString)
                );
            }
            var crimesToReturn = crimes.ToList();
            return crimesToReturn;
        }
    }
}
