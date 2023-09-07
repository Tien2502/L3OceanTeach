using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;

namespace BTL3_MVT.Repository
{
    public class CityRepository : EntityBaseRepository<City>, ICityRepository
    {
        private readonly PersonContext _context;

        public CityRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public int GetIdByName(string? name)
        {
            if (name == null) return 0;
            var citys = _context.Citys.FirstOrDefault(c => c.CityName.ToLower() == name.ToLower());
            return citys.CityId;
        }
    }
}
