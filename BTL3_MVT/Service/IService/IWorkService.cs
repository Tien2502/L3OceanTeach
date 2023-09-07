using BTL3_MVT.Models;

namespace BTL3_MVT.Service.IService
{
    public interface IWorkService
    {
        IEnumerable<WorkDTO> GetAllWorks(string searchQuery);
        WorkDTO GetWorkById(int id);
        int AddWork(WorkDTO workDTO);
        void UpdateWork(WorkDTO workDTO);
        void DeleteWork(int id);
    }
}
