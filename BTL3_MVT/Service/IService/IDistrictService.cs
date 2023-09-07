using BTL3_MVT.Models;

namespace BTL3_MVT.Service.IService
{
    public interface IDistrictService
    {
        IEnumerable<DistrictDTO> GetAllDistricts();
        DistrictDTO GetDistrictById(int id);
        IEnumerable<DistrictDTO> GetDistrictsByCityId(string searchQuery, int id);
        int AddDistrict(DistrictDTO districtDTO);
        void UpdateDistrict(DistrictDTO districtDTO);
        void DeleteDistrict(int id);
    }
}
