using BTL3_MVT.Models;

namespace BTL3_MVT.Service.IService
{
    public interface ICityService
    {
        IEnumerable<CityDTO> GetAllCitys(string searchQuery);
        CityDTO GetCityById(int id);
        int GetIdByName(string cityName);
        int AddCity(CityDTO cityDTO);
        void UpdateCity(CityDTO cityDTO);
        void DeleteCity(int id);
    }
}
