using BTL3_MVT.Models;

namespace BTL3_MVT.Service.IService
{
    public interface IWardService
    {
        IEnumerable<WardDTO> GetAllWards();
        WardDTO GetWardById(int id);
        IEnumerable<WardDTO> GetWardsByDistrictId(string searchQuery, int id);
        int AddWard(WardDTO wardDTO);
        void UpdateWard(WardDTO wardDTO);
        void DeleteWard(int id);
    }
}
