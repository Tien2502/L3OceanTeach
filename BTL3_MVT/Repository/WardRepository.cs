using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;

namespace BTL3_MVT.Repository
{
    public class WardRepository : EntityBaseRepository<Ward>, IWardRepository
    {
        private readonly PersonContext _context;

        public WardRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public int GetIdByName(string? name)
        {
            if (name == null) return 0;
            var wards = _context.Wards.FirstOrDefault(w => w.WardName.ToLower() == name.ToLower());
            return wards.WardId;
        }
    }
}
