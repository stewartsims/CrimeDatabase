using CrimeDatabase.Models;

namespace CrimeDatabase.Data
{
    public interface IAuditLogRepository
    {
        public List<AuditLog> GetAll();
        public AuditLog? GetById(int id);
        public AuditLog Create(AuditLog crimeEvent);
        public AuditLog Update(AuditLog crimeEvent);
        public AuditLog DeleteById(int id);
    }
}
