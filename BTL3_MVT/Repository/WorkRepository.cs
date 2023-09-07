using BTL3_MVT.Data;
using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;
using BTL3_MVT.Repository.IRepository;

namespace BTL3_MVT.Repository
{
    public class WorkRepository : EntityBaseRepository<Work>, IWorkRepository
    {
        private readonly PersonContext _context;

        public WorkRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

        public int GetIdByName(string? name)
        {
            if (name == null) return 0;
            var works = _context.Works.FirstOrDefault(e => e.WorkName.ToLower() == name.ToLower());
            return works.WorkId;
        }
    }
}
