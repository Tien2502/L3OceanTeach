using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BTL3_MVT.Repository
{
    public class PersonRepository : EntityBaseRepository<Person>, IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context) : base(context)
        {
            _context = context;
        }
        
        public Person GetById(int id)
        {
            return _context.Persons
                .Include(p => p.Work)
                .Include(p => p.Ethnicity)
                .Include(p => p.City)
                .Include(p => p.District)
                .Include(p => p.Ward)
                .Include(p => p.WorkCity)
                .Include(p => p.WorkDistrict)
                .Include(p => p.WorkWard)
                .FirstOrDefault(p => p.PersonId == id);
        }
    }
}
