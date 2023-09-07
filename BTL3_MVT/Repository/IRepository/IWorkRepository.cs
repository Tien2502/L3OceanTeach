using BTL3_MVT.Data.Entity;
using BTL3_MVT.Repository.BaseRepository;

namespace BTL3_MVT.Repository.IRepository
{
    public interface IWorkRepository : IEntityBaseRepository<Work>
    {
        int GetIdByName(string name);
    }
}
