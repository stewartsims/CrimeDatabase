using CrimeDatabase.Models;

namespace CrimeDatabase.Data
{
    // repository pattern used for crime event to enable mocking during unit testing
    public interface ICrimeEventRepository
    {
        public List<CrimeEvent> GetAll();
        public CrimeEvent? GetById(int id);
        public CrimeEvent Create(CrimeEvent crimeEvent);
        public CrimeEvent Update(CrimeEvent crimeEvent);
        public CrimeEvent DeleteById(int id);
        public List<CrimeEvent> Search(string queryString);
    }
}
