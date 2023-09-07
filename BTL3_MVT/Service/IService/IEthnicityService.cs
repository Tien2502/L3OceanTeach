using BTL3_MVT.Models;

namespace BTL3_MVT.Service.IService
{
    public interface IEthnicityService
    {
        IEnumerable<EthnicityDTO> GetAllEthnicitys(string searchQuery);
         EthnicityDTO GetEthnicityById(int id);
        int AddEthnicity(EthnicityDTO ethnicityDTO);
        void UpdateEthnicity(EthnicityDTO ethnicityDTO);
        void DeleteEthnicity(int id);
    }
}

