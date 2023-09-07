using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;

namespace BTL3_MVT.Repository
{
    public class EthnicityRepository : EntityBaseRepository<Ethnicity>, IEthnicityRepository
    {
        private readonly PersonContext _context;

        public EthnicityRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public int GetIdByName(string? name)
        {
            if (name == null) return 0;
            var ethnicitys = _context.Ethnicity.FirstOrDefault(e => e.EthnicityName.ToLower() == name.ToLower());
            return ethnicitys.EthnicityId;
        }
    }
}
