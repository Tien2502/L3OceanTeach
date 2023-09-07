using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;

namespace BTL3_MVT.Repository
{
    public class DistrictRepository : EntityBaseRepository<District>, IDistrictRepository
    {
        private readonly PersonContext _context;

        public DistrictRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public int GetIdByName(string? name)
        {
            if (name == null) return 0;
            var districts = _context.Districts.FirstOrDefault(d => d.DistrictName.ToLower() == name.ToLower());
            return districts.DistrictId;
        }
    }
}
